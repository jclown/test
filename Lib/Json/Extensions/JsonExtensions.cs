using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    /// <summary>
    /// Json拓展
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GHSerializeObject(this object value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }
        
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GHSerializeObject(this object value, string[] ignorePropertyList)
        {
            return SerializeObject(value, ignorePropertyList);
            //// 保存值
            //var oldValues = new object[ignorePropertyList.Length];
            //for (int i = 0; i < ignorePropertyList.Length; i++)
            //{
            //    var p = value.GetType().GetProperty(ignorePropertyList[i]);
            //    if (p != null)
            //    {
            //        oldValues[i] = p.GetValue(value);
            //        p.SetValue(value, null, null);
            //    }
            //}

            //var serializeValue = Newtonsoft.Json.JsonConvert.SerializeObject(value);

            //// 清空值
            //for (int i = 0; i < ignorePropertyList.Length; i++)
            //{
            //    var p = value.GetType().GetProperty(ignorePropertyList[i]);
            //    if (p != null)
            //    {
            //        p.SetValue(value, oldValues[i], null);
            //    }
            //}

            //return serializeValue;
        }

        public static string SerializeObject(object value, string[] ignorePropertyList = null)
        {
            // 保存值
            object[] oldValues = null;
            if (ignorePropertyList != null)
            {
                oldValues = new object[ignorePropertyList.Length];
                for (int i = 0; i < ignorePropertyList.Length; i++)
                {
                    var p = value.GetType().GetProperty(ignorePropertyList[i]);
                    if (p != null)
                    {
                        oldValues[i] = p.GetValue(value);
                        p.SetValue(value, null, null);
                    }
                }
            }

            var serializeValue = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            
            // 清空值
            if (ignorePropertyList != null)
            {
                for (int i = 0; i < ignorePropertyList.Length; i++)
                {
                    var p = value.GetType().GetProperty(ignorePropertyList[i]);
                    if (p != null)
                    {
                        p.SetValue(value, oldValues[i], null);
                    }
                }
            }

            return serializeValue;
        }


        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GHDeserializeObject<T>(this string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
        }

    }
}
