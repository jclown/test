//using Microsoft.Extensions.Caching.Memory;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Lib.Cache
//{
//    public class RunTimeCache
//    {
//        public IMemoryCache _cache;
//        public RunTimeCache()
//        {
//            _cache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
//        }
//        public  object Get(string key)
//        {
//            return _cache.Get(key);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        /// <param name="timeOut">timeOut(秒)</param>
//        /// <param name="timeOutType"></param>
//        public  void Set(string key, object value, int timeOut, TimeOutType timeOutType)
//        {
//            if (timeOutType == TimeOutType.Absolute)
//                SetAbsoluteExpiration(key, value, timeOut);
//            else
//                SetSlidingExpiration(key, value, timeOut);
//        }

//        /// <summary>
//        /// 清除缓存
//        /// </summary>
//        /// <param name="key"></param>
//        public  void RemoveCache(string key)
//        {
//            _cache.Remove(key);
//        }

//        /// <summary>
//        /// 绝对过期
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        public void SetSlidingExpiration(string key, object value, double timeOut)
//        {
//            _cache.Set(key, value, new MemoryCacheEntryOptions()
//                     .SetSlidingExpiration(TimeSpan.FromSeconds(timeOut)));
//        }
//        /// <summary>
//        /// 相对过期 timeOut(秒)
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        public  void SetAbsoluteExpiration(string key, object value, double timeOut)
//        {
//            _cache.Set(key, value, new MemoryCacheEntryOptions()
//                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(timeOut)));
//        }
//    }
//    public enum TimeOutType
//    {
//        /// <summary>
//        /// 绝对过期
//        /// </summary>
//        Sliding,
//        /// <summary>
//        /// 相对过期
//        /// </summary>
//        Absolute
//    }
//}
