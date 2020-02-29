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
	public partial class ChineseHousesShareFollowUp
	{	 
          
            /// <summary> 
		    /// 主键ID
            /// </summary>
		    [Key] // 主键
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 自增
            [Description("主键ID")]			
		    public int Id { get; set; } 
          
            /// <summary> 
		    /// 城市ID
            /// </summary>
            [Description("城市ID")]			
		    public int? CityId { get; set; } 
          
            /// <summary> 
		    /// 需求状态 1.有效；2.无效 3交易中  4已成交
            /// </summary>
            [Description("需求状态 1.有效；2.无效 3交易中  4已成交")]			
		    public short? Status { get; set; } 
          
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
		    /// 跟进类型 跟进 = 0,带看 =1   
            /// </summary>
            [Description("跟进类型 跟进 = 0,带看 =1   ")]			
		    public int? FollowUpType { get; set; } 
          
            /// <summary> 
		    /// 房源ID
            /// </summary>
            [Description("房源ID")]			
		    public int? HouseId { get; set; } 
          
            /// <summary> 
		    /// 带看客源Id
            /// </summary>
            [Description("带看客源Id")]			
		    public int? CustomerDemandId { get; set; } 
          
            /// <summary> 
		    /// 备注
            /// </summary>
            [Description("备注")]			
		    public string? Remarks { get; set; } 
          
            /// <summary> 
		    /// 带看日期
            /// </summary>
            [Description("带看日期")]			
		    public DateTime? TakeDate { get; set; } 
          
            /// <summary> 
		    /// 分享ID
            /// </summary>
            [Description("分享ID")]			
		    public int? ShareItemId { get; set; } 
		 
	}
}
