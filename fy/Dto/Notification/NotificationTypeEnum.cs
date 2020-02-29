using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dto.Notification
{
    //public enum NotificationTypeEnum
    //{
    //    System = 1,
    //    Personal = 2
    //}

    public enum NotificationTypeEnum : short
    {
        /// <summary>
        /// 系统消息
        /// </summary>
        [Description("系统消息")]
        SystemMsg = 1,
        /// <summary>
        /// 报备消息
        /// </summary>
        [Description("报备消息")]
        HouseRecords = 2,
        /// <summary>
        /// 工作提醒
        /// </summary>
        [Description("工作提醒")]
        Work = 3,
        /// <summary>
        /// 最新动态
        /// </summary>
        [Description("最新动态")]
        News = 4
    }
}
