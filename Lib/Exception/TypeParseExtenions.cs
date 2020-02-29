using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Lib
{ 
    public static class TypeParseExtenions
    {
        #region 强转成int 如果失败返回 0
        /// <summary>
        /// 强转成int 如果失败返回 0
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int TryToInt(this object thisValue)
        {
            int reval = 0;
            if (thisValue != null && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
        #endregion
        #region 强转成int 如果失败返回 errorValue
        /// <summary>
        /// 强转成int 如果失败返回 errorValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int TryToInt(this object thisValue, int errorValue)
        {
            int reval = 0;
            if (thisValue != null && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        #endregion
        #region 强转成double 如果失败返回 0
        /// <summary>
        /// 强转成money 如果失败返回 0
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static double TryToMoney(this object thisValue)
        {
            double reval = 0;
            if (thisValue != null && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return 0;
        }
        #endregion
        #region 强转成double 如果失败返回 errorValue
        /// <summary>
        /// 强转成double 如果失败返回 errorValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static double TryToMoney(this object thisValue, int errorValue)
        {
            double reval = 0;
            if (thisValue != null && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        #endregion
        #region 强转成string 如果失败返回 ""
        /// <summary>
        /// 强转成string 如果失败返回 ""
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string TryToString(this object thisValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return "";
        }
        #endregion
        #region 强转成string 如果失败返回 errorValue
        /// <summary>
        /// 强转成string 如果失败返回 str
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static string TryToString(this object thisValue, string errorValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return errorValue;
        }
        #endregion
        #region 强转成Decimal 如果失败返回 0
        /// <summary>
        /// 强转成Decimal 如果失败返回 0
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static Decimal TryToDecimal(this object thisValue)
        {
            Decimal reval = 0;
            if (thisValue != null && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return 0;
        }
        #endregion
        #region 强转成Decimal 如果失败返回 errorValue
        /// <summary>
        /// 强转成Decimal 如果失败返回 errorValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static Decimal TryToDecimal(this object thisValue, int errorValue)
        {
            Decimal reval = 0;
            if (thisValue != null && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        #endregion
        #region 强转成DateTime 如果失败返回 DateTime.MinValue
        /// <summary>
        /// 强转成DateTime 如果失败返回 DateTime.MinValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static DateTime TryToDate(this object thisValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
        #endregion
        #region 强转成DateTime 如果失败返回 errorValue
        /// <summary>
        /// 强转成DateTime 如果失败返回 errorValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static DateTime TryToDate(this object thisValue, DateTime errorValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        #endregion

        #region DataTable List
        /// <summary>
        /// 将集合类转换成DataTable
        /// </summary>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(this List<T> list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = typeof(T).GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        if (obj != null && obj != DBNull.Value)
                            tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
        /// <summary>
        /// 将datatable转为list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(this DataTable dt)
        {
            var list = new List<T>();
            Type t = typeof(T);
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());

            foreach (DataRow item in dt.Rows)
            {
                T s = System.Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
                    if (info != null)
                    {
                        if (!Convert.IsDBNull(item[i]))
                        {
                            info.SetValue(s, item[i], null);
                        }
                    }
                }
                list.Add(s);
            }
            return list;
        }
        #endregion

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="d"></param>
        /// <param name="n">四舍五入保留n位小数并保留多余的0</param>
        /// <returns></returns>
        public static decimal ToFixed(this object d, int n)
        {
            if (d == null) return 0;
            d = Math.Round(Convert.ToDecimal(d), n);
            var fix = "0.";
            for (var i = 0; i < n; i++)
            {
                fix = fix + "0";
            }
            return  d.TryToDecimal().ToString(fix).TryToDecimal();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="n">四舍五入保留n位小数并去掉多余的0</param>
        /// <returns></returns>
        public static string ToFixedS(this object d, int n)
        {
            if (d == null) return "";
            d = Math.Round(Convert.ToDecimal(d), n);
            var fix = "0.";
            for (var i = 0; i < n; i++)
            {
                fix = fix + "#";
            }
            return d.TryToDecimal().ToString(fix);
        }

        //public static decimal ToFixed(this object d, int n)
        //{
        //    var fix = "#.";
        //    for (var i = 0; i < n; i++)
        //    {
        //        fix = fix + "#";
        //    }
        //    return d.TryToDecimal().ToString(fix).TryToDecimal();
        //}
    }
}