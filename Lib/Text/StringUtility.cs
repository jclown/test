using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Lib.Text
{
    public static class StringUtility
    {
        /// <summary>
        /// 将字节数组转换为十六进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] data)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("X2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 转为MYSQL IN 查寻格式
        /// </summary>
        /// <returns></returns>
        public static string ToMySqlInString(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            var strs = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(",", strs.Select(c => "'" + c + "'").ToArray());
        }

        public static string RandomString(int size)
        {
            int number;
            char code;
            string randomStr = string.Empty;

            System.Random random = new Random();

            for (int i = 0; i < size; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));

                randomStr += code.ToString();
            }
            return randomStr;
        }



        #region 将HTML标签转换
        /// <summary>
        /// 将HTML标签转换
        /// </summary>
        /// <param name="html"></param>
        public static string EscapeHtml(string html)
        {
            html = html.Replace("&amp;", "&");
            html = html.Replace("\\\\", "\\");
            html = html.Replace("\"", "\\\"");
            html = html.Replace("&lt;", "<");
            html = html.Replace("&gt;", ">");
            return html;
        }
        #endregion

        #region 判断List是否为空
        public static bool IsNullOrEmpty<T>(T[] list)
        {
            return list == null || list.Length <= 0;
        }
        public static bool IsNullOrEmpty<T>(List<T> list)
        {
            return list == null || list.Count <= 0;
        }
        public static bool IsNullOrEmpty<t1, t2>(Dictionary<t1, t2> list)
        {
            return list == null || list.Count <= 0;
        }
        #endregion

        #region 01.截取文本中指定段落
        /// <summary>
        /// 截取文本中指定段落
        /// </summary>
        /// <param name="text">原始文本</param>
        /// <param name="start">起始匹配串</param>
        /// <param name="end">结束匹配串</param>
        /// <returns></returns>
        public static string Intercept(string text, string start, string end)
        {
            string segment = null;
            int startPos = text.IndexOf(start);
            if (startPos != -1)
            {
                startPos += start.Length;
                if (end == null)
                {
                    segment = text.Substring(startPos);
                }
                else
                {
                    int endPos = text.IndexOf(end, startPos);
                    segment = endPos != -1 ? text.Substring(startPos, endPos - startPos) : text.Substring(startPos);
                }
            }
            return segment;
        }
        #endregion


        #region Microsoft

        public static TValue As<TValue>(this string value)
        {
            return value.As<TValue>(default(TValue));
        }

        public static TValue As<TValue>(this string value, TValue defaultValue)
        {
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));
                if (converter.CanConvertFrom(typeof(string)))
                {
                    return (TValue)converter.ConvertFrom(value);
                }
                converter = TypeDescriptor.GetConverter(typeof(string));
                if (converter.CanConvertTo(typeof(TValue)))
                {
                    return (TValue)converter.ConvertTo(value, typeof(TValue));
                }
            }
            catch (Exception)
            {
            }
            return defaultValue;
        }

        /// <summary>
        /// 将字符串true或者false(不区分大小写)转换成bool类型，转换失败返回false
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AsBool(this string value)
        {
            return value.As<bool>(false);
        }

        /// <summary>
        /// 将字符串true或者false(不区分大小写)转换成bool类型，转换失败返回defaultValue
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AsBool(this string value, bool defaultValue)
        {
            return value.As<bool>(defaultValue);
        }

        /// <summary>
        /// 将字符串表示的的时间格式转换相对应的DateTime，转换失败返回DateTime.MinValue
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AsDateTime(this string value)
        {
            return value.As<DateTime>();
        }

        /// <summary>
        /// 将字符串表示的的时间格式转换相对应的DateTime，转换失败返回defaultValue
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AsDateTime(this string value, DateTime defaultValue)
        {
            return value.As<DateTime>(defaultValue);
        }

        public static decimal AsDecimal(this string value)
        {
            return value.As<decimal>();
        }

        public static decimal AsDecimal(this string value, decimal defaultValue)
        {
            return value.As<decimal>(defaultValue);
        }

        public static float AsFloat(this string value)
        {
            return value.As<float>();
        }

        public static float AsFloat(this string value, float defaultValue)
        {
            return value.As<float>(defaultValue);
        }

        public static int AsInt(this string value)
        {
            return value.As<int>();
        }

        public static int AsInt(this string value, int defaultValue)
        {
            return value.As<int>(defaultValue);
        }
        /// <summary>
        /// 相当于String.IsNullOrEmpty(value)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        #endregion

        #region Extension

        public static double AsDouble(this string value)
        {
            return value.As<double>();
        }

        public static double AsDouble(this string value, double defaultValue)
        {
            return value.As<double>(defaultValue);
        }

        public static long AsLong(this string value)
        {
            return value.As<long>();
        }

        public static long AsLong(this string value, long defaultValue)
        {
            return value.As<long>(defaultValue);
        }

        /// <summary>
        /// 将字符串转换成相对应的Guid，转换失败返回Guid.Empty (00000000-0000-0000-0000-000000000000)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Guid AsGuid(this string value)
        {
            return value.As<Guid>();
        }
        /// <summary>
        /// 将字符串转换成相对应的Guid，转换失败返回defaultValue
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Guid AsGuid(this string value, Guid defaultValue)
        {
            return value.As<Guid>(defaultValue);
        }
        /// <summary>
        /// 将字符串表示的的时间格式转换相对应的DateTime，转换失败返回TimeSpan.MinValue(00:00:00)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TimeSpan AsTimeSpan(this string value)
        {
            return value.As<TimeSpan>();
        }

        /// <summary>
        /// 将字符串表示的的时间格式转换相对应的DateTime，转换失败返回defaultValue
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TimeSpan AsTimeSpan(this string value, TimeSpan defaultValue)
        {
            return value.As<TimeSpan>(defaultValue);
        }

        /// <summary>
        /// 格式化字符串，等效于String.Format(value,args)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Format(this string value, params object[] args)
        {
            return string.Format(value, args);
        }

        #endregion

        //<script([\s\S]+?)/script>|<style([\s\S]+?)/style>|<!--.*?-->|</?[^>]*?/?>|\s+|\0|</|>
        //<script([\s\S]+?)/script>|<style([\s\S]+?)/style>|<!--.*?-->|</?[^>]*?/?>|\0|</|>|&nbsp;|\s{2,}
        static Regex cleanHTMLWithOutSpaceRegex = RegexUtility.CreateRegex(@"<script([\s\S]+?)/script>|<style([\s\S]+?)/style>|<!--.*?-->|</?[^>]*?/?>|\0|</|>");
        static Regex htmlRegex = RegexUtility.CreateRegex(@"<script([\s\S]+?)/script>|<style([\s\S]+?)/style>|<!--.*?-->|</?[^>]*?/?>|\s+|\0|</|>");
        static Regex htmlRegexWithspacing = RegexUtility.CreateRegex(@"<script([\s\S]+?)/script>|<style([\s\S]+?)/style>|<!--.*?-->|</?[^>]*?/?>|\0|</|>|&nbsp;|\s{2,}");


        #region Common
        /// <summary>
        /// 去除所有html标签
        /// </summary>
        public static string ClearAllHtmlTag(this string html)
        {
            if (string.IsNullOrEmpty(html)) return html;

            html = System.Web.HttpUtility.HtmlDecode(html);
            return htmlRegex.Replace(html, "");
        }

        /// <summary>
        /// 去除所有html标签，但不会清除空格
        /// </summary>
        public static string ClearAllHtmlTagWithOutSpace(this string html)
        {
            if (string.IsNullOrEmpty(html)) return html;

            html = System.Web.HttpUtility.HtmlDecode(html);
            return cleanHTMLWithOutSpaceRegex.Replace(html, "");
        }

        /// <summary>
        /// 去除所有html标签,&nbsp;以及换行
        /// </summary>
        public static string ClearAllHtmlNospacingTag(this string html)
        {
            if (string.IsNullOrEmpty(html)) return html;
            html = System.Web.HttpUtility.HtmlDecode(html);
            return htmlRegexWithspacing.Replace(html, "");
        }

        public static string SliceWithLength(this string s, int start, int maxLength)
        {
            int length = Math.Min(maxLength, s.Length - start);
            if (!string.IsNullOrEmpty(s) && (length > 0))
            {
                return s.Substring(start, length);
            }
            return null;
        }



        /// <summary>
        /// 截断字符串(按length)
        /// </summary>
        /// <param name="content"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GetContentByLen(this string content, int len)
        {
            return GetContentByLen(content, len, "");
        }

        /// <summary>
        /// 截断字符串(按length)
        /// </summary>
        /// <param name="content"></param>
        /// <param name="len"></param>
        /// <param name="etcString">截取字段后跟的省略字符</param>
        /// <returns></returns>
        public static string GetContentByLen(this string content, int len, string etcString)
        {
            if (content == null) return string.Empty;
            if (len < 1) { return content; }
            if (content.Length > len)
            {
                content = content.Substring(0, len) + etcString;
            }
            return content;
        }

        /// <summary>
        /// 截取字符串(按byte)
        /// </summary>
        /// <param name="mText">要截取的字串</param>
        /// <param name="byteCount">长度</param>
        /// <returns>被截取过的字串</returns>
        public static string GetContentByByteLen(this string mText, int byteCount)
        {
            return GetContentByByteLen(mText, byteCount, "");
        }

        /// <summary>
        /// 截取字符串(按byte)
        /// </summary>
        /// <param name="mText">要截取的字串</param>
        /// <param name="byteCount">截取长度</param>
        /// <param name="etcString">截取字段后跟的省略字符</param>
        /// <returns>被截取过的字串</returns>
        public static string GetContentByByteLen(this string mText, int byteCount, string etcString)
        {
            if (byteCount < 4)
                return mText;

            if (System.Text.Encoding.Default.GetByteCount(mText) <= byteCount)
            {
                return mText;
            }
            else
            {
                byte[] txtBytes = System.Text.Encoding.Default.GetBytes(mText);
                byte[] newBytes = new byte[byteCount - 4];

                for (int i = 0; i < byteCount - 4; i++)
                {
                    newBytes[i] = txtBytes[i];
                }
                string OutPut = System.Text.Encoding.Default.GetString(newBytes) + etcString;
                if (OutPut.EndsWith("?" + etcString) == true)
                {
                    OutPut = OutPut.Substring(0, OutPut.Length - 4);
                    OutPut += etcString;
                }
                return OutPut;
            }
        }

        /// <summary>
        /// 获取字符串的长度(按byte)
        /// </summary>
        /// <param name="mText">字符串</param>
        /// <returns>字符串长度</returns>
        public static Int32 GetContentByteLen(this string mText)
        {
            return System.Text.Encoding.Default.GetByteCount(mText);
        }
        #endregion

        #region 半角，全角转换
        /// <summary>
        /// 转全角的函数(SBC case)
        /// 任意字符串
        /// 半角字符串
        /// 
        /// 全角空格为12288，半角空格为32
        /// 其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 转半角的函数(DBC case)
        /// 任意字符串
        /// 半角字符串
        /// 
        /// 全角空格为12288，半角空格为32
        /// 其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToDBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion


        #region 字符串转集合
        /// <summary>
        /// 字符串转集合
        /// 目前仅支持string和int类型
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="str">需要转换的字符串以“,”隔开</param>
        /// <returns>相应数据类型T的集合</returns>
        public static List<T> StrToList<T>(string str, string splitchar = ",")
        {
            List<T> lists = new List<T>();
            string[] strs;
            strs = str.Split(new string[] { splitchar }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in strs)
            {
                if (typeof(T) == typeof(int))
                {
                    int n = int.Parse(s);
                    object obj = (object)n;
                    lists.Add((T)obj);
                }
                else if (typeof(T) == typeof(string))
                {
                    object obj = (object)s;
                    lists.Add((T)obj);
                }
                else if (typeof(T) == typeof(decimal))
                {
                    decimal n = decimal.Parse(s);
                    object obj = (object)n;
                    lists.Add((T)obj);
                }
            }
            return lists;
        }
        #endregion

        #region 字符串转数组
        /// <summary>
        /// 字符串转数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="splitchar"></param>
        /// <returns></returns>
        public static string[] StrArry(string str, string splitchar = ",")
        {
            return str.Split(new string[] { splitchar }, StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion


        #region 把数组转换为一个用逗号分隔的字符串 ，以便于用in+String 查询 
        /// <summary>
        /// 把数组转换为一个用逗号分隔的字符串 ，以便于用in+String 查询 
        /// </summary>
        /// <param name="strarry"></param>
        /// <param name="splitchar"></param>
        /// <returns></returns>
        public static string converToString(string[] strarry, string splitchar = ",")
        {
            if (strarry != null)
            {
                return string.Join(splitchar, strarry);
            }

            return "";
        }
        #endregion

        #region 把list<string>转换为一个用逗号分隔的字符串
        /// <summary>
        /// 把list<string>转换为一个用逗号分隔的字符串
        /// </summary>
        /// <param name="list"></param>
        /// <param name="splitchar"></param>
        /// <returns></returns>
        public static string listStrToString(List<string> list, string splitchar = ",")
        {
            if (list != null)
            {
                string[] strarry = list.ToArray();
                if (strarry.Length > 0)
                {
                    return string.Join(splitchar, strarry);
                }

            }
            return "";
        }
        #endregion


        #region 把list<int>转换为一个用逗号分隔的字符串
        /// <summary>
        /// 把list<int>转换为一个用逗号分隔的字符串
        /// </summary>
        /// <param name="list"></param>
        /// <param name="splitchar"></param>
        /// <returns></returns>
        public static string listIntToString(List<int> list, string splitchar = ",")
        {
            if (list != null)
            {
                int[] strarry = list.ToArray();
                if (strarry.Length > 0)
                {
                    return string.Join(splitchar, strarry);
                }

            }
            return "";
        }
        #endregion


        /// <summary>
        /// 根据userAgent获取浏览器版本
        /// </summary>
        /// <param name="userAgent"></param>
        /// <param name="browserName"></param>
        /// <param name="ver"></param>
        /// <returns></returns>
        public static string GetBrowserName(string userAgent, out string browserName, out string ver)
        {
            string fullBrowserName = string.Empty;
            browserName = string.Empty;
            ver = string.Empty;
            // IE
            string regexStr = @"msie (?<ver>[\d.]+)";
            Regex r = new Regex(regexStr, RegexOptions.IgnoreCase);
            Match m = r.Match(userAgent);
            if (m.Success)
            {
                browserName = "IE";
                ver = m.Groups["ver"].Value;
                fullBrowserName = string.Format("{0} {1}", browserName, ver);
                return fullBrowserName;
            }
            // Firefox
            regexStr = @"firefox\/([\d.]+)";
            r = new Regex(regexStr, RegexOptions.IgnoreCase);
            m = r.Match(userAgent);
            if (m.Success)
            {
                browserName = "IE";
                ver = m.Groups["ver"].Value;
                fullBrowserName = string.Format("{0} {1}", browserName, ver);
                return fullBrowserName;
            }
            // Chrome
            regexStr = @"chrome\/([\d.]+)";
            r = new Regex(regexStr, RegexOptions.IgnoreCase);
            m = r.Match(userAgent);
            if (m.Success)
            {
                browserName = "IE";
                ver = m.Groups["ver"].Value;
                fullBrowserName = string.Format("{0} {1}", browserName, ver);
                return fullBrowserName;
            }
            // Opera
            regexStr = @"opera.([\d.]+)";
            r = new Regex(regexStr, RegexOptions.IgnoreCase);
            m = r.Match(userAgent);
            if (m.Success)
            {
                browserName = "IE";
                ver = m.Groups["ver"].Value;
                fullBrowserName = string.Format("{0} {1}", browserName, ver);
                return fullBrowserName;
            }
            // Safari
            regexStr = @"version\/([\d.]+).*safari";
            r = new Regex(regexStr, RegexOptions.IgnoreCase);
            m = r.Match(userAgent);
            if (m.Success)
            {
                browserName = "IE";
                ver = m.Groups["ver"].Value;
                fullBrowserName = string.Format("{0} {1}", browserName, ver);
                return fullBrowserName;
            }
            return fullBrowserName;
        }


        /// <summary>
        /// 根据userAgent获取浏览器版本
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static string GetBrowserName(string userAgent)
        {
            if (string.IsNullOrWhiteSpace(userAgent)) return "";
            string fullBrowserName = "Other";
            string browserName = string.Empty;
            string ver = string.Empty;
            // IE
            string regexStr = @"msie (?<ver>[\d.]+)";
            Regex r = new Regex(regexStr, RegexOptions.IgnoreCase);
            Match m = r.Match(userAgent);
            if (m.Success)
            {
                browserName = "IE";
                ver = m.Groups["ver"].Value;
                fullBrowserName = string.Format("{0} {1}", browserName, ver);
                return fullBrowserName;
            }
            // Firefox
            regexStr = @"firefox\/([\d.]+)";
            r = new Regex(regexStr, RegexOptions.IgnoreCase);
            m = r.Match(userAgent);
            if (m.Success)
            {
                browserName = "Firefox";
                ver = m.Groups["ver"].Value;
                fullBrowserName = string.Format("{0} {1}", browserName, ver);
                return fullBrowserName;
            }
            // Chrome
            regexStr = @"chrome\/([\d.]+)";
            r = new Regex(regexStr, RegexOptions.IgnoreCase);
            m = r.Match(userAgent);
            if (m.Success)
            {
                browserName = "Chrome";
                ver = m.Groups["ver"].Value;
                fullBrowserName = string.Format("{0} {1}", browserName, ver);
                return fullBrowserName;
            }
            // Opera
            regexStr = @"opera.([\d.]+)";
            r = new Regex(regexStr, RegexOptions.IgnoreCase);
            m = r.Match(userAgent);
            if (m.Success)
            {
                browserName = "Opera";
                ver = m.Groups["ver"].Value;
                fullBrowserName = string.Format("{0} {1}", browserName, ver);
                return fullBrowserName;
            }
            // Safari
            regexStr = @"version\/([\d.]+).*safari";
            r = new Regex(regexStr, RegexOptions.IgnoreCase);
            m = r.Match(userAgent);
            if (m.Success)
            {
                browserName = "Safari";
                ver = m.Groups["ver"].Value;
                fullBrowserName = string.Format("{0} {1}", browserName, ver);
                return fullBrowserName;
            }
            return fullBrowserName;
        }

    }
}
