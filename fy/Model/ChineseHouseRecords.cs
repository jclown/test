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
	public partial class ChineseHouseRecords
	{	 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Key] // 主键
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 自增
            [Description("")]			
		    public int Id { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int CreaterId { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int ShopId { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public DateTime CreateTime { get; set; } 
          
            /// <summary> 
		    /// 最近更新时间
            /// </summary>
            [Description("最近更新时间")]			
		    public DateTime? LastUpDateTime { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int HouseId { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int HouseSource { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? CustomerName { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? CustomerPhone { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public DateTime? HouseViewTime { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? TraderName { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? TraderPhone { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? TraderShopName { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? Note { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? HandleMessage { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int RecordStatus { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int IsReadC { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int IsReadM { get; set; } 
          
            /// <summary> 
		    /// 分销商、经纪人UserId
            /// </summary>
            [Description("分销商、经纪人UserId")]			
		    public int? TraderId { get; set; } 
          
            /// <summary> 
		    /// 标识是否已经复制到微信
            /// </summary>
            [Description("标识是否已经复制到微信")]			
		    public int IsCopyWx { get; set; } 
          
            /// <summary> 
		    /// 报备确认时间
            /// </summary>
            [Description("报备确认时间")]			
		    public DateTime? ConfirmTime { get; set; } 
          
            /// <summary> 
		    /// 报备确认操作人ID
            /// </summary>
            [Description("报备确认操作人ID")]			
		    public int? ConfirmManagerId { get; set; } 
          
            /// <summary> 
		    /// 报备确认操作人姓名
            /// </summary>
            [Description("报备确认操作人姓名")]			
		    public string? ConfirmManagerName { get; set; } 
          
            /// <summary> 
		    /// 报备确认-图片
            /// </summary>
            [Description("报备确认-图片")]			
		    public string? ConfirmPhotos { get; set; } 
          
            /// <summary> 
		    /// 到场确认状态
            /// </summary>
            [Description("到场确认状态")]			
		    public int ArriveStatus { get; set; } 
          
            /// <summary> 
		    /// 到场确认校验码
            /// </summary>
            [Description("到场确认校验码")]			
		    public string? ArriveCode { get; set; } 
          
            /// <summary> 
		    /// 到场时间
            /// </summary>
            [Description("到场时间")]			
		    public DateTime? ArriveTime { get; set; } 
          
            /// <summary> 
		    /// 到场图片
            /// </summary>
            [Description("到场图片")]			
		    public string? ArrivePhotos { get; set; } 
          
            /// <summary> 
		    /// 到场跟进备注
            /// </summary>
            [Description("到场跟进备注")]			
		    public string? ArriveNote { get; set; } 
          
            /// <summary> 
		    /// 到场确认-处理人ID
            /// </summary>
            [Description("到场确认-处理人ID")]			
		    public int? ArriveManagerId { get; set; } 
          
            /// <summary> 
		    /// 到场确认-处理人姓名
            /// </summary>
            [Description("到场确认-处理人姓名")]			
		    public string? ArriveManagerName { get; set; } 
          
            /// <summary> 
		    /// 到场确认时间
            /// </summary>
            [Description("到场确认时间")]			
		    public DateTime? ArriveConfirmTime { get; set; } 
          
            /// <summary> 
		    /// 认筹状态
            /// </summary>
            [Description("认筹状态")]			
		    public int RenChouStatus { get; set; } 
          
            /// <summary> 
		    /// 认筹时间
            /// </summary>
            [Description("认筹时间")]			
		    public DateTime? RenChouTime { get; set; } 
          
            /// <summary> 
		    /// 认筹金额
            /// </summary>
            [Description("认筹金额")]			
		    public decimal? RenChouSum { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? RenChouPos { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? RenChouCard { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? RenChouReceipt { get; set; } 
          
            /// <summary> 
		    /// 认筹操作人ID
            /// </summary>
            [Description("认筹操作人ID")]			
		    public int? RenChouManagerId { get; set; } 
          
            /// <summary> 
		    /// 认筹操作人姓名
            /// </summary>
            [Description("认筹操作人姓名")]			
		    public string? RenChouManagerName { get; set; } 
          
            /// <summary> 
		    /// 认筹操作时间
            /// </summary>
            [Description("认筹操作时间")]			
		    public DateTime? RenChouHandleTime { get; set; } 
          
            /// <summary> 
		    /// 认筹备注
            /// </summary>
            [Description("认筹备注")]			
		    public string? RenChouNote { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? RenChouPhotos { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? RenChouCustomerName { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? RenChouCustomerPhone { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? RenChouCustomerIdCard { get; set; } 
          
            /// <summary> 
		    /// 预定-状态
            /// </summary>
            [Description("预定-状态")]			
		    public int ReserveStatus { get; set; } 
          
            /// <summary> 
		    /// 预定-楼栋房号
            /// </summary>
            [Description("预定-楼栋房号")]			
		    public string? ReserveHouse { get; set; } 
          
            /// <summary> 
		    /// 预定-认购日期
            /// </summary>
            [Description("预定-认购日期")]			
		    public DateTime? ReserveSubscriptionTime { get; set; } 
          
            /// <summary> 
		    /// 预定-诚意金
            /// </summary>
            [Description("预定-诚意金")]			
		    public decimal? ReserveSinceritySum { get; set; } 
          
            /// <summary> 
		    /// 预定-定金
            /// </summary>
            [Description("预定-定金")]			
		    public decimal? ReserveEarnest { get; set; } 
          
            /// <summary> 
		    /// 预定-备注
            /// </summary>
            [Description("预定-备注")]			
		    public string? ReserveNote { get; set; } 
          
            /// <summary> 
		    /// 预定-处理时间
            /// </summary>
            [Description("预定-处理时间")]			
		    public DateTime? ReserveHandleTime { get; set; } 
          
            /// <summary> 
		    /// 预定-处理人ID
            /// </summary>
            [Description("预定-处理人ID")]			
		    public int? ReserveManagerId { get; set; } 
          
            /// <summary> 
		    /// 预定-处理人姓名
            /// </summary>
            [Description("预定-处理人姓名")]			
		    public string? ReserveManagerName { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? ReservePhotos { get; set; } 
          
            /// <summary> 
		    /// 是否关闭
            /// </summary>
            [Description("是否关闭")]			
		    public int IsClose { get; set; } 
          
            /// <summary> 
		    /// 关闭备注
            /// </summary>
            [Description("关闭备注")]			
		    public string? CloseNote { get; set; } 
          
            /// <summary> 
		    /// 关闭时间
            /// </summary>
            [Description("关闭时间")]			
		    public DateTime? CloseHandleTime { get; set; } 
          
            /// <summary> 
		    /// 关闭人ID
            /// </summary>
            [Description("关闭人ID")]			
		    public int? CloseManagerId { get; set; } 
          
            /// <summary> 
		    /// 关闭人姓名
            /// </summary>
            [Description("关闭人姓名")]			
		    public string? CloseManagerName { get; set; } 
          
            /// <summary> 
		    /// 交资料状态
            /// </summary>
            [Description("交资料状态")]			
		    public int DocCommitStatus { get; set; } 
          
            /// <summary> 
		    /// 交资料备注
            /// </summary>
            [Description("交资料备注")]			
		    public string? DocCommitNote { get; set; } 
          
            /// <summary> 
		    /// 交资料处理时间
            /// </summary>
            [Description("交资料处理时间")]			
		    public DateTime? DocCommitHandleTime { get; set; } 
          
            /// <summary> 
		    /// 交资料处理人ID
            /// </summary>
            [Description("交资料处理人ID")]			
		    public int? DocCommitManagerId { get; set; } 
          
            /// <summary> 
		    /// 交资料处理人名字
            /// </summary>
            [Description("交资料处理人名字")]			
		    public string? DocCommitManagerName { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? DocCommitPhotos { get; set; } 
          
            /// <summary> 
		    /// 结佣备注
            /// </summary>
            [Description("结佣备注")]			
		    public string? CommissionNote { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public DateTime? CommissionHandleTime { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? CommissionManagerId { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? CommissionManagerName { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? RecordsApplay { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? Photos { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? IsQuick { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? RoleType { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? DistributionRule { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? DistributionRuleText { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? ArriveNumber { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? ReserveCustomerName { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? ReserveCustomerPhone { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? ReserveCustomerIdCard { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? ReservePos { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? ReserveReceipt { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? ReserveNumber { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? ReservePayTime { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? ReservePayType { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? IsTaDing { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public DateTime? TaDingHandleTime { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? TaDingeManagerId { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? TaDingManagerName { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? TaDingNote { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public DateTime? ConcludeHandleTime { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? ConcludeManagerId { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? ConcludeManagerName { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? DistributorsApplyInfo { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? DistributorsApplyStatus { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? AgentApplyInfo { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? AgentApplyStatus { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? DevelopersId { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? DevelopersShow { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? RecordsFlowSetting { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? RecordsLog { get; set; } 
          
            /// <summary> 
		    /// 房源的相关报备设置
            /// </summary>
            [Description("房源的相关报备设置")]			
		    public string? Restrictions { get; set; } 
          
            /// <summary> 
		    /// 重复报备
            /// </summary>
            [Description("重复报备")]			
		    public int? IsRepeat { get; set; } 
		 
	}
}
