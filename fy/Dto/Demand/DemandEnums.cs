using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dto.Demand
{
    /// <summary>
    /// 需求类型
    /// </summary>
    public enum EnumDemandEstateTypes : byte
    {
        /// <summary>
        /// 租
        /// </summary>
        [Description("求租")]
        Rent = 1,
        /// <summary>
        /// 买
        /// </summary>
        [Description("求购")]
        Buy = 2
    }

    /// <summary>
    /// 需求类型
    /// </summary>
    public enum EnumDemandHouseTypes : byte
    {
        /// <summary>
        /// 新
        /// </summary>
        [Description("新房")]
        New = 1,
        /// <summary>
        /// 二手
        /// </summary>
        [Description("二手房")]
        PreOwned = 2,
        /// <summary>
        /// 不限
        /// </summary>
        [Description("不限")]
        Any = 0
    }

    public enum EnumDemandTagType
    {
        [Description("地铁房")]
        地铁房 = 1,
        [Description("学区房")]
        学区房 = 2,
        [Description("电梯楼")]
        电梯楼 = 3,
        [Description("带车位")]
        带车位 = 4,
        [Description("多卫生间")]
        多卫生间 = 5,
        [Description("带阳台")]
        带阳台 = 6,
        [Description("厅出阳台")]
        厅出阳台 = 7,
        [Description("带飘窗")]
        带飘窗 = 8,
        [Description("不要顶楼")]
        不要顶楼 = 9,
        [Description("不要底层")]
        不要底层 = 10,
    }

    /// <summary>
    /// 房源类型
    /// 二手房
    /// 新房
    /// </summary>
    public enum EnumHosueCommonType
    {
        /// <summary>
        /// 新房
        /// </summary>
        [Description("新房")]
        NewHouse = 1,
        /// <summary>
        /// 二手房
        /// </summary>
        [Description("二手房")]
        ResaleHouse = 2,
    }

    /// <summary>
    /// 二手房商品类型
    /// </summary>
    public enum EnumResaleHouseCommodityType
    {
        /// <summary>
        /// 在租
        /// </summary>
        [Description("在租")]
        Rent = 1,
        /// <summary>
        /// 在售
        /// </summary>
        [Description("在售")]
        Sale = 2,
        /// <summary>
        /// 租售
        /// </summary>
        [Description("租售")]
        RentAndSale = 3,
    }
}
