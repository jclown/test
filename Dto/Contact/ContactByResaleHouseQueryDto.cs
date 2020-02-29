using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Contact
{
    /// <summary>
    /// 二手房联络列表
    /// </summary>
    public class ContactByResaleHouseQueryDto : QueryDto
    {
        /// <summary>
        /// 二手房Id
        /// </summary>
        public int HouseId { get; set; }
    }
}
