using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.HouseGroup
{
    /// <summary>
    /// 区域负责人
    /// </summary>
    public class PrincipalDetailDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string TraderName { get; set; }

        /// <summary>
        /// IM账号名
        /// </summary>
        public string IMUserName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
