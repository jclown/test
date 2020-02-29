using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Model
{ 
    /// <summary>
    /// -
    /// </summary>
	public partial class ChineseCustomerDemandNew
	{	 
          
            /// <summary> 
		    /// 主键ID
            /// </summary>
		    [Key] // 主键
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 自增
            [Description("主键ID")]			
		    public int Id { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? CustomerNumber { get; set; } 
          
            /// <summary> 
		    /// 用户来源  0为顾问端，1为用户端
            /// </summary>
            [Description("用户来源  0为顾问端，1为用户端")]			
		    public int? UserSourceFrom { get; set; } 
          
            /// <summary> 
		    /// 城市ID
            /// </summary>
            [Description("城市ID")]			
		    public int? CityId { get; set; } 
          
            /// <summary> 
		    /// 需求状态 1.正常；2.已删除
            /// </summary>
            [Description("需求状态 1.正常；2.已删除")]			
		    public short? Status { get; set; } 
          
            /// <summary> 
		    /// 到期时间
            /// </summary>
            [Description("到期时间")]			
		    public DateTime? ExpireTime { get; set; } 
          
            /// <summary> 
		    /// 发布者Id
            /// </summary>
            [Description("发布者Id")]			
		    public int? CreatorId { get; set; } 
          
            /// <summary> 
		    /// 发布者名称
            /// </summary>
            [Description("发布者名称")]			
		    public string? CreatorName { get; set; } 
          
            /// <summary> 
		    /// 创建时间
            /// </summary>
            [Description("创建时间")]			
		    public DateTime? CreateTime { get; set; } 
          
            /// <summary> 
		    /// 维护人
            /// </summary>
            [Description("维护人")]			
		    public int? AccendantId { get; set; } 
          
            /// <summary> 
		    /// 维护人名称
            /// </summary>
            [Description("维护人名称")]			
		    public string? AccendantName { get; set; } 
          
            /// <summary> 
		    /// 发布者Id
            /// </summary>
            [Description("发布者Id")]			
		    public int? UpateUserId { get; set; } 
          
            /// <summary> 
		    /// 更新时间
            /// </summary>
            [Description("更新时间")]			
		    public DateTime? UpdateTime { get; set; } 
          
            /// <summary> 
		    /// 商铺ID
            /// </summary>
            [Description("商铺ID")]			
		    public int? ShopId { get; set; } 
          
            /// <summary> 
		    /// 部门ID
            /// </summary>
            [Description("部门ID")]			
		    public int? DepartmentId { get; set; } 
          
            /// <summary> 
		    /// 意向区域集合
            /// </summary>
            [Description("意向区域集合")]			
		    public string? DistrictIdArray { get; set; } 
          
            /// <summary> 
		    /// 意向区域名称
            /// </summary>
            [Description("意向区域名称")]			
		    public string? DistrictNameArray { get; set; } 
          
            /// <summary> 
		    /// 意向街道 集合
            /// </summary>
            [Description("意向街道 集合")]			
		    public string? SubdistrictIdArray { get; set; } 
          
            /// <summary> 
		    /// 意向街道名称
            /// </summary>
            [Description("意向街道名称")]			
		    public string? SubdistrictNameArray { get; set; } 
          
            /// <summary> 
		    /// 意向小区集合
            /// </summary>
            [Description("意向小区集合")]			
		    public string? CommunityIdArray { get; set; } 
          
            /// <summary> 
		    /// 意向小区名称
            /// </summary>
            [Description("意向小区名称")]			
		    public string? CommunityNameArray { get; set; } 
          
            /// <summary> 
		    /// 备注
            /// </summary>
            [Description("备注")]			
		    public string? Remark { get; set; } 
          
            /// <summary> 
		    /// 客源类型 0=公客 、1=私客   
            /// </summary>
            [Description("客源类型 0=公客 、1=私客   ")]			
		    public int? IsPublic { get; set; } 
          
            /// <summary> 
		    /// 房源类型 0=求购、1=求租   
            /// </summary>
            [Description("房源类型 0=求购、1=求租   ")]			
		    public int? HouseSource { get; set; } 
          
            /// <summary> 
		    /// 客户姓名
            /// </summary>
            [Description("客户姓名")]			
		    public string? CustomerName { get; set; } 
          
            /// <summary> 
		    /// 客户电话
            /// </summary>
            [Description("客户电话")]			
		    public string? PhoneNumber { get; set; } 
          
            /// <summary> 
		    /// 预算最低价
            /// </summary>
            [Description("预算最低价")]			
		    public int? BudgetMin { get; set; } 
          
            /// <summary> 
		    /// 预算最高价
            /// </summary>
            [Description("预算最高价")]			
		    public int? BudgetMax { get; set; } 
          
            /// <summary> 
		    /// 意向面积最小值
            /// </summary>
            [Description("意向面积最小值")]			
		    public int? IntentionAreaMin { get; set; } 
          
            /// <summary> 
		    /// 意向面积最大值
            /// </summary>
            [Description("意向面积最大值")]			
		    public int? IntentionAreaMax { get; set; } 
          
            /// <summary> 
		    /// 意向户型ID
            /// </summary>
            [Description("意向户型ID")]			
		    public string? HouseTypeIdArray { get; set; } 
          
            /// <summary> 
		    /// 意向户型名称
            /// </summary>
            [Description("意向户型名称")]			
		    public string? HouseTypeNameArray { get; set; } 
          
            /// <summary> 
		    /// 共享到房得宝
            /// </summary>
            [Description("共享到房得宝")]			
		    public int? ShareToFdb { get; set; } 
          
            /// <summary> 
		    /// 佣金比例
            /// </summary>
            [Description("佣金比例")]			
		    public decimal? CommissionRate { get; set; } 
          
            /// <summary> 
		    /// 求租方式 合租 = 1,整租 = 2，不限=3   
            /// </summary>
            [Description("求租方式 合租 = 1,整租 = 2，不限=3   ")]			
		    public int? MagdebrugType { get; set; } 
          
            /// <summary> 
		    /// 性别要求 限男 = 1,限女 = 2,不限 = 3   
            /// </summary>
            [Description("性别要求 限男 = 1,限女 = 2,不限 = 3   ")]			
		    public int? GenderLimit { get; set; } 
          
            /// <summary> 
		    /// 房源特色
            /// </summary>
            [Description("房源特色")]			
		    public string? FeatureArray { get; set; } 
          
            /// <summary> 
		    /// 会员备注
            /// </summary>
            [Description("会员备注")]			
		    public string? UserRemarks { get; set; } 
          
            /// <summary> 
		    /// 客户等级： 1、A(重点关注)   2、B(日常维护)  3、B(仅作记录)
            /// </summary>
            [Description("客户等级： 1、A(重点关注)   2、B(日常维护)  3、B(仅作记录)")]			
		    public int? CustomerLevel { get; set; } 
          
            /// <summary> 
		    /// 客户来源: 1、老客户  2、同事介绍   3、客户介绍   4、上门客  5、网络客   6、其它
            /// </summary>
            [Description("客户来源: 1、老客户  2、同事介绍   3、客户介绍   4、上门客  5、网络客   6、其它")]			
		    public int? SourseFrom { get; set; } 
          
            /// <summary> 
		    /// 点击数
            /// </summary>
            [Description("点击数")]			
		    public int? ClickNum { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? VirtualClickNum { get; set; } 
          
            /// <summary> 
		    /// 交易佣金类型
            /// </summary>
            [Description("交易佣金类型")]			
		    public int? CommissionType { get; set; } 
          
            /// <summary> 
		    /// 交易总佣金百分比
            /// </summary>
            [Description("交易总佣金百分比")]			
		    public double? CommissionPercent { get; set; } 
          
            /// <summary> 
		    /// 交易总佣金按月
            /// </summary>
            [Description("交易总佣金按月")]			
		    public double? CommissionMonth { get; set; } 
          
            /// <summary> 
		    /// 交易总佣金按金额
            /// </summary>
            [Description("交易总佣金按金额")]			
		    public double? CommissionTotal { get; set; } 
          
            /// <summary> 
		    /// 合作顾问佣金类型
            /// </summary>
            [Description("合作顾问佣金类型")]			
		    public int? CommissionPartnerType { get; set; } 
          
            /// <summary> 
		    /// 合作顾问佣金百分比
            /// </summary>
            [Description("合作顾问佣金百分比")]			
		    public double? CommissionPartnerPercent { get; set; } 
          
            /// <summary> 
		    /// 合作顾问佣金
            /// </summary>
            [Description("合作顾问佣金")]			
		    public double? CommissionPartner { get; set; } 
          
            /// <summary> 
		    /// 交易佣金支付方
            /// </summary>
            [Description("交易佣金支付方")]			
		    public int? PayBy { get; set; } 
          
            /// <summary> 
		    /// 客户性别
            /// </summary>
            [Description("客户性别")]			
		    public int? CustomerSex { get; set; } 
          
            /// <summary> 
		    /// 意向户型最小值
            /// </summary>
            [Description("意向户型最小值")]			
		    public int? HouseTypeMin { get; set; } 
          
            /// <summary> 
		    /// 意向户型最大值
            /// </summary>
            [Description("意向户型最大值")]			
		    public int? HouseTypeMax { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? IsOpenPhoneNumber { get; set; } 
          
            /// <summary> 
		    /// 意向区域
            /// </summary>
            [Description("意向区域")]			
		    public string? CommunitiesIds { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? LikeNum { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? LikeNum_Open { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? CollectNum { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? CollectNum_Open { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public DateTime? FollowUpLastData { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? SyncId { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? EmpName { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? IsCollective { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public DateTime? SoleAgentTime { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public DateTime? ShareToFdbTime { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? LastFollowUpUserId { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? CustomerCreaterFrom { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int IsDel { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? DeleterId { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? DeleterName { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public DateTime? LastDeleteTime { get; set; } 
		 
	}
}
