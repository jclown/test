using Modobay.Cache;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Modobay.Gateway
{
    public class GatewayManager
    {
        public static void UpdateClientRole(string clientIp)
        {
            var userRoleCacheKey = GetUserRoleCacheKey(clientIp);
            var userRole = AppManager.CurrentAppContext.AppID;
            CacheManager.Set(userRoleCacheKey, userRole, DateTime.Now.GetTodayRemain());
        }

        private static string GetUserRoleCacheKey(string clientIp)
        {
            return $"{nameof(GatewayManager)}:{clientIp}:UserRole";
        }

        internal static void Check(string clientIp)
        {
            //// 未开启检查，直接跳过
            //if (ConfigManager.Configuration["Gateway:EnableGateway"] != "1") return;

            //// 黑名单，直接禁止
            //var dayCountCacheKey = $"{nameof(GatewayManager)}:{clientIp}:DayCount";
            //var blackList = ConfigManager.Configuration["Gateway:BlackList"];
            //if (!string.IsNullOrEmpty(blackList))
            //{
            //    var regex = Lib.Text.RegexUtility.CreateRegex(@"(?<=\d\.)\d{1,3}\.\d{1,3}(?!\d|\.)");
            //    var clientIp2 = regex.Replace(clientIp, "*.*");
            //    if (blackList.Contains(clientIp) || blackList.Contains(clientIp2))
            //    {
            //        Lib.Log.WriteSecurityLog($"黑名单拒绝访问：{clientIp}");
            //        throw new AppException($"{clientIp}", AppExceptionType.AccessDenied);
            //    }
            //}

            //// 白名单，直接跳过
            //var whiteList = ConfigManager.Configuration["Gateway:WhiteList"];
            //if (!string.IsNullOrEmpty(whiteList) && whiteList.Contains(clientIp)) return;

            //// 频访太频繁限制规则：每分30
            //var minuteCountCacheKey = $"{nameof(GatewayManager)}:{clientIp}:MinuteCount";
            //var minuteCount = CacheManager.Get<double?>(minuteCountCacheKey);
            //var minuteRequestCountLimit = int.Parse(ConfigManager.Configuration["Gateway:MinuteRequestCountLimit"]);
            //if (minuteCount >= minuteRequestCountLimit)
            //{
            //    UpdateCount(minuteCountCacheKey, TimeSpan.FromMinutes(1));
            //    Lib.Log.WriteSecurityLog($"访问太频繁：{clientIp}  minuteCount:{minuteCount}");
            //    throw new AppException($"Please try again later.", AppExceptionType.TimeLimitAccessDenied);
            //}
            //UpdateCount(minuteCountCacheKey, TimeSpan.FromMinutes(1));

            //// 每天访问总次数限制规则
            //var requestCountLimit = int.Parse(ConfigManager.Configuration["Gateway:RequestCountLimit"]);
            //var dayCount = CacheManager.Get<double?>(dayCountCacheKey);
            //if (dayCount >= requestCountLimit)
            //{
            //    var userRoleCacheKey = GetUserRoleCacheKey(clientIp);
            //    var userRole = CacheManager.Get<string>(userRoleCacheKey) ?? string.Empty;
            //    if (string.IsNullOrEmpty(userRole)) throw new AppException($"Please log in to the website.", AppExceptionType.AccessDenied);

            //    var userRoleRequestCountLimit = ConfigManager.Configuration[$"Gateway:{userRole}RequestCountLimit"];
            //    if (userRoleRequestCountLimit == null)
            //    {
            //        userRoleRequestCountLimit = ConfigManager.Configuration[$"Gateway:LoginRequestCountLimit"];
            //    }
            //    if (dayCount > int.Parse(userRoleRequestCountLimit))
            //    {
            //        Lib.Log.WriteSecurityLog($"访问太频繁：{clientIp}  dayCount:{dayCount}");
            //        throw new AppException($"Please contact the customer service staff.", AppExceptionType.AccessDenied);
            //    }
            //}

            //UpdateCount(dayCountCacheKey);
        }

        private static void UpdateCount(string cacheKey, TimeSpan? cacheTime = null)
        {
            Task.Run(async () =>
            {
                await Task.Delay(1);
                var tran = Modobay.Cache.CacheManager.RedisCache.CreateTransaction();
                var isKeyExists = Modobay.Cache.CacheManager.RedisCache.KeyExists(cacheKey);
                var newCount = Modobay.Cache.CacheManager.RedisCache.StringIncrement(cacheKey, 1);
                if (!isKeyExists)
                {
                    cacheTime = cacheTime ?? DateTime.Now.GetTodayRemain();
                    Modobay.Cache.CacheManager.RedisCache.KeyExpire(cacheKey, cacheTime);
                    CacheManager.AppCache.Set(cacheKey, newCount, cacheTime.Value);
                }
                else
                {
                    CacheManager.AppCache.Set(cacheKey, newCount);
                }
                tran.Execute();
                
            });
        }
    }
}
