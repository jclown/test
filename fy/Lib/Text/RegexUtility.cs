using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Lib.Text
{
    /// <summary>
    /// 正则表达扩展
    /// </summary>
    public class RegexUtility
    {
        public static RegexOptions compiledIgnoreCaseExplicitCapture = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

        private static Dictionary<string, Regex> regexCache = new Dictionary<string, Regex>();

        /// <summary>
        /// 构建编译，忽略大小写，忽略无组名的正则表达式
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Regex CreateRegex(string pattern)
        {
            Regex reg;
            if (!regexCache.TryGetValue(pattern, out reg))
            {
                reg = new Regex(pattern, compiledIgnoreCaseExplicitCapture);
                regexCache.Add(pattern, reg);
            }
            return reg;
        }

        /// <summary>
        /// 获取匹配的第一个组名里的所有匹配项
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="content"></param>
        /// <param name="ms"></param>
        public static void GetFirstGroups(Regex reg, string content, IList<string> ms)
        {
            var mc = reg.Matches(content);
            var groupName = GetFirstGroupName(reg);
            foreach (Match m in mc)
            {
                var cs = m.Groups[groupName].Captures;
                foreach (var c in cs)
                {
                    ms.Add(c.ToString());
                }
            }
        }

        /// <summary>
        /// 获取匹配的第一个组名里的所有匹配项
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static IList<string> GetFirstGroups(Regex reg, string content)
        {
            var list = new List<string>();
            GetFirstGroups(reg, content, list);
            return list;
        }

        /// <summary>
        /// 获取第一个组名里的第一个匹配项
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetFirstMatch(Regex reg, string content)
        {
            var mc = reg.Matches(content);
            if (mc.Count == 0) return string.Empty;

            var groupName = GetFirstGroupName(reg);
            return string.IsNullOrEmpty(groupName) ? mc[0].Value : mc[0].Groups[groupName].Value.Trim();
        }

        /// <summary>
        /// 获取第一个组名
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        public static string GetFirstGroupName(Regex reg)
        {
            return reg.GetGroupNames().Length >= 2 ? reg.GetGroupNames()[1] : string.Empty;
        }

        /// <summary>
        /// 利用正则匹配文本，同时装配到传进来的对象中
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="content"></param>
        /// <param name="obj"></param>
        public static void Assemble(Regex reg, string content, object obj)
        {
            if (reg == null || string.IsNullOrEmpty(content)) return;

            var mc = reg.Matches(content);
            if (mc.Count == 0) return;

            var groupNames = reg.GetGroupNames();
            Assemble(mc[0], groupNames, obj);
        }

        public static void Assemble(Match m, string[] groupNames, object obj)
        {
            var properties = obj.GetType().GetProperties();

            foreach (var p in properties)
            {
                if (groupNames.Contains(p.Name))
                {
                    if (m.Groups[p.Name].Success)
                    {
                        var capture = m.Groups[p.Name].Value;
                        object value;

                        if (p.PropertyType == typeof(string))
                            value = capture;
                        else if (p.PropertyType == typeof(DateTime))
                            value = Convert.ToDateTime(capture);
                        else
                            value = Convert.ChangeType(capture, p.PropertyType);

                        p.SetValue(obj, value, null);

                    }
                }
            }
        }
    }
}
