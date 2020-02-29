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
	public partial class Agent_ChangeStatisticsToDay
	{	 
          
            /// <summary> 
		    /// 
            /// </summary>
		    [Key] // 主键
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 自增
            [Description("")]			
		    public int Id { get; set; } 
          
            /// <summary> 
		    /// 区域id
            /// </summary>
            [Description("区域id")]			
		    public int HouseGroupId { get; set; } 
          
            /// <summary> 
		    /// 日期
            /// </summary>
            [Description("日期")]			
		    public DateTime Date { get; set; } 
          
            /// <summary> 
		    /// 新注册公司数量(当天)
            /// </summary>
            [Description("新注册公司数量(当天)")]			
		    public int RegCompanyDayCount { get; set; } 
          
            /// <summary> 
		    /// 新认证公司数量(当天)
            /// </summary>
            [Description("新认证公司数量(当天)")]			
		    public int NewCertCompanyDayCount { get; set; } 
          
            /// <summary> 
		    /// 新房浏览数量(当天)
            /// </summary>
            [Description("新房浏览数量(当天)")]			
		    public int NewHouseBrowseDayCount { get; set; } 
          
            /// <summary> 
		    /// 二手房浏览量(当天)
            /// </summary>
            [Description("二手房浏览量(当天)")]			
		    public int SecondHouseBrowseDayCount { get; set; } 
          
            /// <summary> 
		    /// 出租房浏览量(当天)
            /// </summary>
            [Description("出租房浏览量(当天)")]			
		    public int RentHouseBrowseDayCount { get; set; } 
          
            /// <summary> 
		    /// 新注册用户数(当天)
            /// </summary>
            [Description("新注册用户数(当天)")]			
		    public int RegUserDayCount { get; set; } 
          
            /// <summary> 
		    /// 活跃用户数(当天)
            /// </summary>
            [Description("活跃用户数(当天)")]			
		    public int ActiveUserDayCount { get; set; } 
          
            /// <summary> 
		    /// 电话联系数量(当天)
            /// </summary>
            [Description("电话联系数量(当天)")]			
		    public int MobileConnectDayCount { get; set; } 
          
            /// <summary> 
		    /// IM联系数量(当天)
            /// </summary>
            [Description("IM联系数量(当天)")]			
		    public int IMConnectDayCount { get; set; } 
		 
	}
}
