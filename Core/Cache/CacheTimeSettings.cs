using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay.Cache
{
    public class CacheTimeSettings
    {
        public const string Second = "Second";
        public const string Minute = "Minute";
        public const string Hour = "Hour";
        public const string Day = "Day";
        public const string Week = "Week";
        internal const string Static = "Static";
        internal const string StaticReadOnly = "StaticReadOnly";
        public const int MinCacheSeconds = 10;
        public const int CacheMinute = 10;

        /// <summary>
        /// 缓存过期时间
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        internal static int GetCacheSeconds(string setting)
        {
            int seconds = MinCacheSeconds;
            switch (setting)
            {
                case Second:
                    seconds = 30;
                    break;
                case Minute:
                    seconds = CacheMinute * 60;
                    break;
                case Hour:
                    seconds = 60 * 60;
                    break;
                case Day:
                    seconds = 24 * 60 * 60;
                    break;
                case Week:
                    seconds = 7 * 24 * 60 * 60;
                    break;
                case Static:
                    seconds = 30 * 24 * 60 * 60;
                    break;
                case StaticReadOnly:
                    seconds = 180 * 24 * 60 * 60;
                    break;
            }
            return seconds;
        }

        /// <summary>
        /// 缓存更新时间
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        internal static int GetUpdateCacheSeconds(string setting)
        {
            int seconds = MinCacheSeconds;
            switch (setting)
            {
                case Second:
                    seconds = 0;
                    break;
                case Minute:
                    seconds = 25;
                    break;
                case Hour:
                    seconds = 50;
                    break;
                case Day:
                    seconds = 50;
                    break;
                case Week:
                    seconds = 50;
                    break;
                case Static:
                    seconds = 50;
                    break;;
                case StaticReadOnly:
                    seconds = 60 * 60;
                    break;
            }
            return seconds;
        }

        internal static CacheTimeRate GetCacheTimeRate()
        {
            var cacheMode = ConfigManager.Configuration[$"CacheMode"];
            if (string.IsNullOrEmpty(cacheMode)) return null;

            var cacheTimes = cacheMode.Split(',');
            var cacheTime = new Cache.CacheTimeRate();
            cacheTime.HttpCacheTime = double.Parse(cacheTimes[0]);
            cacheTime.AppCacheTime = double.Parse(cacheTimes[1]);
            cacheTime.RedisCacheTime = double.Parse(cacheTimes[2]);
            return cacheTime;
        }
    }
}
