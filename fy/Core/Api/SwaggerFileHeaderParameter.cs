//using Microsoft.AspNetCore.Http;
//using Swashbuckle.AspNetCore.Swagger;
//using Swashbuckle.AspNetCore.SwaggerGen;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Modobay.Api
//{
//    public class SwaggerFileHeaderParameter : IOperationFilter
//    {
//        public void Apply(Operation operation, OperationFilterContext context)
//        {
//            operation.Parameters = operation.Parameters ?? new List<IParameter>();

//            if (!context.ApiDescription.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) &&
//                !context.ApiDescription.HttpMethod.Equals("PUT", StringComparison.OrdinalIgnoreCase))
//            {
//                return;
//            }

//            var fileParameters = context.ApiDescription.ActionDescriptor.Parameters.Where(x =>
//                x.ParameterType == typeof(IFormFile)
//                || x.ParameterType == typeof(IFormFileCollection)).ToList();

//            if (fileParameters.Count == 0)
//            {
//                return;
//            }

//            const string FileCountForSwaggerUIOnly = "fileCountForSwaggerUIOnly";
//            var iFormFileCount = 1;
//            var fileCountForSwaggerUIOnly = context.ApiDescription.ActionDescriptor.Parameters.FirstOrDefault(x => x.Name == FileCountForSwaggerUIOnly);
//            if (fileCountForSwaggerUIOnly != null)
//            {
//                var parameterInfoProperty = fileCountForSwaggerUIOnly.GetType().GetProperty("ParameterInfo");
//                var parameterInfo = parameterInfoProperty.GetValue(fileCountForSwaggerUIOnly);
//                var defaultValue = parameterInfo.GetType().GetProperty("DefaultValue").GetValue(parameterInfo);
//                iFormFileCount = (int)defaultValue;
//                operation.Parameters.Remove(operation.Parameters.FirstOrDefault(x => x.Name == FileCountForSwaggerUIOnly));
//            }

//            foreach (var fileParameter in fileParameters)
//            {
//                for (int i = 1; i <= iFormFileCount; i++)
//                {
//                    if (fileParameter.BindingInfo == null)
//                    {
//                        operation.Parameters.Add(new NonBodyParameter
//                        {
//                            Name = fileParameter.Name + "_" + i.ToString(),
//                            In = "formData",
//                            Description = "文件上传",
//                            Required = false,
//                            Type = "file"
//                        });
//                    }
//                    else
//                    {
//                        var parameter = operation.Parameters.Single(n => n.Name == fileParameter.Name);
//                        //operation.Parameters.Remove(parameter);
//                        operation.Parameters.Add(new NonBodyParameter
//                        {
//                            Name = parameter.Name + "_" + i.ToString(),
//                            In = "formData",
//                            Description = parameter.Description,
//                            Required = parameter.Required,
//                            Type = "file"
//                        });
//                    }
//                }

//                operation.Consumes.Add("multipart/form-data");
//                operation.Parameters.Remove(operation.Parameters.FirstOrDefault(x => x.Name == fileParameter.Name));
//            }
//            //fileParameters.ForEach(x => operation.Parameters.RemoveAt();
//        }
//    }

//}
