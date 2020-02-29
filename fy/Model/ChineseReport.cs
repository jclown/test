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
	public partial class ChineseReport
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
		    /// 跟进类型 客源举报 = 1,房源举报 =2   
            /// </summary>
            [Description("跟进类型 客源举报 = 1,房源举报 =2   ")]			
		    public int? ReportType { get; set; } 
          
            /// <summary> 
		    /// 举报ID
            /// </summary>
            [Description("举报ID")]			
		    public int? ReportId { get; set; } 
          
            /// <summary> 
		    /// 举报原因 房源举报 1=房源不存在，2=虚假报价，3=虚假图片，4=虚假独家，5 =其他  客源举报  1=客源不存在，2=虚假信息，3=其他，
            /// </summary>
            [Description("举报原因 房源举报 1=房源不存在，2=虚假报价，3=虚假图片，4=虚假独家，5 =其他  客源举报  1=客源不存在，2=虚假信息，3=其他，")]			
		    public int? ReportReasons { get; set; } 
          
            /// <summary> 
		    /// 备注
            /// </summary>
            [Description("备注")]			
		    public string? Remarks { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? EconomicId { get; set; } 
		 
	}
}
