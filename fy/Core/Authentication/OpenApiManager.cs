using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Modobay.Cache;
using Lib;

namespace Modobay.OpenApi
{
    public class OpenApiManager
    {
        public static string NewToken(string appid, string secret)
        {
            var appInfo = AppManager.GetAppInfo(appid, out var appInfoKey);
            if (appInfo == null) throw new AppException(AppExceptionType.AppNotFound);

            // 非明文：取secret的前6位和剩余的字符串分别MD5再拼接即可
            string secret2 = appInfo.secret;
            if (appInfo.secretCleartext != "1")
            {                
                secret2 = (secret2.Substring(0, 6).ToMd5() + secret2.Substring(6).ToMd5()).ToUpper();
            }
            if (secret != secret2) throw new AppException(AppExceptionType.AppNotFound);

            // 生成新accessToken
            var accessToken = Guid.NewGuid().ToString().Replace("-", "");
            var accessTokenCacheKey = GetCacheKey("AccessToken", accessToken);

            // 批量操作：写入新accessToken            
            var seconds = int.Parse(ConfigManager.Configuration["OpenApi:AccessTokenExpirySeconds"]);
            var cacheTime = TimeSpan.FromSeconds(seconds);
            var tran = CacheManager.RedisCache.CreateTransaction();
            CacheManager.RedisCache.Set(accessTokenCacheKey, appid, cacheTime);

            // 删除旧accessToken，记录最新的accessToken
            var appTokenCacheKey = GetCacheKey(appInfoKey, "AccessToken");
            var oldAccessToken = CacheManager.RedisCache.Get(appTokenCacheKey);
            if (!string.IsNullOrEmpty(oldAccessToken))
            {
                var oldAccessTokenCacheKey = GetCacheKey("AccessToken", oldAccessToken);
                CacheManager.RedisCache.Remove(oldAccessTokenCacheKey);
            }
            CacheManager.RedisCache.Set(appTokenCacheKey, accessToken, cacheTime);

            // 记录获取accessToken的次数
            var appRequestCountCacheKey = GetCacheKey(appInfoKey, "RequestCount");
            CacheManager.RedisCache.StringIncrement(appRequestCountCacheKey);
            CacheManager.RedisCache.KeyExpire(appRequestCountCacheKey, DateTime.Now.GetTodayRemain());
            bool committed = tran.Execute();

            // 本地缓存：仅缓存accessToken，1分钟
            Modobay.Cache.CacheManager.AppCache.Set(accessTokenCacheKey, appid, TimeSpan.FromSeconds(60));

            return accessToken;
        }
        
        internal static dynamic GetOpenApiInfo(ActionExecutingContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor == null) return null;
            object[] controllerNonToken, actionNonToken;

            if (controllerActionDescriptor.ControllerTypeInfo.Name.EndsWith("Proxy"))
            {
                // 代理类从源类型查找特性
                var controllerType = AppManager.ProxyOriginalTypes[controllerActionDescriptor.ControllerTypeInfo.FullName];
                var methodInfo = controllerType.GetMethod(controllerActionDescriptor.MethodInfo.Name);
                controllerNonToken = controllerType.GetCustomAttributes(typeof(Modobay.Api.OpenApiAttribute), true);
                actionNonToken = methodInfo.GetCustomAttributes(typeof(Modobay.Api.OpenApiAttribute), true);
            }
            else
            {
                controllerNonToken = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes(typeof(Modobay.Api.OpenApiAttribute), true);
                actionNonToken = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(Modobay.Api.OpenApiAttribute), true);
            }

            if (actionNonToken.Count() == 1) return actionNonToken[0];
            if (controllerNonToken.Count() == 1) return controllerNonToken[0];
            return null;
        }

        public static dynamic CheckOpenApiPermission(string accessToken, dynamic openApiInfo,IAppContext app)
        {
            if (string.IsNullOrEmpty(accessToken)) throw new AppException(AppExceptionType.AppTokenInvalid);

            // 从缓存检查accessToken，换取appid
            var accessTokenCacheKey = GetCacheKey("AccessToken", accessToken);
            var appid = Cache.CacheManager.Get<string>(accessTokenCacheKey);
            if (string.IsNullOrEmpty(appid)) throw new AppException(AppExceptionType.AppTokenInvalid);

            var appInfo = AppManager.GetAppInfo(appid);
            if (appInfo == null) throw new AppException(AppExceptionType.AppNotFound);

            // 根据openApiInfo检查签名
            if (openApiInfo.IsSign == true)
            {
                var paraStr = app.HttpContext.Request.Query.OrderBy(o => o.Key).Select(x => x.Key.ToUpper() + "=" + x.Value).ToList().JoinAsString("&");
                var timestamp = app.HttpContext.Request.Headers["timestamp"];
                var sign = app.HttpContext.Request.Headers["sign"];
                var encrytedStr = $"{paraStr}{appid}{appInfo.secret}".ToMd5().ToUpper();
                if (sign != encrytedStr) throw new AppException(AppExceptionType.AppSignInvalid);
            }

            return appInfo;
        }

        private static string GetCacheKey(string category, string accessToken)
        {
            return $"{nameof(OpenApiManager)}:{category}:{accessToken}";
        }
    }
}
