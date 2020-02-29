using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dto.Demand
{
    /// <summary>
    /// 租房需求类型
    /// </summary>
    public enum EnumDemandRentingPlaceTypes : byte
    {
        /// <summary>
        /// 全部(整租)
        /// </summary>
        [Description("整租")]
        Entire = 1,
        /// <summary>
        /// 共享的(合租)
        /// </summary>
        [Description("合租")]
        Shared = 2,
        /// <summary>
        /// 不限
        /// </summary>
        [Description("不限")]
        Any = 0
    }
}
