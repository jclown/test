using Dto.Report;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.House
{
    public class HouseQueryDto: QueryDto
    {
        /// <summary>
        /// 经纪人Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 统计项类型
        /// </summary>
        public StatItemTypeEnum StatItemType { get; set; }
    }
}
