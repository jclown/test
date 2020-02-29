using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dto.Demo
{
    /// <summary>
    /// 跟进状态
    /// </summary>
    public enum FollowUpStatusEnum
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [Description("草稿")]
        Draft= 0,

        /// <summary>
        /// 未跟进
        /// </summary>
        [Description("未跟进")]
        Todo = 1,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Done = 2,

        /// <summary>
        /// 取消
        /// </summary>
        [Description("取消")]
        Cancelled = 3
    }
}
