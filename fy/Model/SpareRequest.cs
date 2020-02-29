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
	public partial class SpareRequest
	{	 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Key] // 主键
            [Description("")]			
		    public string RequestID { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public DateTime? RequestDate { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public double? RequestMoney { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public bool? FlagAudit { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? Remark { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? DeptID { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? EmpID { get; set; } 
		 
	}
}
