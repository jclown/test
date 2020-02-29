using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Modobay.ApiDoc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Modobay
{
    public static class ApiDocManager
    {
        public const string TokenHeaderName = "Authorization";
        const string SwaggerDocName = "all";
        public static List<ApiInfo> ApiInfoList { get; } = new List<ApiInfo>();
        public static void ConfigureApiDoc(this IServiceCollection services, string xmlFilename,List<string> registerList = null)
        {
            if (ConfigManager.Configuration["EnableApiDoc"] != "1") return;
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(SwaggerDocName, new OpenApiInfo { Title = "", Version = SwaggerDocName });
                foreach (var doc in ConfigManager.ApiDocConfig.OrderByDescending(o => o.Key).ToList())
                {
                    if (doc.Key == SwaggerDocName) throw new AppException("接口文档分组名称重复");
                    options.SwaggerDoc(doc.Key, new OpenApiInfo { Title = "", Version = doc.Key });
                }

                // Token
                var securitName = "Authentication token";
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference()
                                {
                                    Id = securitName,
                                    Type = ReferenceType.SecurityScheme
                                }
                            }, Array.Empty<string>()
                        }
                    });
                options.AddSecurityDefinition(securitName, new OpenApiSecurityScheme()
                {
                    Description = "请输入token",
                    Name = TokenHeaderName,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                // todo 解决相同类名会报错的问题
                //options.CustomSchemaIds(type => type.FullName); // 解决相同类名会报错的问题

                // 接口分组和隐藏处理
                options.DocInclusionPredicate((docName, apiDescription) =>
                {
                    // apiDoc展示开关
                    if (ConfigManager.Configuration["EnableApiDoc"] != "1") return false;

                    // 默认分组的接口，除了demo的，其他全部展示
                    if (docName == SwaggerDocName)
                    {
                        if (CheckApiDocGroup("demo", apiDescription.RelativePath)) return false;
                        return true;
                    }

                    // 其他分组的，按规则展示，未配置规则全部展示
                    return CheckApiDocGroup(docName, apiDescription.RelativePath);
                });

                // 显示中文
                options.DocumentFilter<ApplyTagDescriptions>(registerList);

                //// 支持文件上传
                //options.OperationFilter<SwaggerFileHeaderParameter>();

                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);//获取应用程序所在目录
                foreach (var filename in xmlFilename.Split(','))
                {
                    options.IncludeXmlComments(Path.Combine(basePath, filename));
                }

                options.DescribeAllEnumsAsStrings();
            });
        }

        private static bool CheckApiDocGroup(string docName,string relativePath)
        {
            var apiRegexList = ConfigManager.ApiDocConfig[docName];
            if (apiRegexList == null || apiRegexList.Count == 0) return false;

            foreach (var apiRegex in apiRegexList)
            {
                if (string.IsNullOrEmpty(apiRegex)) continue;
                var reg = new Regex(apiRegex);
                if (reg.IsMatch(relativePath)) return true;
            }

            return false;
        }

        public static void UseApiDoc(this IApplicationBuilder app,string configFile)
        {
            if (ConfigManager.Configuration["EnableApiDoc"] != "1") return;
            // 启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();

            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.ShowExtensions();
                c.EnableFilter();
                c.DocumentTitle = $"魔得宝接口文档 {configFile.Split('.')[1]}";
                //c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);//折叠所有方法
                //c.DefaultModelsExpandDepth(-1);//不显示models
                
                foreach (var doc in ConfigManager.ApiDocConfig)
                {
                    c.SwaggerEndpoint($"/swagger/{doc.Key}/swagger.json", doc.Key);
                }
                c.SwaggerEndpoint($"/swagger/{SwaggerDocName}/swagger.json", SwaggerDocName);
            });
        }
    }
}
