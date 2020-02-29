using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dto.Report
{
    /// <summary>
    /// 统计项类型
    /// </summary>
    public enum StatItemTypeEnum
    {
        /// <summary>
        /// 录房
        /// </summary>
        [Description("录房")]
        [Lib.Remark("录入二手房和出租房的总数量")]
        InputHouse = 0,
        /// <summary>
        /// 合作房源
        /// </summary>
        [Description("合作房源")]
        [Lib.Remark("发布到平台的合作房源的总数量")]
        MlsHouse = 1,
        /// <summary>
        /// AI找房
        /// </summary>
        [Description("AI找房")]
        [Lib.Remark("录入客户需求进行智能找房的总次数")]
        InputDemnd = 2,
        /// <summary>
        /// 浏览
        /// </summary>
        [Description("浏览")]
        [Lib.Remark("经纪人浏览他人房源的总次数")]
        Brower = 3,
        /// <summary>
        /// 联络
        /// </summary>
        [Description("联络")]
        [Lib.Remark("")]
        Contact = 4,
        /// <summary>
        /// 联络
        /// </summary>
        [Description("去电")]
        [Lib.Remark("主动联系经纪人合作的总次数")]
        Telephone = 5,
        /// <summary>
        /// 联络
        /// </summary>
        [Description("来电")]
        [Lib.Remark("被经纪人联系合作的总次数")]
        CallMe = 7,
        /// <summary>
        /// 分享
        /// </summary>
        [Description("分享")]
        [Lib.Remark("分享房源到微信平台的总次数")]
        Share = 6,
        /// <summary>
        /// 获客
        /// </summary>
        [Description("获客")]
        [Lib.Remark("分享房源到微信平台获取客户的总人数")]
        VisitUser =8,
        /// <summary>
        /// 关注
        /// </summary>
        [Description("被关注")]
        [Lib.Remark("房源被客户和经纪人浏览、转发和收藏的总次数")]
        Concerned = 9,
    }
}
