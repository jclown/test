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
	public partial class ChineseHouseType
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
		    /// ??ID
            /// </summary>
            [Description("??ID")]			
		    public int? HouseId { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public int? BedRoomsCount { get; set; } 
          
            /// <summary> 
		    /// ???
            /// </summary>
            [Description("???")]			
		    public int? TingRoomsCount { get; set; } 
          
            /// <summary> 
		    /// ?????
            /// </summary>
            [Description("?????")]			
		    public int? BathRoomsCount { get; set; } 
          
            /// <summary> 
		    /// ??
            /// </summary>
            [Description("??")]			
		    public decimal? Areas { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public decimal? TotalPrice { get; set; } 
          
            /// <summary> 
		    /// ??  ? = 1,? = 2,? = 3,? = 4,?? = 5,?? = 6,?? = 7,?? = 8,?? = 9,?? = 10   
            /// </summary>
            [Description("??  ? = 1,? = 2,? = 3,? = 4,?? = 5,?? = 6,?? = 7,?? = 8,?? = 9,?? = 10   ")]			
		    public int? OrientationType { get; set; } 
          
            /// <summary> 
		    /// ??  (1??,2??,3??)           ?? = 2,           ??? = 3,           ???? = 4
            /// </summary>
            [Description("??  (1??,2??,3??)           ?? = 2,           ??? = 3,           ???? = 4")]			
		    public int? Status { get; set; } 
          
            /// <summary> 
		    /// ?????
            /// </summary>
            [Description("?????")]			
		    public string? Photos { get; set; } 
          
            /// <summary> 
		    /// ???
            /// </summary>
            [Description("???")]			
		    public string? MainPhoto { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public string? HouseTag { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public int? BalconyCount { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public string? HouseTypeDesc { get; set; } 
          
            /// <summary> 
		    /// ??VR???
            /// </summary>
            [Description("??VR???")]			
		    public string? HouseTypeVrImage { get; set; } 
          
            /// <summary> 
		    /// ??VR??
            /// </summary>
            [Description("??VR??")]			
		    public string? HouseTypeVrUrl { get; set; } 
          
            /// <summary> 
		    /// ??1 : ??2 : ?? 3 : ?? 4 : ?? 5.????   
            /// </summary>
            [Description("??1 : ??2 : ?? 3 : ?? 4 : ?? 5.????   ")]			
		    public int? DesignType { get; set; } 
          
            /// <summary> 
		    /// ????  1 : ?? 2 : ?? 3 : ?? 4 : ?? 5???
            /// </summary>
            [Description("????  1 : ?? 2 : ?? 3 : ?? 4 : ?? 5???")]			
		    public int? PropertyType { get; set; } 
          
            /// <summary> 
		    /// ????
            /// </summary>
            [Description("????")]			
		    public int? IsDelete { get; set; } 
		 
	}
}
