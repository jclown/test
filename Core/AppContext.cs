using Modobay.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Modobay
{
    public class AppContext : IAppContext
    {
        private static readonly object SyncObj = new object();
        private static readonly List<RequestInfoDto> _requestInfoList = new List<RequestInfoDto>();
        public List<RequestInfoDto> RequestInfoList { get { return _requestInfoList; } }


        //private readonly IMSystem systemMessage;

        public AppContext(IHttpContextAccessor accessor, IServiceProvider serviceProvider)//, IMSystem systemMessage
        {
            _serviceProvider = serviceProvider;
            //this.systemMessage = systemMessage;

            //HttpContext = accessor?.HttpContext;
            //if (HttpContext != null)
            //{
            //    Modobay.AppManager.CurrentAppContext.AppID = this.AppID;
            //    Modobay.AppManager.CurrentAppContext.Language = this.Language;
            //}
        }

        private readonly IServiceProvider _serviceProvider;

        public IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
        }

        public T GetService<T>()
        {
            var item = default(T);
            var type = typeof(T);

            try
            {
                if (_serviceProvider != null)
                {
                    item = (T)_serviceProvider.GetService(type);
                }
                else
                {
                    Lib.Log.WriteOperationLog($"AppContext.GetService：{type.Name}");
                    item = (T)AppManager.GetServiceFromRoot(type, this);
                }
            }
            catch
            {
                System.Diagnostics.StackFrame fr = new System.Diagnostics.StackFrame(1, true);
                MethodBase mb = fr.GetMethod();
                Lib.Log.WriteExceptionLog($"GetService异常，如果是异步调用请改用GetServiceFromRoot {type.Name}  ReuqestID:{ReuqestID}   调用方法：{mb.Name} {fr.GetFileName()} {fr.GetFileLineNumber()}");
            }
            return item;
        }

        public object GetService(Type type)
        {
            object item = null;

            try
            {
                item = _serviceProvider.GetService(type);
            }
            catch
            {
                System.Diagnostics.StackFrame fr = new System.Diagnostics.StackFrame(1, true);
                MethodBase mb = fr.GetMethod();
                Lib.Log.WriteExceptionLog($"GetService异常，如果是异步调用请改用GetServiceFromRoot {type.Name}  ReuqestID:{ReuqestID}   调用方法：{mb.Name} {fr.GetFileName()} {fr.GetFileLineNumber()}");
            }
            return item;
        }

        public HttpContext HttpContext { get; set; }

        private ICookiesManager cookiesManager;
        public ICookiesManager Cookies
        {
            get
            {
                if (cookiesManager == null)
                {
                    cookiesManager = GetService<ICookiesManager>();
                }
                return cookiesManager;
            }
        }

        public IConfiguration Configuration
        {
            get
            {
                return ConfigManager.Configuration;
            }
        }

        public IRedisApiClient Cache
        {
            get
            {
                return Modobay.Cache.CacheManager.RedisCache;
            }
        }

        private const string Cookie_CartId = "cartId";  //cartId
        //private const string CartId_Key = "s_test";  //sessionid
        //private const string CartId_Key = "c_debug";  //cartId
        //private const string CartId_Key = "a_env";  //appid

        private string _appID = string.Empty;

        public string AppID
        {
            get
            {
                if (string.IsNullOrEmpty(_appID))
                {
                    var appid = HttpContext?.Request?.Headers["appid"];
                    if (string.IsNullOrEmpty(appid))
                    {
                        appid = Cookies?.GetCookies("appid");
                    }
                    _appID = ConfigManager.AppConfig.FirstOrDefault(x => x.Value.appId == appid).Key ?? string.Empty;

                    //// 开发调试环境特殊
                    //if (string.IsNullOrEmpty(_appID) && AppManager.IsDevelopment)
                    //{
                    //    _appID = "Api";
                    //}
                }
                return _appID;
            }
            set
            {
                _appID = value;
            }
        }

        private string _cartID;

        public string CartID
        {
            get
            {
                if (string.IsNullOrEmpty(_cartID))
                {
                    _cartID = Cookies.GetCookies(Cookie_CartId);
                }
                return _cartID;
            }
            set
            {
                _cartID = value;
                Cookies.SetCookies(Cookie_CartId, _cartID, 259200);// 180天=180*24*60分钟
            }
        }

        private string _sessionID;

        public string SessionID
        {
            get
            {
                if (string.IsNullOrEmpty(_sessionID))
                {
                    _sessionID = HttpContext?.Request?.Headers["sessionId"];
                    if (string.IsNullOrEmpty(_sessionID))
                    {
                        _sessionID = Cookies.GetCookies("sessionId");
                    }
                }
                return _sessionID;
            }
            set
            {
                _sessionID = value;
                Cookies.SetCookies("sessionId", _sessionID);
            }
        }

        private string _language;

        public string Language
        {
            get
            {
                if (!string.IsNullOrEmpty(_language)) return _language;
                if (AppID == "Customer" || string.IsNullOrEmpty(AppID))
                {
                    _language = Cookies.GetCookies("language");
                    if (string.IsNullOrEmpty(_language))
                    {
                        _language = HttpContext?.Request?.Headers["language_x"];
                    }
                    if (string.IsNullOrEmpty(_language))
                    {
                        _language = HttpContext?.Request?.Query["language_x"];
                    }
                }
                if (string.IsNullOrEmpty(_language) && !string.IsNullOrEmpty(AppID))
                {
                    _language = ConfigManager.AppConfig[AppID].languageDefault;
                }
                if (string.IsNullOrEmpty(_language))
                {
                    _language = "en";
                }
                return _language;
            }
            set
            {
                _language = value;
            }
        }

        public string ReuqestID { get; set; }


        public string Message { get; set; }

        public UserDto User { get; set; }

        private UserDto _guestUser;

        public UserDto GuestUser { get { return _guestUser; } set { _guestUser = value; } }

        public RequestInfoDto RequestInfo { get; set; } = new RequestInfoDto();
        
        public void LogRequest(string traceIdentifier, string actionName, string parm, string clientInfo, string routeName = "")
        {
            try
            {
                AppManager.CurrentAppContext.ReuqestID = traceIdentifier;
                ReuqestID = traceIdentifier;
                RequestInfo.RequestId = traceIdentifier;
                RequestInfo.ClientIpAddress = clientInfo;
                RequestInfo.StartTime = DateTime.Now;
                RequestInfo.SessionID = SessionID;
                RequestInfo.Language = Language;
                RequestInfo.AppLog += $"[service]{AppManager.ServiceAddress}";
                RequestInfo.RequestClassMethon = actionName;
                RequestInfo.RequestApi = routeName;
                RequestInfo.RequestParams = parm;

                if (User != null)
                {
                    RequestInfo.UserID = User.UserID;
                    //RequestInfo.Token = User.Token;
                }

                if (RequestInfoList.Count >= 100)
                {
                    RemoveRequestInfo(100);
                }

                lock (SyncObj)
                {
                    _requestInfoList.Add(RequestInfo);
                }
            }
            catch (Exception ex) { Lib.Log.WriteExceptionLog($"LogRequest:Message:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }
        }

        public IDictionary<string, object> ActionArguments { get; set; } = new Dictionary<string, object>();


        public void LogResponseResult(object resultValue)
        {
            var response = "响应结果，如果看到这个，说明序列化结果时报错";
            try
            {
                response = JsonConvert.SerializeObject(resultValue);
                RequestInfo.ResponseResult = response;
            }
            catch (Exception ex) { Lib.Log.WriteExceptionLog($"LogResponseResult:Message:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }
        }

        public void EndRequest()
        {
            RequestInfo.EndTime = DateTime.Now;
            if (string.IsNullOrEmpty(RequestInfo.RequestParams))
            {
                RequestInfo.RequestParams = JsonConvert.SerializeObject(ActionArguments);
            }

            if (AppManager.IsEnableApiLog)
            {
                AppManager.WriteLog(RequestInfo);
            }
        }

        public void RemoveRequestInfo(int count)
        {
            try
            {
                lock (SyncObj)
                {
                    count = count > _requestInfoList.Count ? _requestInfoList.Count : count;
                    _requestInfoList.RemoveRange(0, count);
                }
            }
            catch (Exception ex) { Lib.Log.WriteExceptionLog($"RemoveRequestInfo:Message:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }
        }

        public List<RequestInfoDto> GetAllEndRequestInfo()
        {
            List<RequestInfoDto> newList = null;

            try
            {
                lock (SyncObj)
                {
                    newList = _requestInfoList.Where(x => x.EndTime > DateTime.MinValue).OrderByDescending(x => x.StartTime).ToList();
                    _requestInfoList.RemoveAll(x => x.EndTime > DateTime.MinValue);
                }
            }
            catch (Exception ex) { Lib.Log.WriteExceptionLog($"GetAllEndRequestInfo:Message:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }

            return newList;
        }

        public List<RequestInfoDto> GetAllRequestInfo()
        {
            List<RequestInfoDto> newList = null;

            try
            {
                newList = _requestInfoList.Where(x => x.EndTime == DateTime.MinValue || DateTime.Now.AddMilliseconds(-1000) < x.EndTime ).OrderBy(x => x.StartTime).ToList();
            }
            catch (Exception ex) { Lib.Log.WriteExceptionLog($"GetAllRequestInfo:Message:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }

            return newList;
        }

        //public void PushSystemMessage(SystemMessageAttribute message)
        //{
        //    systemMessage.PushSystemMessage(message, User.UserID);
        //}

        public int AddUserBehavior(UserBehaviorAttribute userBehavior)
        {
            var service = GetService<IUserBehavior>();
            return service.AddUserBehavior(userBehavior, User);
        }

        #region 业务异常

        public T ThrowException<T>(string errorMessage) where T : notnull
        {
            ExceptionInfo = new AppException(errorMessage);
            return default(T);
        }

        public T ThrowException<T>(string errorMessage, string errorCode) where T : notnull
        {
            ExceptionInfo = new AppException(errorMessage, errorCode);
            return default(T);
        }

        public T ThrowException<T>(string errorMessage, AppExceptionType exceptionType) where T : notnull
        {
            ExceptionInfo = new AppException(errorMessage, exceptionType);
            return default(T);
        }
        
        public T ThrowException<T>(AppExceptionType exceptionType) where T : notnull
        {
            ExceptionInfo = new AppException(exceptionType);
            return default(T);
        }

        public AppException ExceptionInfo { get; set; }


        #endregion
    }
}
