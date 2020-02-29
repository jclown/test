using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Modobay.ApiDoc
{
    public class ApplyTagDescriptions : Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter
    {
        private readonly List<string> registerList;

        public ApplyTagDescriptions(List<string> registerList)
        {
            this.registerList = registerList;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var type = typeof(System.ComponentModel.DescriptionAttribute);
            var actionList = swaggerDoc.Paths.Select(x => x.Key).ToList();
            var controllerList = ApiDocManager.ApiInfoList.Where(x => actionList.Contains(x.ActionName)).Select(x => x.ControllerName).Distinct().ToList();
            swaggerDoc.Info.Title = $"接口数量合计：{actionList.Count}";
            swaggerDoc.Tags = AppManager._apiImplementTypes.Where(x => controllerList.Contains($"I{x.Name}"))
                .Select(x => new OpenApiTag() 
                { 
                    Name = x.Name, 
                    Description = (x.GetCustomAttribute(type, true) as dynamic)?.Description
                }).ToList();

            Dictionary<string, Type> dict = GetAllEnum();

            foreach (var item in swaggerDoc.Components.Schemas)
            //foreach (var item in swaggerDoc.Definitions)
            {
                var property = item.Value;
                var typeName = item.Key;
                Type itemType = null;
                if (property.Enum != null && property.Enum.Count > 0)
                {
                    if (dict.ContainsKey(typeName))
                    {
                        itemType = dict[typeName];
                    }
                    else
                    {
                        itemType = null;
                    }
                    List<OpenApiInteger> list = new List<OpenApiInteger>();
                    foreach (var val in property.Enum)
                    {
                        list.Add((OpenApiInteger)val);
                    }
                    property.Description += DescribeEnum(itemType, list);
                }
            }

            foreach (var schemaDictionaryItem in swaggerDoc.Components.Schemas)
            {
                var schema = schemaDictionaryItem.Value;
                //schema.Description
                foreach (var propertyDictionaryItem in schema.Enum)
                {
                    //var property = (propertyDictionaryItem as dynamic).Value;                    
                }
            }
        }
     
        private static Dictionary<string, Type> GetAllEnum()
        {
            Dictionary<string, Type> dict = new Dictionary<string, Type>();

            Assembly dtoAssembly = Assembly.Load("Dto");
            Type[] dtoTypes = dtoAssembly.GetTypes();
            
            foreach (Type item in dtoTypes)
            {
                if (item.IsEnum)
                {
                    dict.Add(item.Name, item);
                }
            }

            Assembly coreAssembly = Assembly.Load("Core");
            Type[] coreTypes = coreAssembly.GetTypes();

            foreach (Type item in coreTypes)
            {
                if (item.IsEnum)
                {
                    dict.Add(item.Name, item);
                }
            }

            return dict;
        }

        private static string DescribeEnum(Type type, List<OpenApiInteger> enums)
        {
            var enumDescriptions = new List<string>();
            foreach (var item in enums)
            {
                if (type == null) continue;
                var value = Enum.Parse(type, item.Value.ToString());
                var desc = GetDescription(type, value);

                if (string.IsNullOrEmpty(desc))
                    enumDescriptions.Add($"{item.Value.ToString()}:{Enum.GetName(type, value)}; ");
                else
                    enumDescriptions.Add($"{item.Value.ToString()}:{Enum.GetName(type, value)},{desc}; ");

            }
            return $"<br/>{Environment.NewLine}{string.Join("<br/>" + Environment.NewLine, enumDescriptions)}";
        }

        private static string GetDescription(Type t, object value)
        {
            foreach (MemberInfo mInfo in t.GetMembers())
            {
                if (mInfo.Name == t.GetEnumName(value))
                {
                    foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                    {
                        if (attr.GetType() == typeof(DescriptionAttribute))
                        {
                            return ((DescriptionAttribute)attr).Description;
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}
