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
	public partial class VisitorBehaviorRecord
	{	 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Key] // 主键
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 自增
            [Description("")]			
		    public int Id { get; set; } 
          
            /// <summary> 
		    /// 创建时间
            /// </summary>
            [Description("创建时间")]			
		    public DateTime CreateTime { get; set; } 
          
            /// <summary> 
		    /// 房源ID
            /// </summary>
            [Description("房源ID")]			
		    public int HouseId { get; set; } 
          
            /// <summary> 
		    /// 房源类型
            /// </summary>
            [Description("房源类型")]			
		    public int HouseType { get; set; } 
          
            /// <summary> 
		    /// 是否合作发布
            /// </summary>
            [Description("是否合作发布")]			
		    public int IsMls { get; set; } 
          
            /// <summary> 
		    /// 分享ID
            /// </summary>
            [Description("分享ID")]			
		    public string? ShareUUID { get; set; } 
          
            /// <summary> 
		    /// 访客标识
            /// </summary>
            [Description("访客标识")]			
		    public string VisitorTag { get; set; } 
          
            /// <summary> 
		    /// 访客类型（1.经纪人 2.C端客户）
            /// </summary>
            [Description("访客类型（1.经纪人 2.C端客户）")]			
		    public int VisitorType { get; set; } 
          
            /// <summary> 
		    /// 行为来源
            /// </summary>
            [Description("行为来源")]			
		    public int BehaviorSource { get; set; } 
          
            /// <summary> 
		    /// 行为类型
            /// </summary>
            [Description("行为类型")]			
		    public int BehaviorType { get; set; } 
          
            /// <summary> 
		    /// 访问时长
            /// </summary>
            [Description("访问时长")]			
		    public string? StayTime { get; set; } 
          
            /// <summary> 
		    /// 设备
            /// </summary>
            [Description("设备")]			
		    public string? Device { get; set; } 
          
            /// <summary> 
		    /// 次数
            /// </summary>
            [Description("次数")]			
		    public int? Time { get; set; } 
          
            /// <summary> 
		    /// 所属经纪人ID
            /// </summary>
            [Description("所属经纪人ID")]			
		    public int? AgentUserId { get; set; } 
		 
	}
}
