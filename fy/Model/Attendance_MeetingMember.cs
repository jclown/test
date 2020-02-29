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
	public partial class Attendance_MeetingMember
	{	 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Key] // 主键
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 自增
            [Description("")]			
		    public int Id { get; set; } 
          
            /// <summary> 
		    /// 会议ID
            /// </summary>
            [Description("会议ID")]			
		    public int MeetingId { get; set; } 
          
            /// <summary> 
		    /// 创建时间
            /// </summary>
            [Description("创建时间")]			
		    public DateTime CreateTime { get; set; } 
          
            /// <summary> 
		    /// 关联用户ID
            /// </summary>
            [Description("关联用户ID")]			
		    public int UserId { get; set; } 
          
            /// <summary> 
		    /// 签到时间
            /// </summary>
            [Description("签到时间")]			
		    public DateTime? SignInTime { get; set; } 
          
            /// <summary> 
		    /// 签到状态
            /// </summary>
            [Description("签到状态")]			
		    public int SignInStatus { get; set; } 
          
            /// <summary> 
		    /// 是否已读
            /// </summary>
            [Description("是否已读")]			
		    public int IsRead { get; set; } 
          
            /// <summary> 
		    /// 确认（已读）时间
            /// </summary>
            [Description("确认（已读）时间")]			
		    public DateTime? ReadTime { get; set; } 
          
            /// <summary> 
		    /// 状态
            /// </summary>
            [Description("状态")]			
		    public int Status { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int IsRemind { get; set; } 
		 
	}
}
