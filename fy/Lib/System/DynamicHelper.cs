using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Lib
{
    public static class DynamicHelper
    {
        public static List<ExpandoObject> ToWritableDynamicList(List<dynamic> records)
        {
            if (records.Count == 0) return new List<ExpandoObject>();
            var properties = records[0].GetType().GetProperties();

            List<ExpandoObject> list = new List<ExpandoObject>();
            foreach (var item in records)
            {
                dynamic newItem = new ExpandoObject();
                var dic = (IDictionary<string, object>)newItem;
                foreach (System.Reflection.PropertyInfo p in properties)
                {
                    dic[p.Name] = p.GetValue(item);
                }
                list.Add(newItem);
            }

            return list;
        }

        public static dynamic ToDynamic(string value, string split = ",", StringSplitOptions options = default(StringSplitOptions))
        {
            var dictList = new Dictionary<string, string>();
            var list = value.Split(new string[] { split }, options).ToList();

            dynamic newItem = new ExpandoObject();
            var dic = (IDictionary<string, object>)newItem;

            foreach (var item in list)
            {
                var itemValues = item.Split('=');
                dic[itemValues[0]] = itemValues[1];
            }

            return newItem;
        }

        public static List<dynamic> ToDynamicList(string value, string split = ",", StringSplitOptions options = default(StringSplitOptions))
        {
            var dictList = new Dictionary<string, string>();
            var list = value.Split(new string[] { split }, options).ToList();

            dynamic newItem = new ExpandoObject();
            var dic = (IDictionary<string, object>)newItem;

            foreach (var item in list)
            {
                var itemValues = item.Split('=');
                dic[itemValues[0]] = itemValues[1];
            }

            return newItem;
        }
    }

}
