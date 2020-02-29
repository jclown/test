using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Model
{ 
    /// <summary>
    /// 客户跟进表
    /// </summary>
	public partial class FollowUp
	{	 
          
            /// <summary> 
		    /// ??
            /// </summary>
		    [Key] // 主键
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 自增
            [Description("??")]			
		    public int FollowUpID { get; set; } 
          
            /// <summary> 
		    /// ??ID
            /// </summary>
            [Description("??ID")]			
		    public int CustomerID { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public DateTime? PlanTime { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public DateTime? ContactTime { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public string Subject { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public string Description { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public string ContactWayCode { get; set; } 
          
            /// <summary> 
		    /// ??:0?? 1??? 2??? 3??
            /// </summary>
            [Description("??:0?? 1??? 2??? 3??")]			
		    public int Status { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public DateTime CreateTime { get; set; } 
          
            /// <summary> 
		    /// ???ID
            /// </summary>
            [Description("???ID")]			
		    public int CreateUserID { get; set; } 
          
            /// <summary> 
		    /// ??????
            /// </summary>
            [Description("??????")]			
		    public DateTime UpdateTime { get; set; } 
          
            /// <summary> 
		    /// ?????ID
            /// </summary>
            [Description("?????ID")]			
		    public int UpdateUserID { get; set; } 
		 
	}
}
