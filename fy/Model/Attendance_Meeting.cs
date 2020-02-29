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
	public partial class Attendance_Meeting
	{	 
          
            /// <summary> 
		    /// 主键
            /// </summary>
		    [Key] // 主键
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 自增
            [Description("主键")]			
		    public int Id { get; set; } 
          
            /// <summary> 
		    /// 城市ID
            /// </summary>
            [Description("城市ID")]			
		    public int? CityId { get; set; } 
          
            /// <summary> 
		    /// 创建者ID
            /// </summary>
            [Description("创建者ID")]			
		    public int CreatorId { get; set; } 
          
            /// <summary> 
		    /// 创建时间
            /// </summary>
            [Description("创建时间")]			
		    public DateTime CreateTime { get; set; } 
          
            /// <summary> 
		    /// 更新者ID
            /// </summary>
            [Description("更新者ID")]			
		    public int? UpateUserId { get; set; } 
          
            /// <summary> 
		    /// 更新时间
            /// </summary>
            [Description("更新时间")]			
		    public DateTime? UpdateTime { get; set; } 
          
            /// <summary> 
		    /// 企业ID
            /// </summary>
            [Description("企业ID")]			
		    public int ShopId { get; set; } 
          
            /// <summary> 
		    /// 部门ID
            /// </summary>
            [Description("部门ID")]			
		    public int DepartmentId { get; set; } 
          
            /// <summary> 
		    /// 会议名
            /// </summary>
            [Description("会议名")]			
		    public string MeetingName { get; set; } 
          
            /// <summary> 
		    /// 开始时间
            /// </summary>
            [Description("开始时间")]			
		    public DateTime StartTime { get; set; } 
          
            /// <summary> 
		    /// 结束时间
            /// </summary>
            [Description("结束时间")]			
		    public DateTime EndTime { get; set; } 
          
            /// <summary> 
		    /// 参会人员
            /// </summary>
            [Description("参会人员")]			
		    public string? Attendees { get; set; } 
          
            /// <summary> 
		    /// 会议地点
            /// </summary>
            [Description("会议地点")]			
		    public string MeetingPlace { get; set; } 
          
            /// <summary> 
		    /// 会议描述
            /// </summary>
            [Description("会议描述")]			
		    public string? Description { get; set; } 
          
            /// <summary> 
		    /// 是否需要签到
            /// </summary>
            [Description("是否需要签到")]			
		    public int IsNeedSign { get; set; } 
          
            /// <summary> 
		    /// 签到条件 默认0无需到场签到
            /// </summary>
            [Description("签到条件 默认0无需到场签到")]			
		    public int IsNeedConditionSignIn { get; set; } 
          
            /// <summary> 
		    /// 签到地点
            /// </summary>
            [Description("签到地点")]			
		    public string? SignInLocation { get; set; } 
          
            /// <summary> 
		    /// 签到地点-经度
            /// </summary>
            [Description("签到地点-经度")]			
		    public decimal? SignInLongitude { get; set; } 
          
            /// <summary> 
		    /// 签到地点-纬度
            /// </summary>
            [Description("签到地点-纬度")]			
		    public decimal? SignInLatitude { get; set; } 
          
            /// <summary> 
		    /// 签到范围
            /// </summary>
            [Description("签到范围")]			
		    public int? AllowSignInRange { get; set; } 
          
            /// <summary> 
		    /// 设置缺席时间
            /// </summary>
            [Description("设置缺席时间")]			
		    public int? AbsenteeismTimeRange { get; set; } 
          
            /// <summary> 
		    /// 签到是否需要连接WIFI
            /// </summary>
            [Description("签到是否需要连接WIFI")]			
		    public int? IsNeedWifiSignIn { get; set; } 
          
            /// <summary> 
		    /// WIFI名
            /// </summary>
            [Description("WIFI名")]			
		    public string? WIFIName { get; set; } 
          
            /// <summary> 
		    /// WIFISSID
            /// </summary>
            [Description("WIFISSID")]			
		    public string? WIFISSID { get; set; } 
          
            /// <summary> 
		    /// 会议负责人
            /// </summary>
            [Description("会议负责人")]			
		    public string? Manager { get; set; } 
          
            /// <summary> 
		    /// 相关部门
            /// </summary>
            [Description("相关部门")]			
		    public string? RelevantDepartment { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int? IsRevoke { get; set; } 
		 
	}
}
