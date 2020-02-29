using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Reflection;
using Modobay.Api;
using Microsoft.AspNetCore.Http;

namespace Modobay.Proxy
{
    public interface InvocationHandler
    {
        object InvokeMember(object obj, int rid, string name, params object[] args);

        T InvokeMember2<T, R>(object obj, int rid, string name, params object[] args);
    }

    public class FooInvocationHandler : InvocationHandler
    {
        /// <summary>
        /// 返回值为valueType
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="obj"></param>
        /// <param name="rid"></param>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public T InvokeMember2<T, R>(object obj, int rid, string name, params object[] args)
        {
            var tokenHeader = string.Empty;
            var appidHeader = string.Empty;
            var sessionidHeader = string.Empty;
            var requestId = string.Empty;
            var httpContext = AppManager.GetServiceFromRoot<IHttpContextAccessor>().HttpContext;
            if (httpContext != null)
            {
                tokenHeader = httpContext.Request.Headers["token"];
                if (string.IsNullOrEmpty(tokenHeader))
                {
                    tokenHeader = httpContext.Request.Query["token"];
                }
                appidHeader = httpContext.Request.Headers["appid"];
                sessionidHeader = httpContext.Request.Cookies["sessionid"];
                requestId = httpContext.Request.Headers["requestId"];
            }
            else
            {

            }
            PostParas(out var url, out var post, out var getParas, out var apiRoute, obj, rid, name, args);
            var client = new HttpClientHelper(url, tokenHeader, appidHeader, sessionidHeader, requestId);
            var result = client.Post2<T, object>(apiRoute, post, getParas);
            return result;
        }

        /// <summary>
        /// 返回值为objectType
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="rid"></param>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public object InvokeMember(object obj, int rid, string name, params object[] args)
        {
            var httpContext = AppManager.GetServiceFromRoot<IHttpContextAccessor>().HttpContext;
            var tokenHeader = httpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(tokenHeader))
            {
                tokenHeader = httpContext.Request.Query["token"];
            }
            var appidHeader = httpContext.Request.Headers["appid"];
            var sessionidHeader = httpContext.Request.Cookies["sessionid"];
            string requestId = httpContext.Request.Headers["requestId"];
            PostParas(out var url, out var post, out var getParas, out var apiRoute, obj, rid, name, args);
            var client = new HttpClientHelper(url, tokenHeader, appidHeader, sessionidHeader, requestId);
            var result = client.Post2<dynamic, Object>(apiRoute, post, getParas);
            return result;
        }

        void PostParas(out string url, out object post, out string getParas, out string apiRoute, object obj, int rid, string name, params object[] args)
        {
            var type = obj.GetType();
            var interfaceType = AppManager.GetInterfaces(type).FirstOrDefault(x => x.GetMethod(name) != null);
            url = AppManager.GetServiceUrl(interfaceType.FullName);
            var met = interfaceType.GetMethod(name);
            apiRoute = met.GetPath();
            var parameters = met.GetParameters();
            dynamic postParms = new ExpandoObject();
            var dic = (IDictionary<string, object>)postParms;
            post = null;

            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < parameters.Length; i++)
            {
                if (i >= args.Length) break;
                var parameterType = parameters[i].ParameterType;
                var parameterName = parameters[i].Name;

                // todo List<string> 识别待调整，需与前端配合
                var isStringList = parameterType.Equals(typeof(List<string>));
                if (isStringList)
                {
                    post = new object();
                }

                if (!(AppManager.IsValueType(parameterType)) && !isStringList)
                {
                    dic[parameterName] = args[i];
                }
                else if (AppManager.IsValueArrayType(parameterType) || isStringList)
                {
                    foreach (var item in args[i] as dynamic)
                    {
                        var para = string.Format("{0}={1}", parameterName, item?.ToString());
                        sb.Append((sb.Length > 0) ? ("&" + para) : ("?" + para));
                    }
                }
                else
                {
                    var para = string.Format("{0}={1}", parameterName, args[i]?.ToString());
                    sb.Append((sb.Length > 0) ? ("&" + para) : ("?" + para));
                }
            }

            getParas = sb.ToString();
            if (dic.Count == 1)
            {
                post = dic.ElementAt(0).Value;
            }
            else if (dic.Count > 1)
            {
                post = postParms;
            }
        }
    }
}
