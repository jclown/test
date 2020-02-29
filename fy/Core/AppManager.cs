using Lib.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Modobay.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Modobay
{
    public static class AppManager
    {
        private static List<Type> _apiInterfaceTypes = new List<Type>();
        internal static List<Type> _apiImplementTypes = new List<Type>();

        [ThreadStatic]
        internal static AppException _proxyAppException;

        public static IAppContext CurrentAppContext
        {
            get
            {
                //if (_currentAppContext == null) _currentAppContext = GetServiceFromRoot<IAppContext>();
                return _currentAppContext;
            }
            internal set
            {
                _currentAppContext = value;
            }
        }

        [ThreadStatic]
        private static IAppContext _currentAppContext;
        
        private static IServiceProvider _rootServiceProvider;

        //public static bool IsDevelopment { get; set; }
        
        public static dynamic GetAppInfo(string appid)
        {
            return GetAppInfo(appid, out var appInfoKey);
        }

        public static dynamic GetAppInfo(string appid,out string appInfoKey)
        {
            appInfoKey = string.Empty;
            var app = ConfigManager.AppConfig.FirstOrDefault(x => x.Value.appId == appid);
            if (app.Key == null) return null;
            appInfoKey = app.Key;
            var dic = (IDictionary<string, object>)app.Value;
            dic["App"] = app.Key;            
            return dic as dynamic;
        }

        public static IAppContext CopyAppContext(dynamic appContext)
        {
            var newAppContext = new AppContext(null, null);
            try
            {
                newAppContext.ReuqestID = appContext.ReuqestID;
                newAppContext.AppID = appContext.AppID;
                newAppContext.Language = appContext.Language;

                if (appContext.User != null)
                {
                    newAppContext.User = Lib.Mapper<UserDto, UserDto>.Map(appContext.User);
                }

                CopyActionArguments(appContext.ActionArguments, newAppContext.ActionArguments);
            }
            catch (Exception ex) { Lib.Log.WriteExceptionLog($"CopyAppContext Exception：{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }
            return newAppContext;
        }

        public static void CopyActionArguments(IDictionary<string, object> source, IDictionary<string, object> target)
        {
            foreach (var a in source)
            {
                if (a.Value == null) continue;
                var type = a.Value.GetType();
                if (AppManager.IsValueType(type))
                {
                    target.Add(a.Key, a.Value);
                }
                else
                {
                    if (a.Value.GetType().Name != "FileCollection")
                    {
                        dynamic newItem = Lib.Mapper.CloneExtends.DeepCopyByReflect(a.Value);
                        target.Add(a.Key, newItem);
                    }
                }
            }
        }

        public const string PersistenceLogKey = "Channel:AppContext:Log";

        public static bool IsEnableApiLog { get; set; }

        public static bool IsEnableDatabaseLog { get; set; }

        public static bool IsCheckPermissionCode { get; set; }

        
        internal static object GetServiceFromRoot(Type type, IAppContext sourceAppContext = null)
        {
            object service = default(object);
            try
            {
                service = _rootServiceProvider.GetService(type);

                // 设置IAppContext，按固定命名获取，不循环全量字段（注意，接口不存在字段，实例才有）
                if (sourceAppContext != null)
                {
                    var serviceType = service.GetType();
                    var appContext = serviceType.GetField("app", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    if (appContext == null) appContext = serviceType.GetField("appContext", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    if (appContext != null)
                    {
                        appContext.SetValue(service, sourceAppContext);
                    }
                    else
                    {
                        Lib.Log.WriteOperationLog($"GetServiceFromRoot {type.Name}： appContext not found");
                    }
                }
            }
            catch (Exception ex) { Lib.Log.WriteExceptionLog($"GetServiceFromRoot Exception:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }
            return service;
        }

        public static T GetServiceFromRoot<T>()
        {
            return (T)GetServiceFromRoot(typeof(T));            
        }

        public static string GetServiceUrl(string name)
        {
            var discoveryClient = GetServiceFromRoot<Modobay.Discovery.IDiscoveryClient>();
            return discoveryClient.Get(name);
        }

        internal static IServiceCollection _services;

        public static void EndConfigureServices(this IServiceCollection services)
        {
            _services = services;
            _rootServiceProvider = services.BuildServiceProvider();            
        }

        public static void AddAppContext(this IServiceCollection services)
        {
            services.AddTransient<Modobay.ICookiesManager, Modobay.CookiesManager>();
            services.AddTransient<Modobay.IApiPermission, Modobay.ApiPermission>();
            services.AddScoped<Modobay.IAppContext, Modobay.AppContext>();

            services.AddTransient(typeof(Proxy.InvocationHandler), typeof(Proxy.FooInvocationHandler));
            services.AddTransient<Modobay.Discovery.IDiscoveryServer, Modobay.Discovery.DiscoveryServer>();
            services.AddTransient<Modobay.Discovery.IDiscoveryClient, Modobay.Discovery.DiscoveryClient>();
            services.AddTransient<Modobay.Config.IConfigServer, Modobay.Config.ConfigServer>();
        }

        public static Dictionary<string, Type> ProxyOriginalTypes { get; set; } = new Dictionary<string, Type>();

        public static List<string> AddServices(this IServiceCollection services, string bllAssemblyNameList)
        {
            var registerList = new List<string>();
            var items = GetServicesType(bllAssemblyNameList);
            var dependentClassList = new string[] { };
            if (!string.IsNullOrEmpty(ConfigManager.Configuration["ServiceConfig:DependentInterfaceList"]))
            {
                dependentClassList = ConfigManager.Configuration["ServiceConfig:DependentInterfaceList"].Split(',');
            }
            var unDependentClassList = new string[] { };
            if (!string.IsNullOrEmpty(ConfigManager.Configuration["ServiceConfig:UnDependentInterfaceList"]))
            {
                unDependentClassList = ConfigManager.Configuration["ServiceConfig:UnDependentInterfaceList"].Split(',');
            }
            var dependentServices = ConfigManager.Configuration["ServiceConfig:DependentNameSpaceList"];
            var isBuildDependentApi = ConfigManager.Configuration["ServiceConfig:BuildDependentApi"] == "1" ? true : false;
            Regex regDependentServices = string.IsNullOrEmpty(dependentServices) ? null : new Regex(dependentServices);
            var isOpenApi = ConfigManager.Configuration["OpenApi:IsOpen"] == "1";

            // 系统服务：IDiscoveryServer IIMOnlineTalkerManager
            var discoveryServer = typeof(Discovery.DiscoveryServer);
            items.Add(discoveryServer, GetInterfaces(discoveryServer).ToArray());

            foreach (var item in items)
            {
                try
                {
                    var isApiType = item.Key.GetCustomAttribute(typeof(Modobay.Api.NonControllerAttribute), true) == null;
                    if (isOpenApi && item.Key.GetCustomAttribute(typeof(Modobay.Api.OpenApiAttribute), true) == null)
                    {
                        isApiType = false;
                    }

                    foreach (var interfaceType in item.Value)
                    {
                        Type implementType = item.Key;
                        var isProxyType = false;
                        if (((regDependentServices != null && (regDependentServices.IsMatch(interfaceType.FullName)) || dependentClassList.Contains(interfaceType.Name)))
                                && !unDependentClassList.Contains(interfaceType.Name))
                        {
                            // 依赖的外部服务，由代理请求时到服务注册中心获取url                        
                            isProxyType = true;
                            implementType = Modobay.Proxy.InterfaceProxy.GetType(interfaceType, new Modobay.Proxy.FooInvocationHandler());
                            if (implementType == null) continue;
                            if (!ProxyOriginalTypes.ContainsKey(implementType.FullName))
                                ProxyOriginalTypes.Add(implementType.FullName, item.Key);
                        }

                        if (isApiType || (isProxyType && isBuildDependentApi))
                        {
                            _apiImplementTypes.Add(implementType);

                            // 避免路由冲突
                            if (!_apiInterfaceTypes.Any(x => x.Equals(interfaceType) || x.GetInterfaces().Contains(interfaceType))
                                && isApiType
                                && !interfaceType.FullName.StartsWith("System."))
                            {
                                _apiInterfaceTypes.Add(interfaceType);
                            }
                            else
                            {
                                if (!isProxyType) registerList.Add(interfaceType.FullName);
                            }
                        }

                        services.AddTransient(interfaceType, implementType);
                    }
                }
                catch (Exception ex) { Lib.Log.WriteExceptionLog($"AppManager.AddServices:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }
            }
            _apiImplementTypes = _apiImplementTypes.Distinct().ToList();
            return registerList;
        }

        public static void RegisterServices(this IServiceCollection services, List<string> registerList)
        {
            // 本地api注册到服务中心
            var registerService = ConfigManager.Configuration["ServiceConfig:RegisterService"];
            if (!string.IsNullOrEmpty(registerService))
            {
                try
                {
                    var discovery = GetServiceFromRoot<Modobay.Discovery.IDiscoveryServer>();
                    var reg = new Regex(registerService);
                    foreach (var item in _apiInterfaceTypes.Select(x => x.FullName).ToList())
                    {
                        if (reg.IsMatch(item)) registerList.Add(item);
                    }
                    var keep = discovery.Register(ServiceAddress.Replace(":", "_"), registerList);
                    // todo pxg 暂时屏蔽
                    Schedule.ScheduleManager.CreateJob<Modobay.Discovery.KeepServiceScheduleJob>($"0/{keep} * * * * ? *");
                }
                catch (Exception ex) { Lib.Log.WriteExceptionLog($"AppManager.AddServices:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }
            }
        }

        private static string _serviceAddress;
        public static string ServiceAddress
        {
            get
            {
                if (string.IsNullOrEmpty(_serviceAddress))
                {
                    if (string.IsNullOrEmpty(_serviceAddress))
                    {
                        _serviceAddress = GetServiceAddress("59019");
                    }
                    if (!_serviceAddress.EndsWith("/"))
                    {
                        _serviceAddress += "/";
                    }
                }
                return _serviceAddress;
            }
            set
            {
                _serviceAddress = value;
            }
        }

        public static string AttachmentUrl { get; set; }
        public static string AttachmentApiUrl { get; set; }

        public static string GetFullApiUrl(string action)
        {
            var apiUrl = ServiceAddress;
            if (apiUrl.EndsWith("/"))
            {
                apiUrl += action;
            }
            else
            {
                apiUrl += $"/{action}";
            }
            return apiUrl;
        }

        private static string _dataServerIP;
        public static string DataServerIP
        {
            get
            {
                if (string.IsNullOrEmpty(_dataServerIP))
                {
                    _dataServerIP = ConfigManager.Configuration["Environment:DataServerIP"];
                    if (string.IsNullOrEmpty(_dataServerIP))
                    {
                        _dataServerIP = Lib.Net.NetHelper.GetAddressIP();
                    }
                }
                return _dataServerIP;
            }
        }

        private static string _webServerIP;
        public static string WebServerIP
        {
            get
            {
                if (string.IsNullOrEmpty(_webServerIP))
                {
                    _webServerIP = ConfigManager.Configuration["Environment:WebServerIP"];
                    if (string.IsNullOrEmpty(_webServerIP))
                    {
                        _webServerIP = Lib.Net.NetHelper.GetAddressIP();
                    }
                }
                return _webServerIP;
            }
        }

        public static string GetServiceAddress(string port)
        {
            var ip = Lib.Net.NetHelper.GetAddressIP();
            string url = string.Empty;
            if (string.IsNullOrEmpty(port))
            {
                url = $"http://{ip}/";
            }
            else if (port.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                url = port + (port.EndsWith("/") ? "" : "/");
            }
            else
            {
                url = $"http://{ip}:{port}/";
            }
            return url;
        }

        /// <summary>  
        /// 获取程序集中的实现类对应的多个接口
        /// </summary>  
        /// <param name="assemblyNameList">程序集</param>
        private static Dictionary<Type, Type[]> GetServicesType(string assemblyNameList)
        {
            var result = new Dictionary<Type, Type[]>();

            foreach (var assemblyName in assemblyNameList.Split(','))
            {
                var assembly = Assembly.Load(assemblyName);
                var typeList = assembly.GetTypes().Where(x => !x.IsInterface && !x.IsGenericType && x.IsPublic).ToList();

                foreach (var item in typeList)
                {
                    var interfaceTypes = GetInterfaces(item);
                    if (interfaceTypes.Count() == 0) continue;
                    result.Add(item, interfaceTypes.ToArray());
                }
            }
            return result;
        }

        public static void AddApi(this IServiceCollection services, IMvcBuilder builder)
        {
            builder.AddMvcOptions(options =>
            {
                foreach (var interfaceType in _apiInterfaceTypes)
                {
                    options.Conventions.Add(new ControllerModelConvention(interfaceType));
                    options.Conventions.Add(new ActionModelConvention(interfaceType));
                    options.Conventions.Add(new ParameterModelConvention(interfaceType));
                }

                options.Filters.Add<ExceptionFilter>();
                options.Filters.Add<ActionFilter>();
            });

            //// 替换原有的Provider
            //var matches = services.Where(sd => sd.ServiceType == typeof(IApplicationModelProvider) && sd.ImplementationType == typeof(DefaultApplicationModelProvider)).FirstOrDefault();
            //services.Remove(matches);
            //services.AddTransient<IApplicationModelProvider, ApplicationModelProvider>();

            // 追加Provider，保留原有的
            builder.ConfigureApplicationPartManager(manager =>
            {
                var servicesControllers = new ControllerFeatureProvider(_apiImplementTypes);
                manager.FeatureProviders.Add(servicesControllers);                
            });

            builder.AddJsonOptions(options =>
            {
                //忽略空值
                options.JsonSerializerOptions.IgnoreNullValues = false;
                //格式化日期时间格式
                //options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
                //数据格式首字母小写
                options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;                
                ////数据格式原样输出
                //options.JsonSerializerOptions.PropertyNamingPolicy = null;
                ////取消Unicode编码
                //options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);                
                ////允许额外符号
                //options.JsonSerializerOptions.AllowTrailingCommas = true;
                ////反序列化过程中属性名称是否使用不区分大小写的比较
                //options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
            });
        }

        public static void AddApiCors(this IServiceCollection services, string name, string corsUrl)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name,
                    builder =>
                    {
                        if (corsUrl == "*")
                        {
                            //builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                        }
                        else
                        {
                            builder.WithOrigins(corsUrl.Split(',')).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                        }
                    });
            });
        }
               
        /// <summary>
        /// 获取该类型直接实现的接口
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static List<Type> GetInterfaces(Type type)
        {
            var interfaceTypes = type.GetInterfaces().ToList();
            if (type.BaseType != null && !type.BaseType.Equals(typeof(object)))
            {
                interfaceTypes = interfaceTypes.Except(type.BaseType?.GetInterfaces()).ToList();
            }
            return interfaceTypes;
        }

        /// <summary>
        /// 判断参数类型
        /// </summary>
        /// <param name="parameterType"></param>
        /// <returns></returns>
        internal static bool IsValueType(Type parameterType)
        {
            if (parameterType.Equals(typeof(object))) return false;
            return (parameterType.BaseType == null
                                        || parameterType.BaseType.Equals(typeof(ValueType))
                                        || parameterType.Equals(typeof(System.String))
                                        || parameterType.Equals(typeof(System.String[]))
                                        //|| parameterType.Equals(typeof(List<string>))
                                        || parameterType.Equals(typeof(System.Int32[])));
        }

        internal static bool IsValueArrayType(Type parameterType)
        {
            return (parameterType.Equals(typeof(System.String[]))
                                        //|| parameterType.Equals(typeof(List<string>))
                                        || parameterType.Equals(typeof(System.Int32[])));
        }


        public static void WriteLog(RequestInfoDto info)
        {
            Task.Run(async () =>
            {
                var logIno = info.DeepCloneObject();
                await Task.Delay(1);
                using (var common = Modobay.AppManager.GetServiceFromRoot<Modobay.Api.ISysLog>())
                {
                    common.WriteLog(logIno);
                }
            });
        }


        /// <summary>
        /// 状态码Startup.Configure：app.UseStatusCodePages(async context => Modobay.AppManager.HandleExceptionAsync(context));
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Task HandleExceptionAsync(StatusCodeContext context)
        {
            var err = JsonConvert.SerializeObject(new Modobay.Api.ExceptionResultDto()
            {
                ErrorCode = context.HttpContext.Response.StatusCode.ToString(),
                ErrorMessage = context.HttpContext.Connection?.RemoteIpAddress.ToString(),
                RequestId = context.HttpContext.TraceIdentifier
            });
            context.HttpContext.Response.ContentType = "application/json";
            return context.HttpContext.Response.WriteAsync(err);
        }

        public static void UseScheduleJob(this IApplicationBuilder app,string assemblyName)
        {
            var configSection = ConfigManager.Configuration.GetSection("ScheduleJob");
            var children = configSection.GetChildren();
            if (children.Count() == 0) return;

            // todo 选举单活定时服务，并且需要在单活故障后激活其他的（也可以通过配置的差异来实现单活）
            //var tran = Modobay.Cache.CacheManager.RedisCache.CreateTransaction();
            //var isKeyExists = Modobay.Cache.CacheManager.RedisCache.KeyExists(cacheKey);
            //var newCount = Modobay.Cache.CacheManager.RedisCache.StringIncrement(cacheKey, 1);
            //if (!isKeyExists)
            //{
            //    cacheTime = cacheTime ?? DateTime.Now.GetTodayRemain();
            //    Modobay.Cache.CacheManager.RedisCache.KeyExpire(cacheKey, cacheTime);
            //    CacheManager.AppCache.Set(cacheKey, newCount, cacheTime.Value);
            //}
            //else
            //{
            //    CacheManager.AppCache.Set(cacheKey, newCount);
            //}
            //tran.Execute();

            // todo pxg 多个程序集
            var assembly = Assembly.Load(assemblyName);

            foreach (var item in children)
            {
                if (string.IsNullOrEmpty(item.Value)) continue;
                var jobType = assembly.GetType(item.Key);
                if (jobType == null)
                {
                    Lib.Log.WriteExceptionLog($"UseScheduleJob:{item.Key} 加载失败");
                    continue;
                }
                Schedule.ScheduleManager.CreateJob(jobType, item.Value);
            }
        }
    }
}
