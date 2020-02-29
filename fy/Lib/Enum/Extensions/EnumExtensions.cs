using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举的说明
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="n">枚举int值</param>
        /// <returns></returns>
        public static string GetDescription<T>(this int n) where T : struct
        {
            try
            {
                Type enumType = typeof(T);
                if (enumType.IsEnum)
                {
                    string name = Enum.GetName(enumType, n);
                    DescriptionAttribute customAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(enumType.GetField(name), typeof(DescriptionAttribute));
                    return ((customAttribute == null) ? name : customAttribute.Description);
                }
            }
            catch { }
            return string.Empty;
        }

        public static T ToEnum<T>(this int value, string errorMessage = null)
           where T : struct
        {
            T result = default(T);

            try
            {
                result = (T)Enum.ToObject(typeof(T), value);
            }
            catch (Exception ex)
            {
                // todo 抛出AppException
                throw new Exception(errorMessage ?? ex.Message);
            }

            if (Lib.RegexTool.CheckNumberByString(result.ToString()))
            {
                throw new Exception(errorMessage);
            }

            return result;
        }


        /// <summary>
        /// Converts string to enum value.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">String value to convert</param>
        /// <returns>Returns enum object</returns>
        public static T ToEnum<T>(this string value)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Converts string to enum value.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">String value to convert</param>
        /// <param name="ignoreCase">Ignore case</param>
        /// <returns>Returns enum object</returns>
        public static T ToEnum<T>(this string value, bool ignoreCase)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }


        /// <summary>
        ///获取描述属性的对象扩展方法【扩展方法】
        /// </summary>
        /// <param name="value"></param>
        /// <returns>描述属性</returns>
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null) return string.Empty;
            var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return attr == null ? "" : attr.Description;
        }

        /// <summary>
        ///获取描述属性的对象扩展方法【扩展方法】
        /// </summary>
        /// <param name="value"></param>
        /// <returns>备注属性</returns>
        public static string GetRemark(this Enum value)
        {
            var attr = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(Lib.RemarkAttribute), false).FirstOrDefault() as Lib.RemarkAttribute;
            return attr == null ? "" : attr.Remark;
        }

    }
}
