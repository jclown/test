using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Model
{ 
    /// <summary>
    /// 附件
    /// </summary>
	public partial class Attachment
	{	 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Key] //主键
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //主键
            [Description("")]
		    public int AttachmentID { get; set; } 
          
            /// <summary> 
		    /// 文件名
            /// </summary>
            [Description("文件名")]
		    public string FileName { get; set; } 
          
            /// <summary> 
		    /// 附件类别
            /// </summary>
            [Description("附件类别")]
		    public string AttachmentKind { get; set; } 
          
            /// <summary> 
		    /// 文件内容类型
            /// </summary>
            [Description("文件内容类型")]
		    public string ContentType { get; set; } 
          
            /// <summary> 
		    /// 文件大小
            /// </summary>
            [Description("文件大小")]
		    public int FileSize { get; set; } 
          
            /// <summary> 
		    /// 附件备注
            /// </summary>
            [Description("附件备注")]
		    public string Remark { get; set; } 
          
            /// <summary> 
		    /// 文件标识
            /// </summary>
            [Description("文件标识")]
		    public string FileKey { get; set; } 
          
            /// <summary> 
		    /// 文件Url
            /// </summary>
            [Description("文件Url")]
		    public string Url { get; set; } 
          
            /// <summary> 
		    /// 关联对象类型
            /// </summary>
            [Description("关联对象类型")]
		    public string RelationType { get; set; } 
          
            /// <summary> 
		    /// 关联对象id
            /// </summary>
            [Description("关联对象id")]
		    public int RelationID { get; set; } 
          
            /// <summary> 
		    /// 创建时间
            /// </summary>
            [Description("创建时间")]
		    public DateTime CreateTime { get; set; } 
          
            /// <summary> 
		    /// 创建人id
            /// </summary>
            [Description("创建人id")]
		    public int CreateUserID { get; set; } 
          
            /// <summary> 
		    /// 临时文件标识
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("临时文件标识")]
		    public bool IsTemp { get; set; } 
          
            /// <summary> 
		    /// 删除标识
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("删除标识")]
		    public bool IsDeleted { get; set; } 
		 
	}
}
