using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dto
{
    /// <summary>
    /// 日期范围
    /// </summary>
    public enum DateRangeEnum
    {
        /// <summary>
        /// 今日
        /// </summary>
        [Description("今日")]
        Today= 1,

        /// <summary>
        /// 昨日
        /// </summary>
        [Description("昨日")]
        Yesterday = 2,

        /// <summary>
        /// 本周
        /// </summary>
        [Description("本周")]
        ThisWeek = 3,

        /// <summary>
        /// 上周
        /// </summary>
        [Description("上周")]
        LastWeek = 4,

        /// <summary>
        /// 本月
        /// </summary>
        [Description("本月")]
        ThisMonth = 5,

        /// <summary>
        /// 上月
        /// </summary>
        [Description("上月")]
        LastMonth = 6,

        /// <summary>
        /// 自定义
        /// </summary>
        [Description("自定义")]
        Other = 0,
    }
}
