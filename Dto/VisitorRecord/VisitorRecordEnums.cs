using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dto.VisitorRecord
{

    /// <summary>
    /// 多久时间前
    /// </summary>
    public enum LongTimeEnum
    {
        /// <summary>
        /// 今天
        /// </summary>
        [Description("今天")]
        ToDay = 1,
        /// <summary>
        /// 1天前 
        /// </summary>
        [Description("1天前")]
        ADaysAgo = 2,
        /// <summary>
        /// 2天前 
        /// </summary>
        [Description("2天前")]
        TwoDaysAgo = 3,
        /// <summary>
        ///一周前
        /// </summary>
        [Description("1周前")]
        AWeekAgo = 4,
        /// <summary>
        ///一月前
        /// </summary>
        [Description("1月前")]
        AMonthAgo = 5,
        /// <summary>
        ///二月前
        /// </summary>
        [Description("2月前")]
        TwoMonthAgo = 6,
        /// <summary>
        ///三月前
        /// </summary>
        [Description("3月前")]
        ThreeMonthAgo = 7,
    }

    /// <summary>
    /// 行为来源
    /// </summary>
    public enum BehaviorSourceEnum
    {
        /// <summary>
        /// 房得宝APP
        /// （拆分8和9，工作台和平台）
        /// </summary>
        [Description("房得宝APP")]
        房得宝APP = 1,
        /// <summary>
        /// 微信_单聊
        /// </summary>
        [Description("微信_单聊")]
        微信_单聊 = 2,
        /// <summary>
        /// 微信_群聊
        /// </summary>
        [Description("微信_群聊")]
        微信_群聊 = 3,
        /// <summary>
        /// 朋友圈
        /// </summary>
        [Description("朋友圈")]
        朋友圈 = 4,
        /// <summary>
        /// 小程序
        /// </summary>
        [Description("小程序")]
        小程序 = 5,
        /// <summary>
        /// 小程序_收藏列表
        /// </summary>
        [Description("小程序_收藏列表")]
        小程序_收藏列表 = 6,
        /// <summary>
        /// 房得宝_收藏列表
        /// (拆分成13，14 PC和app)
        /// </summary>
        [Description("房得宝_收藏列表")]
        房得宝_收藏列表 = 7,
        /// <summary>
        /// 房得宝PC端
        /// （拆分11和12，工作台和平台)
        /// </summary>
        [Description("房得宝PC端")]
        房得宝PC端 = 8,
        /// <summary>
        /// APP工作台
        /// </summary>
        [Description("APP工作台")]
        APP工作台 = 9,
        /// <summary>
        /// APPMLS平台
        /// </summary>
        [Description("APPMLS平台")]
        APPMLS平台 = 10,
        /// <summary>
        /// PC工作台
        /// </summary>
        [Description("PC工作台")]
        PC工作台 = 11,
        /// <summary>
        /// PCMLS平台
        /// </summary>
        [Description("PCMLS平台")]
        PCMLS平台 = 12,
        /// <summary>
        /// PC收藏列表
        /// </summary>
        [Description("PC收藏列表")]
        PC收藏列表 = 13,
        /// <summary>
        /// APP收藏列表
        /// </summary>
        [Description("APP收藏列表")]
        APP收藏列表 = 14
    }

    /// <summary>
    /// 行为类型
    /// </summary>
    public enum BehaviorTypeEnum
    {
        /// <summary>
        /// 浏览
        /// </summary>
        [Description("浏览")]
        浏览 = 1,
        /// <summary>
        /// 分享到微信好友
        /// </summary>
        [Description("分享到微信好友")]
        分享到微信好友 = 2,
        /// <summary>
        /// 分享到朋友圈
        /// </summary>
        [Description("分享到朋友圈")]
        分享到朋友圈 = 3,
        /// <summary>
        /// 分享到QQ
        /// </summary>
        [Description("分享到QQ")]
        分享到QQ = 4,
        /// <summary>
        /// 转发到微信聊天
        /// </summary>
        [Description("转发到微信聊天")]
        转发到微信聊天 = 5,
        /// <summary>
        /// 分享房源海报到微信
        /// </summary>
        [Description("分享房源海报到微信")]
        分享房源海报到微信 = 6,
        /// <summary>
        /// 分享房源海报到朋友圈
        /// </summary>
        [Description("分享房源海报到朋友圈")]
        分享房源海报到朋友圈 = 7,
        /// <summary>
        /// 分享房源海报到其它平台
        /// </summary>
        [Description("分享房源海报到其它平台")]
        分享房源海报到其它平台 = 8,
        /// <summary>
        /// 收藏
        /// </summary>
        [Description("收藏")]
        收藏 = 9,
        /// <summary>
        /// 二维码分享
        /// </summary>
        [Description("二维码分享")]
        二维码分享 = 10,
        /// <summary>
        /// 转发到朋友圈
        /// </summary>
        [Description("转发到朋友圈")]
        转发到朋友圈 = 11,
        /// <summary>
        /// 转发到QQ
        /// </summary>
        [Description("转发到QQ")]
        转发到QQ = 12
    }
}
