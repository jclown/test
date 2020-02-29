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
	public partial class ChineseSysDataPerm
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
		    /// ????
            /// </summary>
            [Description("????")]			
		    public string? Name { get; set; } 
          
            /// <summary> 
		    /// ???? (1 ??????,2 ???????)
            /// </summary>
            [Description("???? (1 ??????,2 ???????)")]			
		    public int? PermType { get; set; } 
          
            /// <summary> 
		    /// ??ID
            /// </summary>
            [Description("??ID")]			
		    public int? ParentId { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public string? PermDescription { get; set; } 
          
            /// <summary> 
		    /// ??
            /// </summary>
            [Description("??")]			
		    public int? Level { get; set; } 
          
            /// <summary> 
		    /// ???
            /// </summary>
            [Description("???")]			
		    public int? Sort { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public bool? IsUse { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public bool? IsShow { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string? PermCode { get; set; } 
		 
	}
}
