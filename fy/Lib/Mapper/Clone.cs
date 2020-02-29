using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Lib.Mapper
{
    public static class CloneExtends
    {
        internal static bool IsValueArrayType(Type parameterType)
        {
            return (parameterType.Equals(typeof(System.String[]))
                                        //|| parameterType.Equals(typeof(List<string>))
                                        || parameterType.Equals(typeof(System.Int32[])));
        }

        public static T DeepCopyByReflect<T>(T obj)
        {
            //如果是字符串或值类型则直接返回
            if (obj == null) return default(T);
            var type = obj.GetType();
            if (obj is string || type.IsValueType) return obj;
            //if (IsValueArrayType(type))
            if (type.IsArray)
            {
                var method = EnumerableCopier.GetMethondInfo(type);
                var newItem = method.Invoke(null, new object[]{ obj });
                return (T)newItem;
            }
            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                try
                {
                    field.SetValue(retval, DeepCopyByReflect(field.GetValue(obj)));
                }
                catch(Exception ex )
                {
                    ex.ToString();
                }
            }
            return (T)retval;
        }
 
        public static T DeepCloneObject<T>(this T t) where T : class
        {
            T model = System.Activator.CreateInstance<T>();                     //实例化一个T类型对象
            PropertyInfo[] propertyInfos = model.GetType().GetProperties();     //获取T对象的所有公共属性
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //判断值是否为空，如果空赋值为null见else
                if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                    NullableConverter nullableConverter = new NullableConverter(propertyInfo.PropertyType);
                    //将convertsionType转换为nullable对的基础基元类型
                    propertyInfo.SetValue(model, Convert.ChangeType(propertyInfo.GetValue(t), nullableConverter.UnderlyingType), null);
                }
                else
                {
                    propertyInfo.SetValue(model, Convert.ChangeType(propertyInfo.GetValue(t), propertyInfo.PropertyType), null);
                }
            }
            return model;
        }
        public static IList<T> DeepCloneList<T>(this IList<T> tList) where T : class
        {
            IList<T> listNew = new List<T>();
            foreach (var item in tList)
            {
                T model = System.Activator.CreateInstance<T>();                     //实例化一个T类型对象
                PropertyInfo[] propertyInfos = model.GetType().GetProperties();     //获取T对象的所有公共属性
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    //判断值是否为空，如果空赋值为null见else
                    if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    {
                        //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                        NullableConverter nullableConverter = new NullableConverter(propertyInfo.PropertyType);
                        //将convertsionType转换为nullable对的基础基元类型
                        propertyInfo.SetValue(model, Convert.ChangeType(propertyInfo.GetValue(item), nullableConverter.UnderlyingType), null);
                    }
                    else
                    {
                        propertyInfo.SetValue(model, Convert.ChangeType(propertyInfo.GetValue(item), propertyInfo.PropertyType), null);
                    }
                }
                listNew.Add(model);
            }
            return listNew;
        }
    }
}
