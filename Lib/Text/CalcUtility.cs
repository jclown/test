using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lib.Text
{
    public class CalcUtility
    {
        public static string GetCalcBracket(string str)
        {
            /*
               * 命题：计算 加 减 乘 除，含括号
               */
            //计算乘法
            str = str.Replace(" ", "");
            str = CalcBracket(str);
            return str;
        }
        public static string GetCalc(string str)
        {
            /*
               * 命题：计算 加 减 乘 除，不含括号
               */
            //var str = "8*8*8-100*2";
            //计算乘法
            str = str.Replace(" ", "");
            str = Calc(str, '*');
            str = Calc(str, '/');
            str = Calc(str, '+');
            str = Calc(str, '-');
            return str;
        }
        private static string Calc(string exp, char sign)
        {

            try
            {
                var r = new Regex(string.Format(@"(\d+[.]\d+|\d+)\{0}(\d+[.]\d+|\d+)", sign));
                var exps = r.Matches(exp);
                if (exps.Count == 0) return exp;
                exp = exps.Cast<Match>().Aggregate(exp, (current, p) => current.Replace(p.ToString(), Operate(p.ToString(), sign).ToString()));
                exp = Calc(exp, sign);
                return exp;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private static string CalcBracket(string exp)
        {
            try
            {

                var r = new Regex(@"(\((.+?)\))");
                var exps = r.Matches(exp);
                if (exps.Count == 0)
                {
                    exp = Calc(exp, '*');
                    exp = Calc(exp, '/');
                    exp = Calc(exp, '+');
                    exp = Calc(exp, '-');
                    return exp;
                }
                else
                {
                    exp = exps.Cast<Match>().Aggregate(exp, (current, p) => current.Replace(p.ToString(), CalcBracket(p.ToString().TrimStart('(').TrimEnd(')'))));
                    exp = CalcBracket(exp);
                    return exp;
                }
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        private static double Operate(string exp, char sign)
        {
            var arr = exp.Split(sign);
            double result = 0;
            var numA = Double.Parse(arr[0]); //NumberHelper.FormatDouble(arr[0]);
            var numB = Double.Parse(arr[1]); //NumberHelper.FormatDouble(arr[1]);
            switch (sign)
            {
                case '+':
                    result = numA + numB;
                    break;
                case '-':
                    result = numA - numB;
                    break;
                case '*':
                    result = numA * numB;
                    break;
                case '/':
                    result = numB != 0 ? numA / numB : 0;
                    break;
            }
            return result;
        }


        #region 几又几分之几 减号 换加号
        public static string GetCalc1(string str)
        {
            /*
               * 命题：计算 加 减 乘 除，不含括号
               */
            //var str = "8*8*8-100*2";
            //计算乘法
            str = str.Replace(" ", "");
            str = Calc1(str, '*');
            str = Calc1(str, '/');
            str = Calc1(str, '+');
            str = Calc1(str, '-');
            return str;
        }
        private static string Calc1(string exp, char sign)
        {

            try
            {
                var r = new Regex(string.Format(@"(\d+[.]\d+|\d+)\{0}(\d+[.]\d+|\d+)", sign));
                var exps = r.Matches(exp);
                if (exps.Count == 0) return exp;
                exp = exps.Cast<Match>().Aggregate(exp, (current, p) => current.Replace(p.ToString(), Operate1(p.ToString(), sign).ToString()));
                exp = Calc1(exp, sign);
                return exp;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        private static double Operate1(string exp, char sign)
        {
            var arr = exp.Split(sign);
            double result = 0;
            var numA = Double.Parse(arr[0]); //NumberHelper.FormatDouble(arr[0]);
            var numB = Double.Parse(arr[1]); //NumberHelper.FormatDouble(arr[1]);
            switch (sign)
            {
                case '+':
                    result = numA + numB;
                    break;
                case '-':
                    result = numA + numB;
                    break;
                case '*':
                    result = numA * numB;
                    break;
                case '/':
                    result = numB != 0 ? numA / numB : 0;
                    break;
            }
            return result;
        }

        #endregion
    }
}
