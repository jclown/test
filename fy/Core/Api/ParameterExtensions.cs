using System;
using System.Linq;

namespace Modobay.Api
{
    internal static class ParameterExtensions
    {
        /// <summary>
        /// 判断是否为Uri参数类型
        /// </summary>
        /// <param name="parameterType"></param>
        /// <returns></returns>
        internal static bool IsUriParameterType(this Type parameterType)
        {
            if (parameterType == null)
                return false;

            if (parameterType.IsGenericType)
            {
                parameterType = parameterType.GetGenericArguments().FirstOrDefault();
            }

            if (parameterType.IsPrimitive || parameterType.IsEnum)
            {
                return true;
            }

            return parameterType == typeof(string)
                   || parameterType == typeof(decimal)
                   || parameterType == typeof(DateTime)
                   || parameterType == typeof(Guid)
                   || parameterType == typeof(Uri);
        }

        internal static bool IsUriParameterTypeArray(this Type parameterType)
        {
            return parameterType.BaseType == typeof(Array) && parameterType.GetElementType().IsUriParameterType();
        }
    }
}