using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Lib.Text
{
    public static class StringHelper
    {
        //---------------------------------------转换开始----------------------------------------------------------------

        #region  //bool类型转换
        /// <summary> 
        /// Author:
        /// Description: 转换为bool类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="del">可填充</param>
        /// <returns>成功:返回当前转换的数据
        /// 失败:返回默认值或填充的数据</returns>
        public static bool ToBool(this string str, bool del = default(bool))
        {
            bool result;
            return bool.TryParse(str, out result) ? result : del;
        }
        #endregion

        #region  //Int32类型转换
        /// <summary>
        /// Author:
        /// Description: 转换为Int32类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="del">可填充</param>
        /// <returns>成功:返回当前转换的数据
        /// 失败:返回默认值或填充的数据</returns>
        public static int ToInt32(this string str, int del = default(int))
        {
            int result;
            return int.TryParse(str, out result) ? result : del;
        }
        #endregion

        #region  //Int16类型转换 /// <summary>
        /// <summary>
        /// Author:
        /// Description: 转换为Int16类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="del">可填充</param>
        /// <returns>成功:返回当前转换的数据
        /// 失败:返回默认值或填充的数据</returns>
        public static short ToInt16(this string str, short del = default(short))
        {
            short result;
            return short.TryParse(str, out result) ? result : del;

        }
        #endregion

        #region  //Int64类型转换
        /// <summary>
        /// Author:
        /// Description: 转换为Int64类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="del">可填充</param>
        /// <returns>成功:返回当前转换的数据
        /// 失败:返回默认值或填充的数据</returns>
        public static long ToInt64(this string str, long del = default(long))
        {
            long result;
            return long.TryParse(str, out result) ? result : del;
        }
        #endregion

        #region  //Char类型转换
        /// <summary>
        /// Author:
        /// Description: 转换为Char类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="del">可填充</param>
        /// <returns>成功:返回当前转换的数据
        /// 失败:返回默认值或填充的数据</returns>
        public static char ToChar(this string str, char del = default(char))
        {
            char result;
            return char.TryParse(str, out result) ? result : del;
        }
        #endregion

        #region  //Byte类型转换
        /// <summary>
        /// Author:
        /// Description: 转换为Byte类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="del">可填充</param>
        /// <returns>成功:返回当前转换的数据
        /// 失败:返回默认值或填充的数据</returns>
        public static byte ToByte(this string str, byte del = default(byte))
        {
            byte result;
            return byte.TryParse(str, out result) ? result : del;
        }
        #endregion

        #region  //Double类型转换
        /// <summary>
        /// Author:
        /// Description: 转换为Double类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="del">可填充</param>
        /// <returns>成功:返回当前转换的数据
        /// 失败:返回默认值或填充的数据</returns>
        public static double ToDouble(this string str, double del = default(double))
        {
            double result;
            return double.TryParse(str, out result) ? result : del;
        }
        #endregion

        #region  //Float类型转换
        /// <summary>
        /// Author:
        /// Description: 转换为Float类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="del">可填充</param>
        /// <returns>成功:返回当前转换的数据
        /// 失败:返回默认值或填充的数据</returns>
        public static float ToFloat(this string str, float del = default(float))
        {
            float result;
            return float.TryParse(str, out result) ? result : del;
        }
        #endregion

        #region  //Decimal类型转换
        /// <summary>
        /// Author:
        /// Description: 转换为Decimal类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="del">可填充</param>
        /// <returns>成功:返回当前转换的数据
        /// 失败:返回默认值或填充的数据</returns>
        public static decimal ToDecimal(this string str, decimal del = default(decimal))
        {
            decimal result;
            return decimal.TryParse(str, out result) ? result : del;
        }
        #endregion

        #region  //Sbyte类型转换
        /// <summary>
        /// Author:
        /// Description: 转换为Sbyte类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="del">可填充</param>
        /// <returns>成功:返回当前转换的数据
        /// 失败:返回默认值或填充的数据</returns>
        public static sbyte ToSbyte(this string str, sbyte del = default(sbyte))
        {
            sbyte result;
            return sbyte.TryParse(str, out result) ? result : del;
        }
        #endregion

        #region  //Uint类型转换
        /// <summary>
        /// Author:
        /// Description: 转换为Uint类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="del">可填充</param>
        /// <returns>成功:返回当前转换的数据
        /// 失败:返回默认值或填充的数据</returns>
        public static uint ToUint(this string str, uint del = default(uint))
        {
            uint result;
            return uint.TryParse(str, out result) ? result : del;
        }
        #endregion

        #region  //Ulong类型转换
        /// <summary>
        /// Author:
        /// Description: 转换为Ulong类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="del">可填充</param>
        /// <returns>成功:返回当前转换的数据
        /// 失败:返回默认值或填充的数据</returns>
        public static ulong ToUlong(this string str, ulong del = default(ulong))
        {
            ulong result;
            return ulong.TryParse(str, out result) ? result : del;
        }
        #endregion

        #region  //Ushort类型转换
        /// <summary>
        /// Author:
        /// Description: 转换为Ushort类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="del">可填充</param>
        /// <returns>成功:返回当前转换的数据
        /// 失败:返回默认值或填充的数据</returns>
        public static ushort ToUshort(this string str, ushort del = default(ushort))
        {
            ushort result;
            return ushort.TryParse(str, out result) ? result : del;
        }
        #endregion

        #region//DateTime类型转换
        /// <summary>
        /// Author:
        /// Description: 转换为DateTime类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="del">可填充</param>
        /// <returns>成功:返回当前转换的数据
        /// 失败:返回默认值或填充的数据</returns>
        public static DateTime ToDateTime(this string str, DateTime del = default(DateTime))
        {
            DateTime result;
            return DateTime.TryParse(str, out result) ? result : del;
        }
        #endregion

        #region  //MD5类型64位转换
        /// <summary>
        /// Author:
        /// Description: 转换为64位MD5类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="del">可填充</param>
        /// <returns>成功:返回当前转换的数据
        /// 失败:返回默认值或填充的数据</returns>
        public static string ToMD5(this string str)
        {
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(str));
            md5.Clear();
            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// Author:
        /// Description: 加密md5算法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToEncryptMD5(this string str)
        {
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(str));
            str = Convert.ToBase64String(result);
            str = str.Substring(0, str.Length - 2);
            result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(str));
            return Convert.ToBase64String(result);
        }
        #endregion
        //---------------------------------------转换结束----------------------------------------------------------------

        //---------------------------------------验证开始----------------------------------------------------------------
       

        #region//验证当前字符串是否为空
        /// <summary>
        /// Author:
        /// Description:验证当前字符串是否为空  
        /// </summary>
        /// <param name="str"></param>
        /// <returns>成功:返回true;
        /// 失败:返回false</returns>
        public static bool IsNull(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        #endregion

        #region  //验证当前字符串是否为图片格式
        /// <summary>
        /// Author:
        /// Description:验证当前字符串是否为图片格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns>成功:返回true;
        /// 失败:返回false</returns>
        public static bool IsImage(this string str)
        {
            var data = @"^.(jpeg|jpg|gif|bmp|png)$";
            return Regex.IsMatch(str, data, RegexOptions.IgnoreCase) ? true : default(bool);
        }
        #endregion

        #region  //验证当前字符串是否都是中文
        /// <summary>
        /// Author:
        /// Description:验证当前字符串是否都是中文
        /// </summary>
        /// <param name="str"></param>
        /// <returns>成功:返回true;
        /// 失败:返回false</returns> 
        public static bool IsChinese(this string str)
        {
            var data = @"^[\u4e00-\u9fa5]+$";
            return Regex.IsMatch(str, data, RegexOptions.IgnoreCase) ? true : default(bool);
        }

        #endregion

        #region //验证当前字符串是否是邮箱
        /// <summary>
        /// Author:
        /// Description:验证当前字符串是否是邮箱
        /// </summary>
        /// <param name="str"></param>
        /// <returns>成功:返回true;
        /// 失败:返回false</returns> 
        public static bool IsEmail(this string str)
        {
            var data = @"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?";
            if (!string.IsNullOrWhiteSpace(str))
            {
                str = str.Trim();
            }
            return Regex.IsMatch(str, data) ? true : default(bool);
        }
        #endregion

        #region//验证当前字符串是否是字母——数字——中文
        /// <summary>
        /// Author:
        /// Description:验证当前字符串是否是字母——数字——中文
        /// </summary>
        /// <param name="str"></param>
        /// <returns>成功:返回true;
        /// 失败:返回false</returns> 
        public static bool IsEnglish_Chs_Numb(this string str)
        {
            var data = @"^[a-zA-Z0-9\u4e00-\u9fa5]*$";
            return Regex.IsMatch(str, data, RegexOptions.IgnoreCase) ? true : default(bool);
        }
        #endregion

        #region//验证当前字符串是否是字母——数字 
        /// <summary>
        /// Author:
        /// Description:验证当前字符串是否是字母——数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns>成功:返回true;
        /// 失败:返回false</returns> 
        public static bool IsEnglish_Numb(this string str)
        {
            var data = @"^[a-zA-Z0-9]+$";
            return Regex.IsMatch(str, data, RegexOptions.IgnoreCase) ? true : default(bool);
        }
        #endregion

        #region//验证当前字符串是否是数字
        /// <summary>
        /// Author:
        /// Description:验证当前字符串是否是数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns>成功:返回true;
        /// 失败:返回false</returns> 
        public static bool IsNumb(this string str)
        {
            var data = @"^[0-9]+$";
            return Regex.IsMatch(str, data, RegexOptions.IgnoreCase) ? true : default(bool);
        }
        #endregion

        #region  //验证当前字符串是否是邮政编码
        /// <summary>
        /// Author:
        /// Description:验证当前字符串是否是邮政编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns>成功:返回true;
        /// 失败:返回false</returns> 
        public static bool IsZipCode(this string str)
        {
            var data = @"[1-9]\d{5}(?!\d)";
            return Regex.IsMatch(str, data) ? true : default(bool);
        }
        #endregion

        #region  //验证当前字符串是否是QQ
        /// <summary>
        /// Author:
        /// Description:验证当前字符串是否是QQ
        /// </summary>
        /// <param name="str"></param>
        /// <returns>成功:返回true;
        /// 失败:返回false</returns> 
        public static bool IsQQ(this string str)
        {

            var data = @"[1-9][0-9]{4,}";
            return Regex.IsMatch(str, data) ? true : default(bool);
        }
        #endregion

        #region //验证当前字符串是不是Url
        /// <summary>
        /// Author:
        /// Description:验证当前字符串是不是Url
        /// </summary>
        /// <param name="str"></param>
        /// <returns>成功:返回true;
        /// 失败:返回false</returns> 
        public static bool IsUrl(this string str)
        {
            var data = @"[a-zA-z]+://[^\s]*";
            return Regex.IsMatch(str, data) ? true : default(bool);
        }
        #endregion

        #region //验证当前字符串是否为身份证号码
        /// <summary>
        /// Author:
        /// Description:验证当前字符串是否为身份证号码
        /// </summary>
        /// <param name="str"></param>
        /// <returns>成功:返回true;
        /// 失败:返回false</returns> 
        public static bool IsIdCard(this string str)
        {
            var data = @"^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$";
            return Regex.IsMatch(str, data) ? true : default(bool);
        }
        #endregion

        #region //验证当前字符串是否为手机号码
        /// <summary>
        /// Author:
        /// Description:验证当前字符串是否为手机号码
        /// </summary>
        /// <param name="str"></param>
        /// <returns>成功:返回true;
        /// 失败:返回false</returns> 
        public static bool IsPhone(this string str)
        {
            //var data = @"^1[3-9]\d{9}$";
            var data = @"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$";
            if (!string.IsNullOrWhiteSpace(str))
            {
                str = str.Trim();
            }
            return Regex.IsMatch(str, data) ? true : default(bool);
        }


        //
        public static bool IsPhone86(this string str)
        {
            var data = @"^((\\+86)|(86))?[1][3456789][0-9]{9}$";
            return Regex.IsMatch(str, data) ? true : default(bool);
        }
        #endregion

        #region //验证当前字符串是否为电话号码（可纯数字或数字+横杠）最低5位
        /// <summary>
        /// Author:
        /// Description:验证当前字符串是否为电话号码（可纯数字或数字+横杠）最低5位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsTel(this string str)
        {
            var data = @"([\d\-]+){5,}";
            return Regex.IsMatch(str, data) ? true : default(bool);
        }
        #endregion

        //---------------------------------------验证结束---------------------------------------------------------------- 

        //---------------------------------------文件目录扩展开始---------------------------------------------------------------- 
        #region//创建绝对路径
        /// <summary>
        /// Author:
        /// Description:创建绝对路径
        /// </summary>
        /// <param name="path">绝对路径(当前字符串)</param>
        public static void CreateDiskDirectory(this string path)
        {
            Directory.CreateDirectory(path);
        }
        #endregion

        

        #region //判断该路径是否存在
        /// <summary>
        /// Author:
        /// Description:判断该路径是否存在
        /// </summary>
        /// <param name="path">路径(当前字符串)</param>
        /// <returns></returns>
        public static bool DirectoryExists(this string path)
        {
            return Directory.Exists(path) ? true : default(bool);
        }

        #endregion

        #region//获取字符串扩展名
        /// <summary>
        /// Author:
        /// Description:获取字符串扩展名
        /// </summary>
        /// <param name="path">文件名(当前字符串)</param>
        /// <returns></returns>
        public static string GetExtension(this string path)
        {
            return Path.GetExtension(path);
        }
        #endregion
        //---------------------------------------文件目录扩展结束---------------------------------------------------------------- 








        //---------------------------------------字符串扩展开始---------------------------------------------------------------- 
        #region 字符串扩展

        /// <summary>
        /// 获得字符串的长度,一个汉字的长度为1
        /// </summary>
        public static int GetStringLength(string s)
        {
            if (!string.IsNullOrEmpty(s))
                return Encoding.Default.GetBytes(s).Length;

            return 0;
        }

        /// <summary>
        /// 获得字符串中指定字符的个数
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="c">字符</param>
        /// <returns></returns>
        public static int GetCharCount(string s, char c)
        {
            if (s == null || s.Length == 0)
                return 0;
            int count = 0;
            foreach (char a in s)
            {
                if (a == c)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// 获得指定顺序的字符在字符串中的位置索引
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="order">顺序</param>
        /// <returns></returns>
        public static int IndexOf(string s, int order)
        {
            return IndexOf(s, '-', order);
        }

        /// <summary>
        /// 获得指定顺序的字符在字符串中的位置索引
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="c">字符</param>
        /// <param name="order">顺序</param>
        /// <returns></returns>
        public static int IndexOf(string s, char c, int order)
        {
            int length = s.Length;
            for (int i = 0; i < length; i++)
            {
                if (c == s[i])
                {
                    if (order == 1)
                        return i;
                    order--;
                }
            }
            return -1;
        }

        #region 分割字符串

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="splitStr">分隔字符串</param>
        /// <returns></returns>
        public static string[] SplitString(string sourceStr, string splitStr)
        {
            if (string.IsNullOrEmpty(sourceStr) || string.IsNullOrEmpty(splitStr))
                return new string[0] { };

            if (sourceStr.IndexOf(splitStr) == -1)
                return new string[] { sourceStr };

            if (splitStr.Length == 1)
                return sourceStr.Split(splitStr[0]);
            else
                return Regex.Split(sourceStr, Regex.Escape(splitStr), RegexOptions.IgnoreCase);

        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <returns></returns>
        public static string[] SplitString(string sourceStr)
        {
            return SplitString(sourceStr, ",");
        }

        #endregion

        #region 截取字符串

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="startIndex">开始位置的索引</param>
        /// <param name="length">子字符串的长度</param>
        /// <returns></returns>
        public static string SubString(string sourceStr, int startIndex, int length)
        {
            if (!string.IsNullOrEmpty(sourceStr))
            {
                if (sourceStr.Length >= (startIndex + length))
                    return sourceStr.Substring(startIndex, length);
                else
                    return sourceStr.Substring(startIndex);
            }

            return "";
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="length">子字符串的长度</param>
        /// <returns></returns>
        public static string SubString(string sourceStr, int length)
        {
            return SubString(sourceStr, 0, length);
        }

        #endregion

        #region 移除前导/后导字符串

        /// <summary>
        /// 移除前导字符串
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="trimStr">移除字符串</param>
        /// <returns></returns>
        public static string TrimStart(string sourceStr, string trimStr)
        {
            return TrimStart(sourceStr, trimStr, true);
        }

        /// <summary>
        /// 移除前导字符串
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="trimStr">移除字符串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public static string TrimStart(string sourceStr, string trimStr, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(sourceStr))
                return string.Empty;

            if (string.IsNullOrEmpty(trimStr) || !sourceStr.StartsWith(trimStr, ignoreCase, CultureInfo.CurrentCulture))
                return sourceStr;

            return sourceStr.Remove(0, trimStr.Length);
        }

        /// <summary>
        /// 移除后导字符串
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="trimStr">移除字符串</param>
        /// <returns></returns>
        public static string TrimEnd(string sourceStr, string trimStr)
        {
            return TrimEnd(sourceStr, trimStr, true);
        }

        /// <summary>
        /// 移除后导字符串
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="trimStr">移除字符串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public static string TrimEnd(string sourceStr, string trimStr, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(sourceStr))
                return string.Empty;

            if (string.IsNullOrEmpty(trimStr) || !sourceStr.EndsWith(trimStr, ignoreCase, CultureInfo.CurrentCulture))
                return sourceStr;

            return sourceStr.Substring(0, sourceStr.Length - trimStr.Length);
        }

        /// <summary>
        /// 移除前导和后导字符串
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="trimStr">移除字符串</param>
        /// <returns></returns>
        public static string Trim(string sourceStr, string trimStr)
        {
            return Trim(sourceStr, trimStr, true);
        }

        /// <summary>
        /// 移除前导和后导字符串
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="trimStr">移除字符串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public static string Trim(string sourceStr, string trimStr, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(sourceStr))
                return string.Empty;

            if (string.IsNullOrEmpty(trimStr))
                return sourceStr;

            if (sourceStr.StartsWith(trimStr, ignoreCase, CultureInfo.CurrentCulture))
                sourceStr = sourceStr.Remove(0, trimStr.Length);

            if (sourceStr.EndsWith(trimStr, ignoreCase, CultureInfo.CurrentCulture))
                sourceStr = sourceStr.Substring(0, sourceStr.Length - trimStr.Length);

            return sourceStr;
        }

        #endregion
        #endregion

        //---------------------------------------字符串扩展结束---------------------------------------------------------------- 



    }
}
