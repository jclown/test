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
	public partial class MlsContactRecordLog
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
		    public DateTime CreateTime { get; set; } 
          
            /// <summary> 
		    /// 经纪人店铺ID
            /// </summary>
            [Description("经纪人店铺ID")]			
		    public int ShopId { get; set; } 
          
            /// <summary> 
		    /// 经纪人ID
            /// </summary>
            [Description("经纪人ID")]			
		    public int UserId { get; set; } 
          
            /// <summary> 
		    /// 类型 （新房 二手房 客源）
            /// </summary>
            [Description("类型 （新房 二手房 客源）")]			
		    public int Type { get; set; } 
          
            /// <summary> 
		    /// 资源所属店铺 （二手房，新房，客源）
            /// </summary>
            [Description("资源所属店铺 （二手房，新房，客源）")]			
		    public int ResourcesShopId { get; set; } 
          
            /// <summary> 
		    /// 资源ID （二手房，新房，客源）
            /// </summary>
            [Description("资源ID （二手房，新房，客源）")]			
		    public int ResourcesId { get; set; } 
          
            /// <summary> 
		    /// 操作类型（1呼叫 2在线沟通）
            /// </summary>
            [Description("操作类型（1呼叫 2在线沟通）")]			
		    public int OperationType { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? Contacter { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? HouseSourceType { get; set; } 
		 
	}
}
