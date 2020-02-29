using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dto.Demand
{
    public enum EnumWantBedRoomsType
    {
        [Description("1室")]
        One = 1,
        [Description("2室")]
        Two = 2,
        [Description("3室")]
        Three = 3,
        [Description("4室")]
        Four = 4,
        [Description("5室")]
        Five = 5,
        [Description("5室以上")]
        FiveMore = 6
    }
}
