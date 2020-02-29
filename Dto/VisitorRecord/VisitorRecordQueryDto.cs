using Dto.Report;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.VisitorRecord
{
    public class VisitorRecordQueryDto : QueryDto
    {
        /// <summary>
        /// 统计项类型
        /// </summary>
        public StatItemTypeEnum StatItemType { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
    }
}
