using System;
using System.Collections.Generic;
using System.Text;

namespace Dto
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 公司Id
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 公司名称(简称)
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
