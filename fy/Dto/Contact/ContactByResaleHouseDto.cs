using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Contact
{
    /// <summary>
    /// 二手房联络列表
    /// </summary>
    public class ContactByResaleHouseDto
    {
        /// <summary>
        /// 联络分页
        /// </summary>
        public PagedList<ResaleHouseContactListDto> ContactPage { get; set; }

        /// <summary>
        /// 二手房信息
        /// </summary>
        public HouseByContactDto HouseInfo { get; set; }

        /// <summary>
        /// 联络列表
        /// </summary>
        
    }
}
