using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Modobay
{
    /// <summary>
    /// 常见的/通用的业务异常
    /// </summary>
    public enum AppExceptionType
    {
        /// <summary>
        /// 无效应用
        /// </summary>
        [Description("无效应用")]
        AppNotFound = -101,

        /// <summary>
        /// 签名sign无效
        /// </summary>
        [Description("签名sign无效")]
        AppSignInvalid = -102,

        /// <summary>
        /// 访问令牌accessToken无效
        /// </summary>
        [Description("访问令牌accessToken无效")]
        AppTokenInvalid = -103,

        /// <summary>
        /// 数据不存在
        /// </summary>
        [Description("数据不存在")]
        NotFound = -201,

        /// <summary>
        /// 数据删除失败
        /// </summary>
        [Description("数据删除失败")]
        DeleteFail = -202,

        /// <summary>
        /// 数据更新失败
        /// </summary>
        [Description("数据更新失败")]
        UpdateFail = -203,

        /// <summary>
        /// 参数错误
        /// </summary>
        [Description("参数错误")]
        ParameterError = -301,

        /// <summary>
        /// 应用程序无权访问
        /// </summary>
        [Description("应用程序无权访问")]
        AccessDenied = -302,

        /// <summary>
        /// 用户无权访问
        /// </summary>
        [Description("用户无权访问")]
        UserAccessDenied = -303,

        /// <summary>
        /// 无权访问数据
        /// </summary>
        [Description("无权访问数据")]
        DataAccessDenied = -304,

        /// <summary>
        /// 访问太频繁
        /// </summary>
        [Description("访问太频繁")]
        TimeLimitAccessDenied = -305
    }
}
