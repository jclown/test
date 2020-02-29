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
	public partial class ChineseShopJoin
	{	 
          
            /// <summary> 
		    /// ??ID
            /// </summary>
		    [Key] // 主键
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 自增
            [Description("??ID")]			
		    public int Id { get; set; } 
          
            /// <summary> 
		    /// ??
            /// </summary>
            [Description("??")]			
		    public string? Name { get; set; } 
          
            /// <summary> 
		    /// ??
            /// </summary>
            [Description("??")]			
		    public string? AvatarPath { get; set; } 
          
            /// <summary> 
		    /// ????Id
            /// </summary>
            [Description("????Id")]			
		    public int? ShopId { get; set; } 
          
            /// <summary> 
		    /// ????Id
            /// </summary>
            [Description("????Id")]			
		    public int? DepartmentId { get; set; } 
          
            /// <summary> 
		    /// ??ID
            /// </summary>
            [Description("??ID")]			
		    public int? UserId { get; set; } 
          
            /// <summary> 
		    /// ??Id
            /// </summary>
            [Description("??Id")]			
		    public int? TradeId { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public DateTime? UpdateTime { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public DateTime? CreateTime { get; set; } 
          
            /// <summary> 
		    /// ???ID
            /// </summary>
            [Description("???ID")]			
		    public int? CreatedBy { get; set; } 
          
            /// <summary> 
		    /// ?????ID
            /// </summary>
            [Description("?????ID")]			
		    public int? UpdateBy { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public int? Status { get; set; } 
		 
	}
}
