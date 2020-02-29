using System;
using System.Globalization;

namespace Lib
{
    public static class Time
    {
        public static void GetWeekDays(int year,int week ,out DateTime startDate, out DateTime endDate)
        {
            var date = new DateTime(year, 1, 1,23,59,59).AddDays(week * 7);
            while(GetWeekOfYear(date) != week)
            {
                date = date.AddDays(-1);
            }
            startDate = date.AddDays(-7).AddSeconds(1);
            endDate = date;
        }
        

        /// <summary>
        /// 获取本周的开始结束日期，以及今天是星期几
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int GetDaysOfWeek(this DateTime date, out DateTime startDate, out DateTime endDate,bool isMondayFirst = true)
        {
            int intDaysOfWeek = (int)date.DayOfWeek;
            if (isMondayFirst)
            {
                if (intDaysOfWeek == 0)
                {
                    // 周的第一天是周一，将周日0换算为7
                    intDaysOfWeek = 7;
                }
            }
            else
            {
                intDaysOfWeek += 1;
            }

            startDate = DateTime.Parse(date.AddDays(1 - intDaysOfWeek).ToString("yyyy-MM-dd 00:00:00"));
            endDate = DateTime.Parse(startDate.AddDays(6).ToString("yyyy-MM-dd 23:59:59.999"));

            return intDaysOfWeek;
        }

        /// <summary>
        /// 本周是一年的第几周
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetWeekOfYear(DateTime value)
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(value, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            return weekOfYear;
        }

        public static string GetTimeDesc(int? year,int? month,int? week)
        {
            DateTime dateS, dateE;
            if (week > 0)
            {
                GetWeekDays(year.Value, week.Value, out dateS, out dateE);
                return string.Format("{0}-{1}", dateS.ToString("MM.dd"), dateE.ToString("MM.dd"));
            }
            else if (month > 0)
            {
                return string.Format("{0}.{1}", year,month);
            }

            return year.ToString();
        }

        /// <summary>
        /// 通过时间返回今天、昨天、前天
        /// </summary>
        /// <param name="language"></param>
        /// <param name="thisTime"></param>
        /// <returns></returns>
        public static string GetTimeStr(string language,DateTime thisTime)
        {
            try
            {
                var timeStr = thisTime.ToString("yyyy-MM-dd");
                var now = DateTime.Now;
                var days = (thisTime.Date - now.Date).Days;
                if (days == 0)
                {
                    timeStr = thisTime.ToString("HH:mm");
                }
                else if (days == -1)
                {
                    timeStr = language == "en" ? "Yesterday" : "昨天";
                }
                //else if (days == -2)
                //{
                //    timeStr = language == "en" ? "one day ago" : "前天";
                //}
                return timeStr;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
