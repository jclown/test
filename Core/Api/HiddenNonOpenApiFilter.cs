//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Swashbuckle.AspNetCore.Swagger;
//using Swashbuckle.AspNetCore.SwaggerGen;

//namespace Modobay.Api
//{
//    public class HiddenNonOpenApiFilter : IDocumentFilter
//    {

//        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
//        {
//            var isOpenApi = ConfigManager.Configuration["OpenApi:IsOpen"] == "1";
//            if (isOpenApi)
//            {
//                swaggerDoc.Definitions.Clear();
//                foreach (var item in context.ApiDescriptions)
//                {
//                    var api = item.ActionDescriptor.AttributeRouteInfo.Template;
//                    if (api.IndexOf("OpenApi/") == -1)
//                    {
//                        swaggerDoc.Paths.Remove("/" + item.RelativePath);
//                    }
//                }
//            }
//        }
//    }
//}
