using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace Lib.Office
{
    public static class ConvertHelper
    {


        #region IEnumerable<T>转换DataTable
        /// <summary>
        /// 
        /// Description:IEnumerable<T>转换DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static DataTable ConvertIEnumerableToDataTable<T>(this IEnumerable<T> array)
        {
            var dt = new DataTable();
            var pro = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor dp in pro)
            {
                if ((dp.PropertyType).IsGenericType && (dp.PropertyType).IsConstructedGenericType)
                    continue;

                dt.Columns.Add(dp.Name, dp.PropertyType);
            }

            foreach (T item in array)
            {
                var Row = dt.NewRow();
                foreach (PropertyDescriptor dp in pro)
                {
                    if ((dp.PropertyType).IsGenericType && (dp.PropertyType).IsConstructedGenericType)
                        continue;

                    Row[dp.Name] = dp.GetValue(item);
                }

                dt.Rows.Add(Row);
            }
            return dt;
        }

        #endregion


        #region  DataTable转换IEnumerable<T>
        /// <summary>
        /// 
        /// Description: DataTable转换IEnumerable<T>
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="dt">数据表</param>
        /// <returns>返回实体集合</returns>
        public static IEnumerable<T> ConvertDataTableToIEnumerable<T>(this DataTable dt) where T : class, new()
        {

            var pros = typeof(T).GetProperties();
            T entity = null;
            foreach (DataRow dr in dt.Rows)
            {
                entity = new T();
                foreach (var pr in pros)
                {
                    if (dt.Columns.Contains(pr.Name))
                    {
                        var value = dr[pr.Name];

                        if (pr.CanWrite)
                        {
                            if (value != DBNull.Value && !string.IsNullOrEmpty(value.ToString()))
                            {
                                pr.SetValue(entity, TypeConvert(value, pr.PropertyType), null);
                            }
                            else
                            {
                                if (pr.PropertyType.Equals(typeof(DateTime)))
                                    pr.SetValue(entity, System.Data.SqlTypes.SqlDateTime.MinValue.Value, null);
                                else
                                    pr.SetValue(entity, pr.PropertyType.IsValueType ? Activator.CreateInstance(pr.PropertyType) : "", null);
                            }

                        }
                    }
                    else
                    {
                        if (pr.PropertyType.Equals(typeof(DateTime)))
                        {
                            pr.SetValue(entity, System.Data.SqlTypes.SqlDateTime.MinValue.Value, null);
                        }
                        else if (pr.PropertyType.Equals(typeof(string)))
                        {
                            pr.SetValue(entity, "", null);
                        }
                    }
                }
                yield return (entity);
            }

        }
        #endregion


        #region 类型强转 (可判断泛型Nullable值)
        /// <summary>
        /// 
        /// Description ：类型强转
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object TypeConvert(object obj, Type t)
        {
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (obj == null)
                {
                    return null;
                }
                t = Nullable.GetUnderlyingType(t);
            }
            return Convert.ChangeType(obj, t);

        }
        #endregion


    }
}
