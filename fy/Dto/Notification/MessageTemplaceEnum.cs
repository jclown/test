using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dto.Notification
{
    /// <summary>
    /// 消息模版
    /// </summary>
    public enum MessageTemplaceEnum
    {
        /// <summary>
        /// 需求失效提醒
        /// </summary>
        [Description("需求失效提醒")]
        DemandExpriy = 1,

        /// <summary>
        /// 考勤打卡-上班
        /// </summary>
        [Description("考勤打卡-上班")]
        AttendanceCheckIn = 2,

        /// <summary>
        /// 考勤打卡-下班
        /// </summary>
        [Description("考勤打卡-下班")]
        AttendanceCheckOut = 3,
    }
}
