using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using Lib;

namespace Dto
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public class QueryDto
    {
        public QueryDto()
        {
            DateRange = DateRangeEnum.Other;
        }
        /// <summary> 
        /// 搜索内容
        /// </summary>
        public string Keyword { get; set; }
        
        DateRangeEnum dateRange;
        /// <summary>
        /// 日期范围
        /// </summary>
        public DateRangeEnum DateRange
        {
            get { return dateRange; }
            set
            {
                dateRange = value;
                var today = DateTime.Now.ToString("yyyy-MM-dd");
                CheckDateRange(value, today, today, out var dateS, out var dateE);
                startTime = dateS.ToString("yyyy-MM-dd");
                endTime = dateE.ToString("yyyy-MM-dd");
            }
        }

        private string startTime;
        private string endTime;
        /// <summary> 
        /// 开始时间
        /// </summary>
        public string StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                if (DateRange != DateRangeEnum.Other)
                {
                    return;
                }
                startTime = value;
            }
        }

        /// <summary> 
        /// 结束时间
        /// </summary>
        public string EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                if (DateRange != DateRangeEnum.Other)
                {
                    return;
                }
                endTime = value;
            }
        }

        /// <summary> 
        /// 结束时间，仅用于后端，前端请忽略。
        /// </summary>
        [JsonIgnore]
        public DateTime DateS { get { return DateTime.Parse($"{StartTime} 00:00:00.000"); } }

        /// <summary> 
        /// 结束时间，仅用于后端，前端请忽略。
        /// </summary>
        [JsonIgnore]
        public DateTime DateE { get { return DateTime.Parse($"{EndTime} 23:59:59.999"); } }

        /// <summary>
        /// 页码，从1开始。
        /// </summary>
        [Required]
        [Range(1, 100, ErrorMessage = "页数必须介于1-100")]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页记录数，最大1000。
        /// </summary>
        [Required]
        [Range(1, 1000, ErrorMessage = "每页记录数必须介于1-1000")]
        public int PageSize { get; set; } = int.MaxValue;


        public static void Today(out DateTime dateS,out DateTime dateE)
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            dateS = DateTime.Parse($"{date} 00:00:00.000");
            dateE = DateTime.Parse($"{date} 23:59:59.999");
        }

        public static void Yesterday(out DateTime dateS, out DateTime dateE)
        {
            var date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            dateS = DateTime.Parse($"{date} 00:00:00.000");
            dateE = DateTime.Parse($"{date} 23:59:59.999");
        }

        public static bool CheckDateRange(DateRangeEnum dateRange, string startDate, string endDate, out DateTime dateS, out DateTime dateE)
        {
            dateS = default;
            dateE = default;
            switch (dateRange)
            {
                case DateRangeEnum.Today:
                    Today(out dateS, out dateE);
                    break;
                case DateRangeEnum.Yesterday:
                    Yesterday(out dateS, out dateE);
                    break;
                case DateRangeEnum.ThisWeek:
                    DateTime.Now.GetDaysOfWeek(out dateS, out dateE);
                    break;
                case DateRangeEnum.LastWeek:
                    DateTime.Now.AddDays(-7).GetDaysOfWeek(out dateS, out dateE);
                    break;
                case DateRangeEnum.ThisMonth:
                    var dateMonthE = DateTime.Parse(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddSeconds(-1);
                    dateS = DateTime.Parse($"{DateTime.Now.ToString("yyyy-MM-01")} 00:00:00.000");
                    dateE = DateTime.Parse($"{dateMonthE.ToString("yyyy-MM-dd")} 23:59:59.999");
                    break;
                case DateRangeEnum.LastMonth:
                    var dateLastMonthE = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddSeconds(-1);
                    dateS = DateTime.Parse($"{DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01")} 00:00:00.000");
                    dateE = DateTime.Parse($"{dateLastMonthE.ToString("yyyy-MM-dd")} 23:59:59.999");
                    break;
                default:
                    if (!(startDate.IsStartDate(out dateS) && endDate.IsEndDate(out dateE)))
                    {
                        return false;
                    }
                    break;
            }            
            return true;
        }
    }
}
