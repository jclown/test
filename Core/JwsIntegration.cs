using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Modobay
{
    public static class WebHostBuilderJexusExtensions
    {

        /// <summary>
        /// 启用JexusIntegration中间件
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseJexusIntegration(this IWebHostBuilder hostBuilder)
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            // 检查是否已经加载过了
            if (hostBuilder.GetSetting(nameof(UseJexusIntegration)) != null)
            {
                return hostBuilder;
            }


            // 设置已加载标记，防止重复加载
            hostBuilder.UseSetting(nameof(UseJexusIntegration), true.ToString());


            // 添加configure处理
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter>(new JwsSetupFilter());
            });


            return hostBuilder;
        }

    }

    public class JwsSetupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseMiddleware<JexusMiddleware>();
                next(app);
            };
        }
    }


    public class JexusMiddleware
    {
        readonly RequestDelegate _next;
        private readonly ILoggerFactory loggerFactory;

        public JexusMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IOptions<IISOptions> options)
        {
            _next = next;
            this.loggerFactory = loggerFactory;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var headers = httpContext.Request.Headers;

            try
            {
                const string Header_Key_Real = "X-Real-IP";// "X-Original-For";
                const string Header_Key_Forwarded = "X-Forwarded-For";
                string ipaddAdndPort = string.Empty;

                if (headers != null)
                {
                    if (headers.ContainsKey(Header_Key_Forwarded))
                    {
                        ipaddAdndPort = headers[Header_Key_Forwarded].ToArray()[0];
                    }
                    else if (headers.ContainsKey(Header_Key_Real))
                    {
                        ipaddAdndPort = headers[Header_Key_Real].ToArray()[0];
                    }

                    if (!string.IsNullOrEmpty(ipaddAdndPort))
                    {
                        var dot = ipaddAdndPort.IndexOf(":", StringComparison.Ordinal);
                        var ip = ipaddAdndPort;
                        var port = 0;
                        if (dot > 0)
                        {
                            ip = ipaddAdndPort.Substring(0, dot);
                            port = int.Parse(ipaddAdndPort.Substring(dot + 1));
                        }

                        httpContext.Connection.RemoteIpAddress = System.Net.IPAddress.Parse(ip);
                        if (port != 0) httpContext.Connection.RemotePort = port;
                    }
                }
            }
            finally
            {
                try
                {
                    await _next(httpContext);
                }
                catch (Exception ex) { Lib.Log.WriteExceptionLog($"Invoke:Message:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }
            }
        }
    }
}
