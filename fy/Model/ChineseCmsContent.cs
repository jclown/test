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
	public partial class ChineseCmsContent
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
		    public int? CityId { get; set; } 
          
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
		    /// ????
            /// </summary>
            [Description("????")]			
		    public string? CityName { get; set; } 
          
            /// <summary> 
		    /// ??ID
            /// </summary>
            [Description("??ID")]			
		    public int? ColumnId { get; set; } 
          
            /// <summary> 
		    /// ??
            /// </summary>
            [Description("??")]			
		    public string? Title { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public string? ImgUrl { get; set; } 
          
            /// <summary> 
		    /// ??
            /// </summary>
            [Description("??")]			
		    public string? Description { get; set; } 
          
            /// <summary> 
		    /// ??
            /// </summary>
            [Description("??")]			
		    public string? Content { get; set; } 
          
            /// <summary> 
		    /// ???
            /// </summary>
            [Description("???")]			
		    public int? FollowUpPeople { get; set; } 
          
            /// <summary> 
		    /// ???
            /// </summary>
            [Description("???")]			
		    public int? ClickNum { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public string? ExternalLinks { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? LawyerId { get; set; } 
		 
	}
}
