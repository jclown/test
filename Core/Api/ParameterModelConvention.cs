using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Modobay.Api
{
    internal class ParameterModelConvention : IParameterModelConvention
    {
        public ParameterModelConvention(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        private Type serviceType { get; }

        public void Apply(ParameterModel parameter)
        {
            if (!serviceType.IsAssignableFrom(parameter.Action.Controller.ControllerType)) return;

            var actionParams = parameter.Action.ActionMethod.GetParameters();
            //var actionParams = serviceType.GetMethod(parameter.Action.ActionName).GetParameters();
            var method = serviceType.GetMethods().FirstOrDefault(mth => parameter.Action.ActionMethod.Name == mth.Name && !actionParams.Except(mth.GetParameters(), new ModelConventionHelper.ParameterInfoEqualityComparer()).Any());
            if (method == null) return;
            var theParam = parameter.ParameterInfo;
            var isGetMethod = method.GetCustomAttribute<HttpGetAttribute>(true) != null;

            if (theParam == null) return;

            var paramAttrs = theParam.GetCustomAttributes().Select(att => isGetMethod ? (IBindingSourceMetadata)new FromQueryAttribute() : new FromBodyAttribute());

            //当没有指定请求Content-Type时
            if (!paramAttrs.Any())
            {
                //默认配置：如果参数类型为uri参数或者是uri参数数组，并且Action且为Get方法，则从QueryString取（框架默认，所以直接返回），否则从Body取
                if (theParam.ParameterType.IsUriParameterType() || theParam.ParameterType.IsUriParameterTypeArray())
                    return;

                if (isGetMethod)
                {
                    paramAttrs = new[] { new FromQueryAttribute() };
                }
                else
                {
                    paramAttrs = new IBindingSourceMetadata[] { new FromBodyAttribute(), new FromFormAttribute() };
                }
            }

            parameter.BindingInfo = BindingInfo.GetBindingInfo(paramAttrs);
        }
    }
}