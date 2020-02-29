using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Model
{ 
    /// <summary>
    /// 消息模版表
    /// </summary>
	public partial class MessageTemplate
	{	 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Key] // 主键
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 自增
            [Description("")]			
		    public int Id { get; set; } 
          
            /// <summary> 
		    /// 模版类型
            /// </summary>
            [Description("模版类型")]			
		    public int Type { get; set; } 
          
            /// <summary> 
		    /// 模版代码
            /// </summary>
            [Description("模版代码")]			
		    public string Code { get; set; } 
          
            /// <summary> 
		    /// 模版名称
            /// </summary>
            [Description("模版名称")]			
		    public string Name { get; set; } 
          
            /// <summary> 
		    /// 消息标题
            /// </summary>
            [Description("消息标题")]			
		    public string? MessageTitle { get; set; } 
          
            /// <summary> 
		    /// 消息内容
            /// </summary>
            [Description("消息内容")]			
		    public string MessageContent { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public DateTime CreateTime { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public DateTime UpdateTime { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("")]			
		    public bool IsDeleted { get; set; } 
		 
	}
}
