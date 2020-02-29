using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
   
     public static class NumberAndStringExtensions
    {

        private const string characters = "abcdefghijkl012mnopqrstuvwxyz34ABCDEFGHIJKL789MNOPQRSTUVWXYZ56";


        private const int CharSalt = 9999;

        /// <summary>
        /// 数字转换字符串形式  拓展方法
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToShortString(this long @this)
        {
            List<string> result = new List<string>();
            long t = @this+ CharSalt; 
            while (t > 0)
            {
                var mod = t % characters.Length;
                t = Math.Abs(t / characters.Length);
                var character = characters[Convert.ToInt32(mod)].ToString();
                result.Insert(0, character);
            }

            return string.Join("", result.ToArray());
        }

        /// <summary>
        /// 字符串转换为数字形式  拓展方法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long ToShortLong (this string @this)
        {
            long result = 0;
            int j = 0;
            char[] arr = @this.ToCharArray();
            Array.Reverse(arr);
            foreach (char ch in new string(arr))
            {
                if (characters.Contains(ch.ToString()))
                {
                    result += characters.IndexOf(ch) * ((long)Math.Pow(characters.Length, j));
                    j++;
                }
            }
            return result- CharSalt;
        }



        /// <summary>
        /// 数字转换字符串形式  拓展方法
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToShortString(this int @this)
        {
            List<string> result = new List<string>();
            long t = (long)(@this + CharSalt);
            while (t > 0)
            {
                var mod = t % characters.Length;
                t = Math.Abs(t / characters.Length);
                var character = characters[Convert.ToInt32(mod)].ToString();
                result.Insert(0, character);
            }

            return string.Join("", result.ToArray());
        }

        /// <summary>
        /// 字符串转换为数字形式  拓展方法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToShortInt(this string @this)
        {
            long result = 0;
            int j = 0;
            char[] arr = @this.ToCharArray();
            Array.Reverse(arr);
            foreach (char ch in new string(arr))
            {
                if (characters.Contains(ch.ToString()))
                {
                    result += characters.IndexOf(ch) * ((long)Math.Pow(characters.Length, j));
                    j++;
                }
            }
            return (int)(result - CharSalt);
        }

    }
}
