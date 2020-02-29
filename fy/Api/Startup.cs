using Dal;
using Modobay;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;

namespace Api
{
    public class Startup
    {
        private const string ServiceDll = "Bll";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigManager.Configuration = Configuration;
            System.Threading.ThreadPool.SetMinThreads(200, 200);
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            services.AddSingleton<ICache, ApiClient.Redis.RedisApiClient>();
            services.AddSingleton<Modobay.IRedisApiClient, ApiClient.Redis.RedisApiClient>();            
            services.AddTransient<Modobay.IAuthenticationProxy, Modobay.AuthenticationProxy>();            
            services.AddAppContext();

            // dal
            services.AddDbContext3(Configuration["ConnectionStrings:SqlServer"], Configuration["ConnectionStrings:SqlServerFy"]);
                        
            // 注册服务（多个程序集需以逗号间隔）
            var registerList = services.AddServices(ServiceDll);

            // 注册服务Api          
            //var builder = services.AddControllersWithViews();
            var builder = services.AddControllersWithViews().AddNewtonsoftJson(options => { options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; });
            services.AddApi(builder);
            services.AddApiCors("AllowCors", Configuration[$"CorsUrl"] ?? string.Empty);
            services.AddRazorPages();
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // 注册ApiDoc
            services.ConfigureApiDoc("Api.xml,Bll.xml,Model.xml,Dto.xml,Core.xml", registerList);

            // 注册第三方服务Client
            services.AddHttpClient();
            services.AddTransient<ApiClient.FastDFSApiClient>();

            services.Configure<FormOptions>(options =>
            {
                options.KeyLengthLimit = 20480;
            });
            
            // 结束
            services.EndConfigureServices();            

            // 注册到服务中心
            services.RegisterServices(registerList);

            // 配置
            AppManager.IsEnableApiLog = (Configuration[$"EnableApiLog"] == "1");
            AppManager.IsEnableDatabaseLog = (Configuration[$"EnableDatabaseLog"] == "1");
            //AppManager.AttachmentUrl = Configuration[$"FastDFS:OpenUrl"].Replace("{CustomerMall}", Configuration[$"Environment:CustomerMall"]);
            //AppManager.AttachmentApiUrl = Configuration[$"FastDFS:SafeUrl"];//.Replace("{API}", AppManager.ServiceAddress);
            Lib.StopwatchLog.IsEnablePerformanceLog = (Configuration[$"EnablePerformanceLog"] == "1");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(builder => builder.Run(async context => await ErrorEvent(context)));
            //app.UseMiddleware(typeof(ErrorHandlingMiddleware));            
            //app.UseStatusCodePages(async context => Modobay.AppManager.HandleExceptionAsync(context));
            
            app.UseHttpsRedirection();
            app.UseCors("AllowCors");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseApiDoc(Program.ConfigFile);
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
            
            app.UseScheduleJob(ServiceDll);

            Lib.Log.WriteOperationLog($"Api-{AppManager.ServiceAddress} Loading completed.");
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var message = Lib.Log.GetExceptionLog(e.Exception);
            System.Diagnostics.StackFrame fr = new System.Diagnostics.StackFrame(1, true);
            MethodBase mb = fr.GetMethod();
            Lib.Log.WriteExceptionLog($"{System.Environment.NewLine}调用方法：{mb.Name} {fr.GetFileName()} {fr.GetFileLineNumber()}  {System.Environment.NewLine}exception:{message}");
        }

        public Task ErrorEvent(Microsoft.AspNetCore.Http.HttpContext context)
        {
            string errorMessage = string.Empty;
            string requestMessage = string.Empty;

            try
            {
                var feature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
                var error = feature?.Error;
                var errorDto = Modobay.Api.ResultBuilder.AsFail(error, "Startup.ErrorEvent");

                var info = new Modobay.Api.RequestInfoDto();
                info.RequestId = context.TraceIdentifier;
                info.ClientIpAddress = context.Request.HttpContext.Connection?.RemoteIpAddress.ToString();
                info.ClientIpPort = context.Request.HttpContext.Connection?.LocalPort.ToString();
                if (info.StartTime.Equals(DateTime.MinValue)) info.StartTime = DateTime.Now;
                info.RequestApi = context.Request.Path;
                info.RequestParams = context.Request.QueryString.ToString();

                errorMessage = JsonConvert.SerializeObject(errorDto);
                info.ExceptionLog = errorMessage;
                requestMessage = JsonConvert.SerializeObject(info);

                // 记录到文本
                Lib.Log.WriteExceptionLog(requestMessage);
            }
            catch { }

            var code = HttpStatusCode.InternalServerError;
            context.Response.OnStarting(() =>
             {
                 context.Response.ContentType = "application/json";
                 context.Response.StatusCode = (int)code;
                 return context.Response.WriteAsync(errorMessage);
             });

            return Task.FromResult(0);
        }
    }
}
