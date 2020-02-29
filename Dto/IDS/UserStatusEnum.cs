using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dto.IDS
{
    public enum UserStatusEnum
    {
        [Description("未激活")]
        NonActivated = 1,

        [Description("正常")]
        Normal = 2,

        [Description("禁用")]
        Disabled = 3
    }
}
