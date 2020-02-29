﻿using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using HttpDelete = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGet = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpHead = Microsoft.AspNetCore.Mvc.HttpHeadAttribute;
using HttpOptions = Microsoft.AspNetCore.Mvc.HttpOptionsAttribute;
using HttpPost = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPut = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using Route = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Modobay.Api
{
    internal class ActionModelConvention : IActionModelConvention
    {
        public ActionModelConvention(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        private Type serviceType { get; }

        public void Apply(ActionModel action)
        {
            if (!serviceType.IsAssignableFrom(action.Controller.ControllerType)) return;
            var actionParams = action.ActionMethod.GetParameters();
            var method = serviceType.GetMethods().FirstOrDefault(methodInfo =>
                action.ActionMethod.Name == methodInfo.Name
                && !actionParams.Except(methodInfo.GetParameters(), new ModelConventionHelper.ParameterInfoEqualityComparer()).Any());
            if (method == null) return;

            var attrs = method.GetCustomAttributes();
            var actionAttrs = new List<object>();
            var routePath = method.GetPath();

            if (!attrs.Any(x => x is HttpMethodAttribute || x is RouteAttribute))
            {
                if (actionParams.Any(x => !(AppManager.IsValueType(x.ParameterType) || x.ParameterType.IsEnum)) 
                    || method.Name.StartsWith("Delete") || method.Name.StartsWith("Update"))
                {
                    actionAttrs.Add(new HttpPost(routePath));
                }
                else
                {
                    actionAttrs.Add(new HttpGet(routePath));
                }
            }
            else
            {
                foreach (var att in attrs)
                {
                    switch (att)
                    {
                        case HttpMethodAttribute methodAttr:
                            var httpMethod = methodAttr.Method;
                            var path = methodAttr.Path;

                            if (httpMethod == HttpMethod.Get)
                            {
                                actionAttrs.Add(new HttpGet(path));
                            }
                            else if (httpMethod == HttpMethod.Post)
                            {
                                actionAttrs.Add(new HttpPost(path));
                            }
                            else if (httpMethod == HttpMethod.Put)
                            {
                                actionAttrs.Add(new HttpPut(path));
                            }
                            else if (httpMethod == HttpMethod.Delete)
                            {
                                actionAttrs.Add(new HttpDelete(path));
                            }
                            else if (httpMethod == HttpMethod.Head)
                            {
                                actionAttrs.Add(new HttpHead(path));
                            }
                            else if (httpMethod == HttpMethod.Options)
                            {
                                actionAttrs.Add(new HttpOptions(path));
                            }
                            break;

                        case RouteAttribute routeAttr:
                            actionAttrs.Add(new Route(routeAttr.Template));
                            break;
                    }
                }
            }

            action.Selectors.Clear();
            ModelConventionHelper.AddRange(action.Selectors, ModelConventionHelper.CreateSelectors(actionAttrs));
        }
    }
}