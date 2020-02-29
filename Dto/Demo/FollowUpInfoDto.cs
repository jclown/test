using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Dto.Demo
{
    /// <summary>
    /// 跟进详情
    /// </summary>
    public class FollowUpInfoDto : FollowUpEditDto
    {
        /// <summary> 
        /// 状态名称
        /// </summary>
        public string StatusName { get; set; } = string.Empty;
        
        /// <summary>
        /// 联系方式名称
        /// </summary>
        public string ContactWayName { get; set; } = string.Empty;

        /// <summary>
        /// 创建用户名称
        /// </summary>
        public string CreateUserName { get; set; } = string.Empty;

        /// <summary>
        /// 更新用户名称
        /// </summary>
        public string UpdateUserName { get; set; } = string.Empty;
    }
}
