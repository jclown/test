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
	public partial class ChineseAdManage
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
		    public int? AdZoneId { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? AdZoneCode { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public string? AdTitle { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public string? AdImage { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public string? AdUrl { get; set; } 
          
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
		    /// 
            /// </summary>
            [Description("")]			
		    public byte? AdRouteType { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? Extend { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? Sort { get; set; } 
		 
	}
}
