using Modobay.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Modobay.Cache
{
    public static class CacheManager
    {
        private static readonly IMemoryCache _appCache;
        private static readonly IRedisApiClient _redisApiClient;
        private static string cacheDirectory = string.Empty;
        private static string cacheKeyListFileName = string.Empty;
        private static ReaderWriterLockSlim _cacheKeyListFileWriteLock = new ReaderWriterLockSlim();
        private static Dictionary<string, string> cacheKeyList = new Dictionary<string, string>();

        #region 初始化缓存

        static CacheManager()
        {
            cacheDirectory = Lib.IOHelper.GetFileFullPath("Cache");
            cacheKeyListFileName = Lib.IOHelper.GetFileFullPath(cacheDirectory + @"\" + "cacheKeyList");

            _appCache = AppManager.GetServiceFromRoot<IMemoryCache>();
            _redisApiClient = AppManager.GetServiceFromRoot<IRedisApiClient>();

            if (!System.IO.Directory.Exists(cacheDirectory))
            {
                System.IO.Directory.CreateDirectory(cacheDirectory);
            }
            if (!System.IO.File.Exists(cacheKeyListFileName))
            {
                File.WriteAllText(cacheKeyListFileName, string.Empty);
            }
            else
            {
                var cacheItems = File.ReadAllLines(cacheKeyListFileName);
                foreach (var cacheItem in cacheItems)
                {
                    var cacheInfo = cacheItem.Split('|');
                    cacheKeyList.Add(cacheInfo[0], cacheInfo[1]);
                }
            }
        }

        //public static void ClearDataCacheKey()
        //{
        //    var ipPort = ConfigManager.Configuration["Redis:ConnectionString"].Replace("{DataServerIP}", Modobay.AppManager.DataServerIP).Split(',')[0];
        //    var RedisCache = Modobay.Cache.CacheManager.RedisCache;
        //    var keyPrefix = $"DataCacheInvokeTask:{AppManager.ServiceAddress.Replace(':', '-')}*";
        //    var keys = RedisCache.GetServer(ipPort).Keys(0, keyPrefix, int.MaxValue);
        //    foreach (var key in keys)
        //    {
        //        RedisCache.Remove(key);
        //    }
        //}

        public static void LoadStaticCache()
        {
            var cacheKey = string.Empty;

            try
            {
                var files = Directory.GetFiles(cacheDirectory);
                foreach (var file in files)
                {
                    if (file == cacheKeyListFileName) continue;
                    if (!File.Exists(file)) continue;

                    cacheKey = (new FileInfo(file)).Name.Replace('_', ':');
                    if (!cacheKeyList.ContainsKey(cacheKey)) continue;

                    // 缓存的dto必须包含在dto之中，或netcore自带的类型
                    var typeName = cacheKeyList[cacheKey];
                    var type = Type.GetType(typeName);
                    if (type == null)
                    {
                        var asmb = Assembly.Load("Dto");
                        type = asmb.GetType(typeName);
                        if (type == null) continue; ;
                    }

                    var cacheResult = JsonConvert.DeserializeObject(File.ReadAllText(file), type);
                    AppCache.Set(cacheKey, cacheResult);
                }
            }
            catch (Exception ex) { Lib.Log.WriteExceptionLog($"CacheManager.LoadStaticCache:{cacheKey}:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }
        }

        /// <summary>
        /// 获取所有缓存键
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCacheKeys()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var entries = AppCache.GetType().GetField("_entries", flags).GetValue(AppCache);
            var cacheItems = entries as IDictionary;
            var keys = new List<string>();
            if (cacheItems == null) return keys;
            foreach (DictionaryEntry cacheItem in cacheItems)
            {
                keys.Add(cacheItem.Key.ToString());
            }
            return keys;
        }

        #endregion

        #region 缓存Key

        /// <summary>
        /// 缓存key，适用于不区分公司和用户的
        /// </summary>
        /// <param name="module">模块名称 或 类名称</param>
        /// <param name="scene">场景名称 或 方法/接口名称 </param>
        /// <param name="flag">其他标识，可以多个</param>
        /// <returns></returns>
        public static string Key(string module, string scene, params object[] flag)
        {
            return $"{module}:{scene}:{string.Join("_", flag)}";
        }

        /// <summary>
        /// 缓存key，适用于要区分公司及用户的
        /// </summary>
        /// <param name="appContext">请通过app.UserKey调用此方法</param>
        /// <param name="module">模块名称 或 类名称</param>
        /// <param name="scene">场景名称 或 方法/接口名称 </param>
        /// <param name="dateStartFlag">可选，开始日期标识，不用日期隔离的无需此参数。建议采用yyyyMMdd...格式</param>
        /// <param name="dateEndFlag">可选，结束日期标识，不用结束日期的无需此参数。建议采用yyyyMMdd...格式</param>
        /// <returns></returns>
        public static string UserKey(this IAppContext appContext, string module, string scene, string dateStartFlag = "", string dateEndFlag = "")
        {
            return Key(module, scene, appContext?.User?.ShopId, appContext?.User?.UserID, dateStartFlag, dateEndFlag);
        }

        /// <summary>
        /// 缓存key，适用于要区分公司，不区分用户的
        /// </summary>
        /// <param name="appContext">请通过app.ShopKey调用此方法</param>
        /// <param name="module">模块名称 或 类名称</param>
        /// <param name="scene">场景名称 或 方法/接口名称 </param>
        /// <param name="dateStartFlag">可选，开始日期标识，不用日期隔离的无需此参数。建议采用yyyyMMdd...格式</param>
        /// <param name="dateEndFlag">可选，结束日期标识，不用结束日期的无需此参数。建议采用yyyyMMdd...格式</param>
        /// <returns></returns>
        public static string ShopKey(this IAppContext appContext, string module, string scene, string dateStartFlag = "", string dateEndFlag = "")
        {
            return Key(module, scene, appContext?.User?.ShopId, dateStartFlag, dateEndFlag);
        }

        #endregion

        #region 基础缓存管理

        /// <summary>
        /// 应用缓存
        /// </summary>
        public static IMemoryCache AppCache { get { return _appCache; } }

        /// <summary>
        /// 分布式缓存
        /// </summary>
        public static IRedisApiClient RedisCache { get { return _redisApiClient; } }
             

        public static bool KeyExists(string key)
        {
            return RedisCache.KeyExists(key);
        }

        public static void Remove(string key)
        {
            RedisCache.Remove(key);
            AppCache.Remove(key);
        }

        /// <summary>
        /// 自动缓存到appCache的时间
        /// </summary>
        private const int AutoAppCacheSeconds = 60;

        public static dynamic Get<T>(string key, double restoreAppCahcheSeconds = AutoAppCacheSeconds)
        {
            AppCache.TryGetValue<T>(key, out var value);

            if (value == null)
            {
                value = RedisCache.Get2<T>(key);

                // 恢复本地缓存 todo 调整时间为1分钟？
                if (restoreAppCahcheSeconds != 0)
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(1);
                        if (restoreAppCahcheSeconds == -1)
                        {
                            var remainTime = RedisCache.KeyTimeToLive(key);
                            if (remainTime != null)
                            {
                                restoreAppCahcheSeconds = remainTime.Value.TotalSeconds;
                            }
                        }
                        AppCache.Set<T>(key, value, TimeSpan.FromSeconds(restoreAppCahcheSeconds));
                    });
                }
            }
            return value;
        }

        public static void Set<T>(string key, T value, TimeSpan timeSpan, bool async = true, bool isCacheFile = false)
        {
            AppCache.Set(key, value, TimeSpan.FromSeconds(AutoAppCacheSeconds));

            if (async)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(1);
                    if (isCacheFile)
                    {
                        var cacheResultJson = ConvertJson(value);
                        var cacheFileName = CacheFile(key, cacheResultJson, typeof(T).FullName);
                    }
                    else
                    {
                        RedisCache.Set(key, value, timeSpan);
                        //RedisCache.Set3(key, value, timeSpan);
                    }
                });
            }
            else
            {
                if (isCacheFile)
                {
                    var cacheResultJson = ConvertJson(value);
                    var cacheFileName = CacheFile(key, cacheResultJson, typeof(T).FullName);
                }
                else
                {
                    RedisCache.Set(key, value, timeSpan);
                    //RedisCache.Set3(key, value, timeSpan);
                }
            }
        }

        public static void Remove(string key, bool async = true)
        {
            AppCache.Remove(key);

            if (async)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(1);
                    RedisCache.Remove(key);
                });
            }
            else
            {
                RedisCache.Remove(key);
            }
        }

        #endregion

        #region 数据缓存管理

        private static string GetDataCacheKey<T>(Delegate func, string flag = "")
        {
            var regex = new Regex("<(.*?)>");
            var method = regex.Match(func.Method.Name)?.Value.Replace("<", "").Replace(">", "");
            var type = func.Target.GetType();
            var cacheKey = $"{type.ReflectedType?.Name ?? type.Name}:{method}:{flag}";
            return cacheKey;
        }

        private static string GetCacheFileName(string cacheKey)
        {
            return Lib.IOHelper.GetFileFullPath(cacheDirectory + @"\" + cacheKey.Replace(':', '_')); ;
        }
        
        private static string CacheFile(string cacheKey, string cacheResultJson, string typeName)
        {
            var cacheFileName = GetCacheFileName(cacheKey);
            _cacheKeyListFileWriteLock.EnterWriteLock();

            try
            {
                // 比较内容是否有更新
                if (File.Exists(cacheFileName))
                {
                    var old = File.ReadAllText(cacheFileName);
                    if (old == cacheResultJson) return cacheFileName;
                }

                // 注意：同一个api文件夹运行多个实例可能存在冲突
                File.WriteAllText(cacheFileName, cacheResultJson);

                List<string> lines = null;
                lock (cacheKeyList)
                {
                    cacheKeyList[cacheKey] = typeName;
                    lines = cacheKeyList.Select(x => $"{x.Key}|{x.Value}").ToList();
                }
                
                File.WriteAllLines(cacheKeyListFileName, lines ?? new List<string>());
                Lib.Log.WriteOperationLog($"CacheFile:{cacheKey}");
            }
            catch (Exception ex)
            {
                Lib.Log.WriteExceptionLog($"CacheFile:{cacheKey} {ex.Message} {ex.StackTrace}");
            }
            finally
            {
                _cacheKeyListFileWriteLock.ExitWriteLock();
            }

            return cacheFileName;
        }
        
        private static string ConvertJson<T>(T value)
        {
            string result = value is string ? value.ToString() : JsonConvert.SerializeObject(value);
            return result;
        }

        private static bool IsStatic(string cacheTimeSetting)
        {
            if (cacheTimeSetting == CacheTimeSettings.Static || cacheTimeSetting == CacheTimeSettings.StaticReadOnly)
            {
                return true;
            }
            return false;
        }

        private static T GetAndCache<T>(Delegate func, string cacheTimeSetting, string flag = "", params object[] input)
        {
            var cacheResult = default(T);
            var cacheKey = GetDataCacheKey<T>(func, flag);
            var cacheTime = TimeSpan.FromSeconds(CacheTimeSettings.GetCacheSeconds(cacheTimeSetting));

            // 从本地缓存返回
            AppCache.TryGetValue(cacheKey, out cacheResult);

            // 从本地文件返回
            if (cacheResult == null && IsStatic(cacheTimeSetting))
            {
                var cacheFileName = GetCacheFileName(cacheKey);
                if (File.Exists(cacheFileName))
                {
                    cacheResult = JsonConvert.DeserializeObject<T>(File.ReadAllText(cacheFileName));                    
                    AppCache.Set(cacheKey, cacheResult, cacheTime);// 恢复本地缓存
                }
            }

            // 从redis缓存返回
            if (cacheResult == null && !IsStatic(cacheTimeSetting))
            {
                cacheResult = RedisCache.Get<T>(cacheKey);
                if (cacheResult != null)
                {
                    AppCache.Set(cacheKey, cacheResult, cacheTime);// 恢复本地缓存
                    //if (IsStatic(cacheTimeSetting))
                    //{
                    //    CacheFile(cacheKey, JsonConvert.SerializeObject(cacheResult), typeof(T).FullName);
                    //}
                }
            }

            // 从数据源返回
            if (cacheResult == null)
            {
                // 如果其他线程已在获取，则等待，超时再重新获取
                var isNeedUpdate = CacheManager.CheckUpdateAndFlag(cacheKey, cacheTimeSetting);//, "UpdateCache:Data"
                if (!isNeedUpdate)
                {
                    var index = 0;
                    while (cacheResult == null && index < 30)
                    {
                        index += 1;
                        AppCache.TryGetValue<T>(cacheKey, out cacheResult);
                        if (cacheResult == null) cacheResult = RedisCache.Get<T>(cacheKey);
                        if (cacheResult == null) System.Threading.Thread.Sleep(100);
                        Lib.StopwatchLog.RecordElapsedMilliseconds($"1 {cacheKey}");
                    }
                    if (cacheResult != null) return cacheResult;
                }

                cacheResult = (T)func.DynamicInvoke(input);
                Set(cacheKey, cacheResult, cacheTime, true, IsStatic(cacheTimeSetting));      
                return cacheResult;
            }

            if (cacheTimeSetting != CacheTimeSettings.Second && CacheManager.CheckUpdateAndFlag(cacheKey, cacheTimeSetting) == true)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(1);
                    cacheResult = (T)func.DynamicInvoke(input);
                    Set(cacheKey, cacheResult, cacheTime, false, IsStatic(cacheTimeSetting));
                });
            }            

            // 缓存预热，每隔10分钟，先异步更新缓存，并续期，待下次访问时替换为最新缓存
            return cacheResult;
        }

        public static T CacheWeek<T>(Delegate func, string flag = "", params object[] input)
        {
            return GetAndCache<T>(func, CacheTimeSettings.Week, flag, input);
        }

        public static T CacheStatic<T>(Delegate func, string flag = "", params object[] input)
        {
            return GetAndCache<T>(func, CacheTimeSettings.Static, flag, input);
        }

        public static T CacheStaticReadOnly<T>(Delegate func, string flag = "", params object[] input)
        {
            return GetAndCache<T>(func, CacheTimeSettings.StaticReadOnly, flag, input);
        }

        #endregion

        #region 接口缓存管理

        private static bool IsEnableApiCache(string appid)
        {
            var enableApiCache = ConfigManager.AppConfig[appid]?.enableApiCache;
            if (enableApiCache != null && enableApiCache == "1") return true;
            return false;
        }

        private static string GetApiCacheKey(ControllerActionDescriptor actionDescriptor, HttpContext httpContext)
        {
            var cacheKey = $"ApiCache:{actionDescriptor.AttributeRouteInfo.Template}:";            
            if (!httpContext.Request.QueryString.HasValue) return cacheKey;
            var newQueryString = httpContext.Request.Query.OrderBy(o => o.Key).Select(x => x.Key.ToUpper() + "=" + x.Value).ToList().JoinAsString("&");
            return cacheKey + newQueryString;
        }
        
        internal static int CalcCacheSeconds(ControllerActionDescriptor controllerActionDescriptor, out string cacheTimeSetting)
        {
            object[] responseCache;
            if (controllerActionDescriptor.ControllerTypeInfo.Name.EndsWith("Proxy"))
            {
                // 代理类，从源类型中查找相关特性
                var sourceControllerType = AppManager.ProxyOriginalTypes[controllerActionDescriptor.ControllerTypeInfo.FullName];
                var methodInfo = sourceControllerType.GetMethod(controllerActionDescriptor.MethodInfo.Name);
                responseCache = methodInfo.GetCustomAttributes(typeof(CacheAttribute), true);
            }
            else
            {
                responseCache = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(CacheAttribute), true);
            }
            if (responseCache.Count() != 1)
            {
                cacheTimeSetting = string.Empty;
                return 0;
            }

            var cache = responseCache[0] as CacheAttribute;
            cacheTimeSetting = cache.CacheProfileName;
            return CacheTimeSettings.GetCacheSeconds(cache.CacheProfileName);
        }

        internal static void WriteCache(object resultValue, ActionExecutedContext context,string appid)
        {
            if (!IsEnableApiCache(appid)) return;
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var cacheSeconds = CalcCacheSeconds(actionDescriptor, out var cacheTimeSetting);
            if (cacheSeconds <= 0) return;
            var cacheKey = GetApiCacheKey(actionDescriptor, context.HttpContext);
            var isNeedUpdate = CheckUpdateAndFlag(cacheKey, cacheTimeSetting);
            if (!isNeedUpdate) return;

            var cacheTimeRate = WriteCacheByCacheTimeSetting(resultValue, cacheKey, cacheSeconds, cacheTimeSetting);

            // 客户端（浏览器）缓存
            if (cacheTimeRate?.HttpCacheTime > 0)
            {
                var seconds = cacheTimeRate.HttpCacheTime * cacheSeconds;
                if (seconds < 10) seconds = 10;
                if (seconds > 60) seconds = 60;
                context.HttpContext.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                {
                    Public = true,
                    MaxAge = TimeSpan.FromSeconds(seconds)
                };
            }
        }

        private static CacheTimeRate WriteCacheByCacheTimeSetting(object resultValue, string cacheKey, double cacheSeconds, string cacheTimeSetting)
        {
            CacheTimeRate cacheTimeRate = null;

            try
            {                
                if (cacheSeconds <= 0) return null;                
                cacheTimeRate = CacheTimeSettings.GetCacheTimeRate();
                if (cacheTimeRate == null || (cacheTimeRate.AppCacheTime == 0 && cacheTimeRate.RedisCacheTime == 0)) return null;

                // 对不需要封装的接口，无需封装
                if (!resultValue.GetType().ToString().StartsWith("Modobay.Api.ResultDto"))
                {
                    resultValue = ResultBuilder.AsSuccess(resultValue);
                }

                // 本地缓存
                if (cacheTimeRate.AppCacheTime > 0 || IsStatic(cacheTimeSetting))
                {
                    var seconds = cacheTimeRate.AppCacheTime * cacheSeconds;
                    if (seconds < 60) seconds = 60;
                    if (seconds > 600) seconds = 600;
                    AppCache.Set(cacheKey, resultValue, TimeSpan.FromSeconds(seconds));
                    //AppCache.Set(cacheKey, resultValue, TimeSpan.FromSeconds(cacheTimeRate.AppCacheTime * cacheSeconds));
                }

                // 分布式缓存     
                if (cacheTimeRate.RedisCacheTime > 0 && !IsStatic(cacheTimeSetting)) //if (cacheTimeRate.RedisCacheTime > 0 && !RedisCache.KeyExists(cacheKey))
                {
                    RedisCache.Set2(cacheKey, resultValue, TimeSpan.FromSeconds(cacheTimeRate.RedisCacheTime * cacheSeconds));
                }                

                // 静态模式更新文件?
            }
            catch (Exception ex) { Lib.Log.WriteExceptionLog($"WriteCache Exception：{cacheKey}  {ex.Message}  <br> StackTrace:{ex.StackTrace}"); }
            return cacheTimeRate;
        }
        
            /// <summary>
            /// 检查是否可以更新缓存，如果可以，设置正在更新的标识
            /// </summary>
            /// <param name="cacheKey"></param>
            /// <returns></returns>
        public static bool CheckUpdateAndFlag(string cacheKey,string cacheTimeSetting = "", string cacheKeyPrefix = null)
        {
            var updateKey = $"{(cacheKeyPrefix ?? "UpdateCache")}:{cacheKey}";
            var updateFlag = Get<object>(updateKey);
            var isNeedUpdate = false;
            if (updateFlag == null)
            {
                double seconds = CacheTimeSettings.GetUpdateCacheSeconds(cacheTimeSetting);
                if (seconds > 0)
                {
                    Set(updateKey, new object(), TimeSpan.FromSeconds(seconds));
                    isNeedUpdate = true;
                }
            }
            return isNeedUpdate;
        }

        internal static ResultDto<dynamic> ReadCache(ActionExecutingContext context,IAppContext appContext)
        {
            try
            {
                if (!IsEnableApiCache(appContext.AppID)) return null;
                var cacheTimeRate = CacheTimeSettings.GetCacheTimeRate();
                if (cacheTimeRate == null || (cacheTimeRate.AppCacheTime == 0 && cacheTimeRate.RedisCacheTime ==0)) return null;
                var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
                var cacheSeconds = CalcCacheSeconds(actionDescriptor, out var cacheTimeSetting);
                if (cacheSeconds <= 0) return null;
                ResultDto<dynamic> cacheResult = null;
                var cacheKey = GetApiCacheKey(actionDescriptor, context.HttpContext);
                
                try
                {
                    if (cacheTimeRate.AppCacheTime > 0)
                    {
                        AppCache.TryGetValue<ResultDto<dynamic>>(cacheKey,out cacheResult);
                    }
                    if (cacheResult == null && cacheTimeRate.RedisCacheTime > 0)
                    {
                        cacheResult = RedisCache.Get2<ResultDto<dynamic>>(cacheKey);
                        
                        if (cacheResult != null && cacheTimeRate.AppCacheTime > 0)
                        {
                            // 恢复本地缓存
                            var seconds = cacheTimeRate.AppCacheTime * cacheSeconds;
                            if (seconds < 60) seconds = 60;
                            if (seconds > 600) seconds = 300;
                            AppCache.Set(cacheKey, cacheResult, TimeSpan.FromSeconds(seconds));
                            //AppCache.Set(cacheKey, cacheResult, TimeSpan.FromSeconds(cacheTimeRate.AppCacheTime * cacheSeconds));
                        }
                    }

                    // 存在缓存则检查缓存是否需要更新；如果cacheResult为null，则接口调用完成后由WriteCache写入缓存
                    if (cacheResult != null) 
                    {
                        var isNeedUpdate = CheckUpdateAndFlag(cacheKey, cacheTimeSetting);
                        if (isNeedUpdate)
                        {
                            Task.Run(async () =>
                            {
                                var newAppContext = AppManager.CopyAppContext(appContext);
                                await Task.Delay(1);
                                var resultValue = InvokeByActionContext(context, newAppContext);
                                if (resultValue != null)
                                {
                                    WriteCacheByCacheTimeSetting(resultValue, cacheKey, cacheSeconds, cacheTimeSetting);
                                }                                
                            });
                        }
                    }

                    return cacheResult;
                }
                catch (Exception ex)
                {
                    Lib.Log.WriteExceptionLog($"ReadCache-{cacheKey} Exception：{cacheKey}  {ex.Message}  <br> StackTrace:{ex.StackTrace}");
                    return null;
                }
            }
            catch (Exception exx)
            {
                return null;
            }
        }

        #endregion

        /// <summary>
        /// 获取变量的默认值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        private static object InvokeByActionContext(ActionExecutingContext context, IAppContext appContext)
        {
            var actionDesciptor = (context.ActionDescriptor as ControllerActionDescriptor);
            var methodName = actionDesciptor.MethodInfo.Name;
            var typ = context.Controller.GetType();
            return Invoke(typ, methodName, appContext.ActionArguments,appContext);
        }
        
        public static object Invoke(Type type, string methodName, IDictionary<string, object> actionArguments, IAppContext appContext = null)
        {
            var interfaceType = AppManager.GetInterfaces(type).FirstOrDefault(x => x.GetMethod(methodName) != null);
            var obj = AppManager.GetServiceFromRoot(interfaceType, appContext);
            var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            var parameters = method.GetParameters();
            var paraValues = new Dictionary<string, object>();

            foreach (var para in parameters)
            {
                if (actionArguments.ContainsKey(para.Name))
                {
                    paraValues[para.Name] = actionArguments[para.Name];
                }
                else
                {
                    paraValues[para.Name] = GetDefaultValue(para.ParameterType);
                }
            }

            var input = paraValues.Select(x => x.Value).ToArray();
            var data = method.Invoke(obj, input);
            obj = null;
            return data;
        }
    }
}
