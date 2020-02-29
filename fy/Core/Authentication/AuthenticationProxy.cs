using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Modobay
{
    public class AuthenticationProxy : IAuthenticationProxy
    {
        private readonly Modobay.IAppContext app;
        private IAuthentication authentication;

        public AuthenticationProxy(Modobay.IAppContext appContext)
        {
            this.app = appContext;
        }

        private IAuthentication Authentication
        {
            get
            {
                if (authentication == null)
                {
                    authentication = app.GetService<IAuthentication>();
                }
                return authentication;
            }
        }

        public UserDto SetCurrentUser(string token, ActionExecutingContext context = null)
        {
            UserDto user = null;
            if (context != null && IsCheckToken(context))
            {
                // 要求检查token
                if (string.IsNullOrEmpty(token)) throw new AppException("token不能为空");
                var key =  Cache.CacheManager.Key("AuthenticationProxy", "SetCurrentUser", token);
                user =  Cache.CacheManager.Get<UserDto>(key);
                if (user == null)
                {
                    user = Authentication.CheckToken(token);
                     Cache.CacheManager.Set(key, user, TimeSpan.FromMinutes(3));
                }
            }
            else if (!string.IsNullOrEmpty(token))
            {
                // 不要求检查token，但附带了token，也需要设置到app.User（部分标记为NonToken的接口还是会获取当前用户信息）
                var key = Cache.CacheManager.Key("AuthenticationProxy", "SetCurrentUser", token);
                user = Cache.CacheManager.Get<UserDto>(key);
                if (user == null)
                {
                    try
                    {
                        user = Authentication.CheckToken(token);
                    }
                    catch (Exception ex)
                    {
                        // 无需记录 Lib.Log.WriteExceptionLog($"SetCurrentUser:Message:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); 
                    }
                    finally
                    {
                         Cache.CacheManager.Set(key, user, TimeSpan.FromMinutes(3));
                    }
                }
            }

            app.User = user;
            return user;
        }

        public UserDto SetCurrentGuest(string clientId)
        {
            if (string.IsNullOrEmpty(clientId) || clientId.Length < 6) return null;
            var user = new UserDto() { UserID = 0, UserName = $"Guest_{clientId.Left(6)}" };
            user.NickName = user.UserName;
            app.GuestUser = user;
            return user;
        }

        internal static bool IsCheckToken(ActionExecutingContext context)
        {
            bool checkAndThrow = false;
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor == null) return checkAndThrow;

            if (controllerActionDescriptor.ControllerTypeInfo.Name.EndsWith("Proxy"))
            {
                // 代理类从源类型查找特性
                var controllerType = AppManager.ProxyOriginalTypes[controllerActionDescriptor.ControllerTypeInfo.FullName];
                var methodInfo = controllerType.GetMethod(controllerActionDescriptor.MethodInfo.Name);
                var controllerNonToken = controllerType.GetCustomAttributes(typeof(Modobay.Api.NonTokenAttribute), true);
                var actionNonToken = methodInfo.GetCustomAttributes(typeof(Modobay.Api.NonTokenAttribute), true);
                checkAndThrow = !(controllerNonToken.Count() == 1 || actionNonToken.Count() == 1);
            }
            else
            {
                var controllerNonToken = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes(typeof(Modobay.Api.NonTokenAttribute), true);
                var actionNonToken = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(Modobay.Api.NonTokenAttribute), true);
                checkAndThrow = !(controllerNonToken.Count() == 1 || actionNonToken.Count() == 1);
            }

            return checkAndThrow;
        }

        internal static bool IsCheckGateway(ActionExecutingContext context)
        {
            bool checkAndThrow = false;
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor == null) return checkAndThrow;

            if (controllerActionDescriptor.ControllerTypeInfo.Name.EndsWith("Proxy"))
            {
                // 代理类从源类型查找特性
                var controllerType = AppManager.ProxyOriginalTypes[controllerActionDescriptor.ControllerTypeInfo.FullName];
                var methodInfo = controllerType.GetMethod(controllerActionDescriptor.MethodInfo.Name);
                var controllerNonToken = controllerType.GetCustomAttributes(typeof(Modobay.Api.NonGatewayAttribute), true);
                var actionNonToken = methodInfo.GetCustomAttributes(typeof(Modobay.Api.NonGatewayAttribute), true);
                checkAndThrow = !(controllerNonToken.Count() == 1 || actionNonToken.Count() == 1);
            }
            else
            {
                var controllerNonToken = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes(typeof(Modobay.Api.NonGatewayAttribute), true);
                var actionNonToken = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(Modobay.Api.NonGatewayAttribute), true);
                checkAndThrow = !(controllerNonToken.Count() == 1 || actionNonToken.Count() == 1);
            }

            return checkAndThrow;
        }
    }
}
