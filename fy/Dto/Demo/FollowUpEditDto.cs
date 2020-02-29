using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dto.Demo
{
    /// <summary>
    /// 跟进详情
    /// </summary>
    public class FollowUpEditDto
    {
        /// <summary> 
        /// 跟进ID。新增时为0
        /// </summary>
        public int FollowUpID { get; set; }

        /// <summary> 
        /// 联系时间
        /// </summary>
        public string ContactTime { get; set; }

        /// <summary> 
        /// 联系主题，最大长度160。
        /// </summary>
        [Required(ErrorMessage = RuleConst.NonEmpty)]
        [StringLength(160, ErrorMessage = RuleConst.StringLengthMax)]
        public string Subject { get; set; }

        /// <summary> 
        /// 联系描述
        /// </summary>
        public string Description { get; set; }

        /// <summary> 
        /// 联系方式
        /// </summary>
        public string ContactWayCode { get; set; }

        /// <summary>
        /// 状态
        /// </summary>        
        public FollowUpStatusEnum Status { get; set; }
    }
}
