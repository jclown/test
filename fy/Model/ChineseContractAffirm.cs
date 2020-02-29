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
	public partial class ChineseContractAffirm
	{	 
          
            /// <summary> 
		    /// 主键ID
            /// </summary>
		    [Key] // 主键
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 自增
            [Description("主键ID")]			
		    public int Id { get; set; } 
          
            /// <summary> 
		    /// 城市ID
            /// </summary>
            [Description("城市ID")]			
		    public int? CityId { get; set; } 
          
            /// <summary> 
		    /// 发布者Id
            /// </summary>
            [Description("发布者Id")]			
		    public int? CreatorId { get; set; } 
          
            /// <summary> 
		    /// 发布者名称
            /// </summary>
            [Description("发布者名称")]			
		    public string? CreatorName { get; set; } 
          
            /// <summary> 
		    /// 创建时间
            /// </summary>
            [Description("创建时间")]			
		    public DateTime? CreateTime { get; set; } 
          
            /// <summary> 
		    /// 发布者Id
            /// </summary>
            [Description("发布者Id")]			
		    public int? UpateUserId { get; set; } 
          
            /// <summary> 
		    /// 更新时间
            /// </summary>
            [Description("更新时间")]			
		    public DateTime? UpdateTime { get; set; } 
          
            /// <summary> 
		    /// 商铺ID
            /// </summary>
            [Description("商铺ID")]			
		    public int? ShopId { get; set; } 
          
            /// <summary> 
		    /// 部门ID
            /// </summary>
            [Description("部门ID")]			
		    public int? DepartmentId { get; set; } 
          
            /// <summary> 
		    /// 合同ID
            /// </summary>
            [Description("合同ID")]			
		    public int ContractId { get; set; } 
          
            /// <summary> 
		    /// ??????????
            /// </summary>
            [Description("??????????")]			
		    public string OriginalOwner { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? NewOwnerId { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string NewOwner { get; set; } 
          
            /// <summary> 
		    /// ??????????
            /// </summary>
            [Description("??????????")]			
		    public DateTime AffirmDate { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int AffirmerId { get; set; } 
		 
	}
}
