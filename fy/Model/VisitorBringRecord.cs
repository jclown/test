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
	public partial class VisitorBringRecord
	{	 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Key] // 主键
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 自增
            [Description("")]			
		    public int Id { get; set; } 
          
            /// <summary> 
		    /// 创建时间
            /// </summary>
            [Description("创建时间")]			
		    public DateTime CreateTime { get; set; } 
          
            /// <summary> 
		    /// 访客标识
            /// </summary>
            [Description("访客标识")]			
		    public string VisitorTag { get; set; } 
          
            /// <summary> 
		    /// 拓客来源
            /// </summary>
            [Description("拓客来源")]			
		    public string? SourceShareUUID { get; set; } 
          
            /// <summary> 
		    /// 拓客人
            /// </summary>
            [Description("拓客人")]			
		    public string Bringer { get; set; } 
          
            /// <summary> 
		    /// 拓客人类型
            /// </summary>
            [Description("拓客人类型")]			
		    public int BringerType { get; set; } 
		 
	}
}
