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
	public partial class UsersLoginRecord
	{	 
          
            /// <summary> 
		    /// ??ID
            /// </summary>
		    [Key] // 主键
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 自增
            [Description("??ID")]			
		    public int Id { get; set; } 
          
            /// <summary> 
		    /// ??ID
            /// </summary>
            [Description("??ID")]			
		    public int? UserId { get; set; } 
          
            /// <summary> 
		    /// IP??
            /// </summary>
            [Description("IP??")]			
		    public string? IpAddress { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public DateTime? LoginTime { get; set; } 
          
            /// <summary> 
		    /// ???_W
            /// </summary>
            [Description("???_W")]			
		    public string? Latitude { get; set; } 
          
            /// <summary> 
		    /// ???_Y
            /// </summary>
            [Description("???_Y")]			
		    public string? Longitude { get; set; } 
		 
	}
}
