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
	public partial class ShareRecord
	{	 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Key] // 主键
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 自增
            [Description("")]			
		    public int Id { get; set; } 
          
            /// <summary> 
		    /// 分享全局唯一ID
            /// </summary>
            [Description("分享全局唯一ID")]			
		    public string? UUID { get; set; } 
          
            /// <summary> 
		    /// 创建时间
            /// </summary>
            [Description("创建时间")]			
		    public DateTime CreateTime { get; set; } 
          
            /// <summary> 
		    /// 房源所属公司
            /// </summary>
            [Description("房源所属公司")]			
		    public int ShopId { get; set; } 
          
            /// <summary> 
		    /// 分享人标识
            /// </summary>
            [Description("分享人标识")]			
		    public string SharerTag { get; set; } 
          
            /// <summary> 
		    /// 分享人类型
            /// </summary>
            [Description("分享人类型")]			
		    public int SharerType { get; set; } 
          
            /// <summary> 
		    /// 分享方式
            /// </summary>
            [Description("分享方式")]			
		    public int ShareType { get; set; } 
          
            /// <summary> 
		    /// 房源ID
            /// </summary>
            [Description("房源ID")]			
		    public int HouseId { get; set; } 
          
            /// <summary> 
		    /// 房源分类
            /// </summary>
            [Description("房源分类")]			
		    public int HouseType { get; set; } 
          
            /// <summary> 
		    /// 转发的源记录
            /// </summary>
            [Description("转发的源记录")]			
		    public string? TranspondSource { get; set; } 
          
            /// <summary> 
		    /// 原始分享人ID
            /// </summary>
            [Description("原始分享人ID")]			
		    public int OringnalShareUserId { get; set; } 
          
            /// <summary> 
		    /// 是否MLS房源
            /// </summary>
            [Description("是否MLS房源")]			
		    public int IsMls { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? BehaviorId { get; set; } 
		 
	}
}
