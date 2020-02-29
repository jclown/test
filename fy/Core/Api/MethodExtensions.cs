using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Modobay.Api
{
    internal static class MethodExtensions
    {
        internal static string GetPath(this MethodInfo method, Type declaringType = null)
        {
            // todo pxg 路由设置。暂时忽略：方法重载可能会导致接口同名，后期由调用方判断该接口是否已存在并重命名处理
            var type = declaringType ?? method.DeclaringType;

            /*
             * 完整路径：/Bll/CRM/IContactManager/GetContactList，简化后ContactManager/GetContactList
             * 简化路径：不要类名(如要保留，须移除接口的命名前缀“I”)，即/{最后一节命名空间}/{方法}
             */

            var nameSpace = type.Namespace.Split('.').LastOrDefault();
            var path = string.Format("{0}/{1}", nameSpace, method.Name);
            ApiDocManager.ApiInfoList.Add(new ApiDoc.ApiInfo() { ControllerName = type.Name, ActionName = $"/{path}" });
            return path;
        }
    }
}