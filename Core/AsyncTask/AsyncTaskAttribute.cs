using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay.AsyncTask
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AsyncTaskAttribute : Attribute
    {
        /// <summary>
        /// 指定用于获取任务标识的表达式，只能从输入参数获取，如“IN:quoteInfo.InquiryID”，如“IN:standardId”
        /// </summary>
        public string TaskFlagExpression { get; set; }

        /// <summary>
        /// 默认按当前用户id隔离
        /// </summary>
        public bool TaskFlagByUserID { get; set; } = true;
    }
}