using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Lib
{
    public class EnumHelper
    {
        /// <summary>
        /// 枚举转List<>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<EnumberEntity> EnumToList<T>()
        {
            List<EnumberEntity> list = new List<EnumberEntity>();

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                EnumberEntity m = new EnumberEntity();
                Type enumType = typeof(T);
                if (enumType.IsEnum)
                {
                    string name = e.ToString();
                    DescriptionAttribute customAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(enumType.GetField(name), typeof(DescriptionAttribute));
                    m.Desction = ((customAttribute == null) ? name : customAttribute.Description);
                }
                m.Value = Convert.ToInt32(e);
                m.Name = e.ToString();
                list.Add(m);
            }
            return list;
        }

        public static List<EnumberEntity> EnumToList(Type value)
        {
            var enumType = value.GetType();
            //if (!enumType.IsEnum) return null;

            List<EnumberEntity> list = new List<EnumberEntity>();

            foreach (var e in Enum.GetValues(value))
            {
                EnumberEntity m = new EnumberEntity();
                string name = e.ToString();
                DescriptionAttribute customAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(value.GetField(name), typeof(DescriptionAttribute));
                m.Desction = ((customAttribute == null) ? name : customAttribute.Description);
                m.EnumType = value.Name;
                m.Value = Convert.ToInt32(e);
                m.Name = e.ToString();
                list.Add(m);
            }
            return list;
        }

        public class EnumberEntity
        {
            /// <summary>
            /// 归属枚举
            /// </summary>
            public string EnumType { set; get; }

            /// <summary>  
            /// 枚举的描述  
            /// </summary>  
            public string Desction { set; get; }

            /// <summary>  
            /// 枚举名称  
            /// </summary>  
            public string Name { set; get; }

            /// <summary>  
            /// 枚举对象的值  
            /// </summary>  
            public int Value { set; get; }
        }
    }
}
