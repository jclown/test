using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dto.Contact
{
    public enum ContactEnum
    {
        /// <summary>
        ///打电话
        /// </summary>
        [Description("打电话")]
        Telephone = 1,
        /// <summary>
        /// IM在线沟通
        /// </summary>
        [Description("IM在线沟通")]
        IM = 2
    }
}
