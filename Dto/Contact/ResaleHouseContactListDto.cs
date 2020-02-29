using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Contact
{
    /// <summary>
    /// 二手房联络列表
    /// </summary>
    public class ResaleHouseContactListDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public ContactEnum OperationType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 第几次联系
        /// </summary>
        public int HowTimes { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo UserInfo { get; set; }
    }
}
