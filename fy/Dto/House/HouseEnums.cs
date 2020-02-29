using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dto.House
{
    /// <summary>
    /// 楼层高低
    /// </summary>
    public enum EnumFloorType : short
    {
        [Description("低楼层")]
        Low = 1,
        [Description("中楼层")]
        Center = 2,
        [Description("高楼层")]
        Hight = 3
    }

    /// <summary>
    /// 朝向
    /// </summary>
    public enum EnumOrientationType
    {
        [Description("东")]
        东 = 1,
        [Description("南")]
        南 = 2,
        [Description("西")]
        西 = 3,
        [Description("北")]
        北 = 4,
        [Description("东南")]
        东南 = 5,
        [Description("东北")]
        东北 = 6,
        [Description("西南")]
        西南 = 7,
        [Description("西北")]
        西北 = 8,
        [Description("南北")]
        南北 = 9,
        [Description("东西")]
        东西 = 10,
        [Description("")]
        Empty = 0,
    }
}
