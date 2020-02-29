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
	public partial class ChineseBrowsingHistory
	{	 
          
            /// <summary> 
		    /// ??ID
            /// </summary>
		    [Key] // 主键
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 自增
            [Description("??ID")]			
		    public int Id { get; set; } 
          
            /// <summary> 
		    /// ???Id
            /// </summary>
            [Description("???Id")]			
		    public int? CreatorId { get; set; } 
          
            /// <summary> 
		    /// ?????
            /// </summary>
            [Description("?????")]			
		    public string? CreatorName { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public DateTime? CreateTime { get; set; } 
          
            /// <summary> 
		    /// ???Id
            /// </summary>
            [Description("???Id")]			
		    public int? UpateUserId { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public DateTime? UpdateTime { get; set; } 
          
            /// <summary> 
		    /// ??ID
            /// </summary>
            [Description("??ID")]			
		    public int? ShopId { get; set; } 
          
            /// <summary> 
		    /// ??ID
            /// </summary>
            [Description("??ID")]			
		    public int? DepartmentId { get; set; } 
          
            /// <summary> 
		    /// ??ID
            /// </summary>
            [Description("??ID")]			
		    public int? ItemId { get; set; } 
          
            /// <summary> 
		    /// ??:1 ??,2 ??
            /// </summary>
            [Description("??:1 ??,2 ??")]			
		    public int? ItemType { get; set; } 
          
            /// <summary> 
		    /// Ip
            /// </summary>
            [Description("Ip")]			
		    public string? Ip { get; set; } 
          
            /// <summary> 
		    /// ????? 0=?? 1=??
            /// </summary>
            [Description("????? 0=?? 1=??")]			
		    public int? IsModobay { get; set; } 
		 
	}
}
