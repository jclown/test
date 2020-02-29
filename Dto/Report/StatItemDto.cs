using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Report
{
    /// <summary>
    /// 统计项
    /// </summary>
    public class StatItemDto
    {
        /// <summary>
        /// 统计值
        /// </summary>
        public dynamic Value { get; set; }

        /// <summary>
        /// 统计项类型
        /// </summary>
        public StatItemTypeEnum Type { get; set; }

        /// <summary>
        /// 统计项类型名称
        /// </summary>
        public string TypeName { get { return Type.GetDescription(); } }

        /// <summary>
        /// 统计项类型描述
        /// </summary>
        public string TypeDesc { get { return Type.GetRemark(); } }
    }
}
