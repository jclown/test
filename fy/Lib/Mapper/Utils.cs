using System;
using System.Linq;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Lib.Mapper
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Utils
    {
        private static readonly Type _typeString = typeof(string);

        private static readonly Type _typeIEnumerable = typeof(IEnumerable);

        private static readonly ConcurrentDictionary<Type, Func<object>> _ctors = new ConcurrentDictionary<Type, Func<object>>();

        /// <summary>
        /// 判断是否是string以外的引用类型
        /// </summary>
        /// <returns>True：是string以外的引用类型，False：不是string以外的引用类型</returns>
        public static bool IsRefTypeExceptString(Type type)
            => !type.IsValueType && type != _typeString;

        /// <summary>
        /// 判断是否是string以外的可遍历类型
        /// </summary>
        /// <returns>True：是string以外的可遍历类型，False：不是string以外的可遍历类型</returns>
        public static bool IsIEnumerableExceptString(Type type)
            => _typeIEnumerable.IsAssignableFrom(type) && type != _typeString;

        /// <summary>
        /// 创建指定类型实例
        /// </summary>
        /// <param name="type">类型信息</param>
        /// <returns>指定类型的实例</returns>
        public static object CreateNewInstance(Type type) =>
            _ctors.GetOrAdd(type,
               t => Expression.Lambda<Func<object>>(Expression.New(t)).Compile())();

        public static void EnumToValue(object source, object target)//PropertyInfo[] properties
        {
            var enumType = source.GetType().GetProperties().Where(x => x.PropertyType.IsEnum).ToList();
            if (enumType.Count > 0)
            {
                var entityType = target.GetType();

                foreach (var p in enumType)
                {
                    var entityProperty = entityType.GetProperty(p.Name);
                    if (entityProperty.ToString().IndexOf("System.Byte") > 0)
                    {
                        var enumValue = (byte)p.GetValue(source);
                        if (entityProperty != null)
                        {
                            entityProperty.SetValue(target, enumValue, null);
                        }
                    }
                    else
                    {
                        var enumValue = (int)p.GetValue(source);
                        if (entityProperty != null)
                        {
                            entityProperty.SetValue(target, enumValue, null);
                        }
                    }
                }
            }
        }

        public static void ValueToEnum(object source, object target)//PropertyInfo[] properties
        {
            var enumType = target.GetType().GetProperties().Where(x => x.PropertyType.IsEnum).ToList();
            if (enumType.Count > 0)
            {
                var entityType = source.GetType();

                foreach (var p in enumType)
                {
                    var entityProperty = entityType.GetProperty(p.Name);
                    if (entityProperty.ToString().IndexOf("System.Byte") > 0)
                    {
                        var value = entityProperty.GetValue(source);                        
                        if (entityProperty != null && value != null)
                        {
                            p.SetValue(target, (byte)value, null);
                        }
                    }
                    else
                    {
                        var value = entityProperty.GetValue(source);
                        if (entityProperty != null && value != null)
                        {
                            p.SetValue(target, (int)value, null);
                        }
                    }
                }
            }
        }

        public static void SetNullToEmpty(object value)
        {
            // 将string为null的设置为空字符串
            foreach (var p in value.GetType().GetProperties())
            {
                if (p.PropertyType.Name == "String" && p.GetValue(value) == null)//字符串属性  
                {
                    p.SetValue(value, string.Empty, null);
                }
            }
        }
    }
}
