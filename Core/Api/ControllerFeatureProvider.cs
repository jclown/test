using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Modobay.Api
{
    internal class ControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private const string ControllerTypeNameSuffix = "Controller";
        private readonly IList<Type> ServiceTypes;

        public ControllerFeatureProvider(IList<Type> serviceTypes)
        {
            this.ServiceTypes = serviceTypes;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var type in ServiceTypes)
            {
                if (!IsController(type)) continue;
                if (ServiceTypes.Any(o => type.IsClass && o.IsAssignableFrom(type)) && !feature.Controllers.Contains(type))
                {
                    if (feature.Controllers.FirstOrDefault(x => x.Equals(type)) != null) continue;
                    feature.Controllers.Add(type.GetTypeInfo());
                }
            }
        }

        protected bool IsController(Type typeInfo)
        {
            if (typeInfo.IsAbstract) return false;
            if (!typeInfo.IsClass) return false;
            if (!typeInfo.IsPublic) return false;
            if (typeInfo.ContainsGenericParameters) return false;
            if (typeInfo.IsDefined(typeof(Microsoft.AspNetCore.Mvc.NonControllerAttribute))
                || typeInfo.IsDefined(typeof(Modobay.Api.NonControllerAttribute))) return false;

            if (!typeInfo.IsDefined(typeof(ControllerAttribute))
                && typeInfo.GetInterfaces().Where(x => x.FullName.IndexOf("ISerializable") == -1).ToList().Count == 0) return false;

            return true;
        }
    }
}
