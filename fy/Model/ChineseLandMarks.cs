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
	public partial class ChineseLandMarks
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
		    public string Keyword { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public double? Latitude { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public double? Longtitude { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("")]			
		    public bool IsDeleted { get; set; } 
		 
	}
}
