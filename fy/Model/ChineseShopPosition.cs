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
	public partial class ChineseShopPosition
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
		    public int ShopId { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public string Name { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? PositionLevel { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int Role { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int HousingResources { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int CustomerResources { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("")]			
		    public bool ManageStaff { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("")]			
		    public bool ManageContract { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("")]			
		    public bool ManagePosition { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public bool? ManageShopSetting { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("")]			
		    public bool IsMustFollowUp { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("")]			
		    public bool CanSaveHouse { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("")]			
		    public bool CanDeleteHouse { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("")]			
		    public bool CheckFollowUp { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("")]			
		    public bool CanExportExcel { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("")]			
		    public bool ShopShare { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public DateTime UpdateTime { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public DateTime CreateTime { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("")]			
		    public bool CanDeleteSelfFollowUp { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("")]			
		    public bool ManageFollowUp { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("")]			
		    public bool ManageRecycled { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("")]			
		    public bool CanEditHouseCore { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("")]			
		    public bool CheckReportCenter { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Column(TypeName = "bit")]
            [Description("")]			
		    public bool CanSaveNewHouse { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public bool? CanSeeInviteMember { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public bool? CanRecordsExportExcel { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public bool? CanSeeAllApplyRecords { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public bool? CanSeeAllGetRecords { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public bool? ManageAttendanceGroup { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public bool? ManageMeeting { get; set; } 
		 
	}
}
