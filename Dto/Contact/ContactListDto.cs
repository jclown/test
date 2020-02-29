using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Contact
{
    public class ContactListDto
    {
        /// <summary>
        /// 联系Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public ContactEnum OperationType { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string OperationTypeName { get { return ((int)OperationType).GetDescription<ContactEnum>(); } }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 第几次联系（暂不实现）
        /// </summary>
        public int HowTimes { get; set; }

        /// <summary>
        /// 客源方佣金 (单位%)
        /// </summary>
        public HouseByContactDto HouseInfo { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo UserInfo { get; set; }
    }
}
