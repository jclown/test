using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Modobay.Api
{
    /// <summary>
    /// 返回结果（系统异常的情况）
    /// </summary>
    [DataContract]
    public class ExceptionResultDto
    {
        /// <summary>
        /// 操作状态：固定为false
        /// </summary>
        [DataMember]
        public bool Success { get; set; } = false;

        /// <summary>
        /// 错误代码，常见的错误代码请查看AppExceptionType的定义，具体业务可以自行约定。
        /// </summary>
        [DataMember]
        public string ErrorCode { get; set; } = string.Empty;

        /// <summary>
        /// 系统异常信息
        /// </summary>
        [DataMember] 
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 请求id，可根据请求id查询相关日志信息或异步任务执行进度。
        /// </summary>
        [DataMember] 
        public string RequestId { get; set; }
    }
}
