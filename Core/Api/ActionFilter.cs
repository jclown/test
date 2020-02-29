using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Modobay.Cache;
using Modobay.AsyncTask;
using Modobay.Gateway;
using Modobay.OpenApi;
using System.Text.RegularExpressions;
using Lib;

namespace Modobay.Api
{
    internal class ActionFilter : IActionFilter
    {
        private readonly IAppContext app;
        private static bool _isEnableRealTimeLog = false;

        static ActionFilter()
        {
            _isEnableRealTimeLog = ConfigManager.Configuration[$"EnableRealTimeLog"] == "1";
        }

        public ActionFilter(IAppContext app)
        {
            this.app = app;
        }

        private bool IsEnableLog(ActionExecutedContext context)
        {
            if (!_isEnableRealTimeLog) return false;

            bool isEnable = true;
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                var controllerNonToken = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes(typeof(NonLogAttribute), true);
                var actionNonToken = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(NonLogAttribute), true);
                isEnable = !(controllerNonToken.Count() == 1 || actionNonToken.Count() == 1);
            }
            return isEnable;
        }

        private bool IsEnableLog(ActionExecutingContext context)
        {
            if (!_isEnableRealTimeLog) return false;

            bool isEnable = true;
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                var controllerNonToken = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes(typeof(NonLogAttribute), true);
                var actionNonToken = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(NonLogAttribute), true);
                isEnable = !(controllerNonToken.Count() == 1 || actionNonToken.Count() == 1);
            }
            return isEnable;
        }

        private bool IsEnableCookie(ActionExecutingContext context)
        {
            bool isEnable = true;
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                var controllerNonToken = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes(typeof(NonCookieAttribute), true);
                var actionNonToken = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(NonCookieAttribute), true);
                isEnable = !(controllerNonToken.Count() == 1 || actionNonToken.Count() == 1);
            }
            return isEnable;
        }

        private bool IsPackageResultDto(ActionExecutedContext context)
        {
            bool isEnable = true;
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                var controllerNonToken = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes(typeof(NonPackageResultDtoAttribute), true);
                var actionNonToken = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(NonPackageResultDtoAttribute), true);
                isEnable = !(controllerNonToken.Count() == 1 || actionNonToken.Count() == 1);
            }
            return isEnable;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var api = context.ActionDescriptor.AttributeRouteInfo.Template;
            ResultDto<object> resultValue = null;
            ObjectResult result = null;
            var isEnableLog = IsEnableLog(context);

            try
            {
                // 异常返回
                if (context.Exception != null) return;
                if (AppManager._proxyAppException != null)
                {
                    var exceptionResult = ResultBuilder.AsFail(AppManager._proxyAppException, app.ReuqestID);
                    AppManager._proxyAppException = null;
                    context.Result = new JsonResult(exceptionResult);
                    return;
                }
                if (app.ExceptionInfo != null)
                {
                    var exceptionResult = ResultBuilder.AsFail(app.ExceptionInfo, app.ReuqestID);
                    context.Result = new JsonResult(exceptionResult);
                    return;
                }               

                if (!IsPackageResultDto(context))
                {
                    return;
                }

                result = (context.Result as ObjectResult);
                if (result == null)
                {
                    resultValue = new ResultDto<object>()
                    {
                        Data = "此接口未定义返回数据，调用结果默认为成功。",
                        Success = true,
                        Message = "操作成功",
                    };
                }
                else
                {
                    resultValue = ResultBuilder.AsSuccess(result.Value);
                }

                resultValue.RequestId = app.ReuqestID;
                if (!string.IsNullOrEmpty(app.Message))
                {
                    resultValue.Message = app.Message;
                }

                context.Result = new JsonResult(resultValue);
                if (isEnableLog)
                {
                    app.LogResponseResult((object)resultValue ?? context.Result);
                }
                
                // todo 异步
                if (resultValue.Success)
                {
                    //// 推送系统消息
                    //var systemMessage = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetCustomAttributes(typeof(SystemMessageAttribute), true);
                    //if (systemMessage.Count() == 1)
                    //{
                    //    var message = systemMessage[0] as SystemMessageAttribute;
                    //    message.RelationID = int.Parse(ValueExpression.GetValue(message.RelationIDExpression, app.ActionArguments, resultValue.Data));
                    //    message.ToUserID = int.Parse(ValueExpression.GetValue(message.ToUserIDExpression, app.ActionArguments, resultValue.Data));
                    //    app.PushSystemMessage(message);
                    //}

                    // 记录用户行为
                    var userBehavior = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetCustomAttributes(typeof(UserBehaviorAttribute), true);
                    if (userBehavior.Count() == 1)
                    {
                        var behavior = userBehavior[0] as UserBehaviorAttribute;
                        behavior.BehaviorValue = ValueExpression.GetValue(behavior.BehaviorValueExpression, app.ActionArguments, resultValue.Data);
                        behavior.RelationID = int.Parse(ValueExpression.GetValue(behavior.RelationIDExpression, app.ActionArguments, resultValue.Data));
                        app.AddUserBehavior(behavior);
                    }
                }
            }
            catch (Exception ex) { Lib.Log.WriteExceptionLog($"OnActionExecuted:Message:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }
            finally
            {
                if (isEnableLog) app.EndRequest();
                CleanOnActionClose();
                if (resultValue != null)
                {
                    if (resultValue.Success) CacheManager.WriteCache(resultValue, context, app.AppID);
                }
                else if (result != null && result.Value != null)
                {
                    CacheManager.WriteCache(result.Value, context, app.AppID);
                }               
            }
        }

        private void CleanOnActionClose()
        {
            AppManager._proxyAppException = null;
            Lib.StopwatchLog.Stopwatch.Reset();
            Lib.StopwatchLog._stopwatchLogDetail.Clear();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var api = context.ActionDescriptor.AttributeRouteInfo.Template;
            var isEnableLog = IsEnableLog(context);

            try
            {
                // 设置当前访问信息
                string clientInfo = context.HttpContext.Connection?.RemoteIpAddress.ToString();
                string requestId = context.HttpContext.Request.Headers["requestId"];
                if (string.IsNullOrEmpty(requestId))
                {
                    requestId = context.HttpContext.TraceIdentifier;
                    context.HttpContext.Request.Headers["requestId"] = requestId;
                }
                app.ActionArguments = context.ActionArguments;
                AppManager.CurrentAppContext = app;

                // 记录日志                
                if (isEnableLog)
                {
                    string parm = JsonConvert.SerializeObject(app.ActionArguments);
                    app.LogRequest(requestId, context.ActionDescriptor.DisplayName, parm, clientInfo, context.ActionDescriptor.AttributeRouteInfo.Template);
                }

                // 读取或设置sessionId和language
                if (IsEnableCookie(context))
                {
                    InitCookies(api);
                }

                // 访问限制，登录等方法不受限制                
                if (AuthenticationProxy.IsCheckGateway(context))
                {
                    var clientIp = context.HttpContext.Connection?.RemoteIpAddress.ToString();
                    if (!string.IsNullOrEmpty(clientIp)) GatewayManager.Check(clientIp);
                }

                var authentication = app.GetService<IAuthenticationProxy>();
                              
                // 鉴权-检查用户token，设置当前用户上下文                
                var token = (context.HttpContext.Request.Headers[ApiDocManager.TokenHeaderName]).ToString().Replace("Bearer ", "");                
                if (AuthenticationProxy.IsCheckToken(context) || !string.IsNullOrEmpty(token))
                {
                    if (ConfigManager.Configuration["EnableApiDoc"] == "1" && string.IsNullOrEmpty(token))
                    {
                        token = ConfigManager.Configuration["DefaultToken"];
                    }
                    authentication.SetCurrentUser(token, context);
                }
                authentication.SetCurrentGuest(app.SessionID);

                // 检查数据
                VaildateModel(context);

                // 异步
                var asyncTask = AsyncTaskManager.GetTaskInfo(context, app.User?.UserID.ToString());
                if (asyncTask != null)
                {
                    var resultValue = ResultBuilder.AsSuccess(asyncTask);
                    context.Result = new JsonResult(resultValue);
                    return;
                }

                // 从缓存返回
                var cacheResult = CacheManager.ReadCache(context, app);
                if (cacheResult != null)
                {
                    cacheResult.RequestId = context.HttpContext.TraceIdentifier;
                    var cacheTimeRate = CacheTimeSettings.GetCacheTimeRate();
                    var cacheSeconds = CacheManager.CalcCacheSeconds(context.ActionDescriptor as ControllerActionDescriptor, out var cacheTimeSetting);
                    var seconds = cacheTimeRate.HttpCacheTime * cacheSeconds;
                    if (seconds < 10) seconds = 10;
                    if (seconds > 60) seconds = 60;

                    context.HttpContext.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(seconds)
                    };
                    context.Result = new JsonResult(cacheResult);
                    return;
                }
            }
            // 不捕获异常，由ExceptionFilter处理。
            finally
            {
                if (context.Result != null)
                {
                    CleanOnActionClose();
                    if (isEnableLog)
                    {
                        app.LogResponseResult(context.Result);
                        app.EndRequest();
                    }
                }
            }
        }

        private bool IsAllowAllAppid(ActionExecutingContext context)
        {
            bool IsAllowAllAppid = false;
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor == null) return IsAllowAllAppid;

            var controllerAttribute = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes(typeof(Modobay.Api.AllowAllAppidAttribute), true);
            var actionAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(Modobay.Api.AllowAllAppidAttribute), true);
            IsAllowAllAppid = (controllerAttribute.Count() == 1 || actionAttribute.Count() == 1);
            return IsAllowAllAppid;
        }

        private void InitCookies(string api)
        {
            if (string.IsNullOrEmpty(app.SessionID))// || api.IndexOf("CheckToken") >= 0)
            {
                app.SessionID = Guid.NewGuid().ToString().Replace("-", "");
            }
        }

        /// <summary>
        /// 校验模型
        /// </summary>
        /// <param name="context"></param>
        private void VaildateModel(ActionExecutingContext context)
        {
            var modelState = context.ModelState;
            if (modelState.IsValid) return;

            var errors = modelState.Values.SelectMany(v => v.Errors);
            var sb = new StringBuilder("参数错误：");
            var reg = new Regex("[\"'](.*?)[\"']");
            
            foreach (var err in errors)
            {
                var errMsg = string.IsNullOrEmpty(err.ErrorMessage) ? err.Exception?.Message : err.ErrorMessage;
                var errItem = reg.Match(errMsg)?.Value;
                sb.AppendLine(errItem);
            }

            throw new AppException(sb.ToString(), AppExceptionType.ParameterError);
        }
    }
}