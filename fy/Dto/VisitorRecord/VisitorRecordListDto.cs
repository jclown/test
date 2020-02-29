using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Dto.VisitorRecord
{
    public class VisitorRecordListDto
    {
        /// <summary> 
        /// 访客标识
        /// </summary>
        public string VisitorTag { get; set; }
        /// <summary>
        /// 微信昵称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 微信头像
        /// </summary>
        public string ProfilePhotoPath { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int BrowserCount { get; set; }

        /// <summary>
        /// 获取LongTimeEnum
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static LongTimeEnum GetLongTimeType(DateTime dateTime)
        {
            if (dateTime < DateTime.Now.AddMonths(-3)) return LongTimeEnum.ThreeMonthAgo;
            if (dateTime < DateTime.Now.AddMonths(-2)) return LongTimeEnum.TwoMonthAgo;
            if (dateTime < DateTime.Now.AddMonths(-1)) return LongTimeEnum.AMonthAgo;
            if (dateTime < DateTime.Now.AddDays(-7)) return LongTimeEnum.AWeekAgo;
            if (dateTime < DateTime.Now.AddDays(-2)) return LongTimeEnum.TwoDaysAgo;
            if (dateTime < DateTime.Now.AddDays(-1)) return LongTimeEnum.ADaysAgo;
            if (dateTime.Date == DateTime.Today) return LongTimeEnum.ToDay;
            return LongTimeEnum.ToDay;
        }

        /// <summary>
        /// 什么时间
        /// </summary>
        public LongTimeEnum? LongTimeType
        {
            get
            {
                return GetLongTimeType(this.LastTime);
            }
        }

        /// <summary>
        /// 多久时间前
        /// </summary>
        public string LongTime
        {
            get
            {
                return this.LongTimeType.GetDescription();
            }
        }

        /// <summary>
        /// 最后访问时间
        /// </summary>
        [JsonIgnore]
        public DateTime LastTime { get; set; }
    }
}
