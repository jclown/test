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
	public partial class AccountTypes
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
		    public string Name { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public double Price_Monthly { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public double Price_Annually { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int AdditionalRoleId { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string SubText { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? AdditionalWebsitePlanId { get; set; } 
		 
	}
}
