using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Lib.Mapper;

namespace Lib
{
    public static class Mapper<TSource, TTarget> where TSource : class where TTarget : class
    {
        /// <summary>
        /// 对象数据转换。仅支持同名属性转换，区分大小写。
        ///<para>示例：var b1 = Mapper&lt;A, B&gt;.Map(a);</para>
        ///<para></para>
        ///<para>建议：为了获得更好的性能，建议对需要转换的类型在Dal项目中【预先初始化】一次</para>
        ///<para>预先初始化示例1-简单对象：Mapper&lt;A, B&gt;.Map(new A());</para>
        ///<para>预先初始化示例2-组合对象：Mapper&lt;A, B&gt;.Map(new A() { User = new C() });</para>
        /// </summary>
        public static readonly Func<TSource, TTarget> Map;

        /// <summary>
        /// 同Map，忽略大小写，性能稍弱。
        /// </summary>
        public static readonly Func<TSource, TTarget> MapIgnoreCase;

        /// <summary>
        /// 拷贝source的数据到target。适用于db.Create时候后，拷贝dto的数据到实体对象。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void CopyTo(TSource source, TTarget target)
        {
            Copier<TSource, TTarget>.Copy(source, target);
            Utils.SetNullToEmpty(target);
        }

        public static TTarget Clone(TSource source)
        {
            var target = Map(source);
            Utils.SetNullToEmpty(target);
            return target;            
        }

        public static TTarget CloneDto(TSource source)
        {
            var target = Map(source);
            Utils.SetNullToEmpty(target);
            Utils.ValueToEnum(source, target);
            return target;
        }

        static Mapper()
        {
            if (Map == null) Map = GetMap();
            if (MapIgnoreCase == null) MapIgnoreCase = GetMap(true);
        }

        private static Func<TSource, TTarget> GetMap(bool ignoreCase = false)
        {
            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);
            var parameterExpression = Expression.Parameter(sourceType, "p");
            var memberInitExpression = GetExpression(parameterExpression, sourceType, targetType, ignoreCase);
            var lambda = Expression.Lambda<Func<TSource, TTarget>>(memberInitExpression, parameterExpression);
            return lambda.Compile();
        }

        /// <summary>
        /// 根据转换源和目标获取表达式树
        /// </summary>
        /// <param name="parameterExpression">表达式参数p</param>
        /// <param name="sourceType">转换源类型</param>
        /// <param name="targetType">转换目标类型</param>
        /// <returns></returns>
        private static MemberInitExpression GetExpression(Expression parameterExpression, Type sourceType,
            Type targetType, bool ignoreCase = false)
        {
            var memberBindings = new List<MemberBinding>();

            foreach (var targetItem in targetType.GetProperties().Where(x => x.PropertyType.IsPublic && x.CanWrite))
            {
                PropertyInfo sourceItem;
                if (ignoreCase)
                {
                    sourceItem = sourceType.GetProperty(targetItem.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                }
                else
                {
                    sourceItem = sourceType.GetProperty(targetItem.Name);
                }

                //判断实体的读写权限
                if (sourceItem == null || !sourceItem.CanRead || sourceItem.PropertyType.IsNotPublic) continue;

                //标注NotMapped特性的属性忽略转换
                if (sourceItem.GetCustomAttribute<NotMappedAttribute>() != null) continue;

                var propertyExpression = Expression.Property(parameterExpression, sourceItem);

                //判断都是class 且类型不相同时
                if (targetItem.PropertyType.IsClass && sourceItem.PropertyType.IsClass &&
                    targetItem.PropertyType != sourceItem.PropertyType)

                    if (targetItem.PropertyType != targetType) //防止出现自己引用自己无限递归
                    {
                        var memberInit = GetExpression(propertyExpression, sourceItem.PropertyType,
                            targetItem.PropertyType);

                        memberBindings.Add(Expression.Bind(targetItem, memberInit));
                        continue;
                    }

                if (targetItem.PropertyType != sourceItem.PropertyType) continue;

                memberBindings.Add(Expression.Bind(targetItem, propertyExpression));
            }

            return Expression.MemberInit(Expression.New(targetType), memberBindings);
        }
    }
}