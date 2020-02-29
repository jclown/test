using Modobay.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Modobay
{
    public interface IAppContext
    {
        object GetService(Type type);
        string AppID { get; set; }

        string CartID { get; set; }

        T GetService<T>();

        string SessionID { get; set; }

        string ReuqestID { get; set; }

        string Message { get; set; }

        UserDto User { get; set; }

        UserDto GuestUser { get; set; }

        string Language { get; set; }

        HttpContext HttpContext { get; set; }

        ICookiesManager Cookies { get; }

        IConfiguration Configuration { get; }

        IRedisApiClient Cache { get; }

        Api.RequestInfoDto RequestInfo { get; set; }

        void LogRequest(string traceIdentifier, string actionName, string parm, string clientInfo, string routeName = "");

        void LogResponseResult(object resultValue);
        void EndRequest();

        void RemoveRequestInfo(int count);

        List<RequestInfoDto> GetAllEndRequestInfo();

        List<RequestInfoDto> GetAllRequestInfo();

        IDictionary<string, object> ActionArguments { get; set; }

        //void PushSystemMessage(SystemMessageAttribute systemMessage);

        int AddUserBehavior(UserBehaviorAttribute userBehavior);

        IServiceProvider ServiceProvider { get; }

        T ThrowException<T>(string errorMessage) where T : notnull;
        T ThrowException<T>(string errorMessage, string errorCode) where T : notnull;

        T ThrowException<T>(string errorMessage, AppExceptionType exceptionType) where T : notnull;

        T ThrowException<T>(AppExceptionType exceptionType) where T : notnull;

        AppException ExceptionInfo { get; set; }

    }
}