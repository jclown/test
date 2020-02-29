using System;
using System.Globalization;
using System.Linq;
using System.ComponentModel;

namespace System
{
    /// <summary>
    /// Extension methods for all objects.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Used to simplify and beautify casting an object to a type. 
        /// </summary>
        /// <typeparam name="T">Type to be casted</typeparam>
        /// <param name="obj">Object to cast</param>
        /// <returns>Casted object</returns>
        public static T As<T>(this object obj)
            where T : class
        {
            return (T)obj;
        }

        /// <summary>
        /// Converts given object to a value type using <see cref="Convert.ChangeType(object,System.TypeCode)"/> method.
        /// </summary>
        /// <param name="obj">Object to be converted</param>
        /// <typeparam name="T">Type of the target object</typeparam>
        /// <returns>Converted object</returns>
        public static T To<T>(this object obj)
            where T : struct
        {
            if (typeof(T) == typeof(Guid))
            {
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(obj.ToString());
            }

            return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        }

        // todo pxg 通过mapper转换
        public static T ToDto<T>(this object obj)
            where T : class
        {            
            var sourceType = obj.GetType();
            var targetType = typeof(T);
            var mapperType = typeof(Lib.Mapper<,>).MakeGenericType(sourceType, targetType);
            var m = mapperType.GetMethod("CopyTo");
            var dto = System.Activator.CreateInstance<T>();
            m.Invoke(null, new object[] { obj, dto });
            return (T)dto;
        }

        public static TTarget ToDtoList<TSource, TTarget>(this object obj, Func<TSource, TTarget> func)
            where TSource : class where TTarget : class
        {
            //var sourceType = obj.GetType();
            //var targetType = typeof(T);
            //var mapperType = typeof(Lib.Mapper<,>).MakeGenericType(sourceType, targetType);
            //var m = mapperType.GetMethod("CopyTo");
            //var dto = System.Activator.CreateInstance<T>();
            //m.Invoke(null, new object[] { obj, dto });
            //return (T)dto;
            return null;
        }

        /// <summary>
        /// Check if an item is in a list.
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <param name="list">List of items</param>
        /// <typeparam name="T">Type of the items</typeparam>
        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }
    }
}
