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
	public partial class ChineseSysRoleDataMapping
	{	 
          
            /// <summary> 
		    /// ??ID
            /// </summary>
            [Description("??ID")]			
		    public int RoleId { get; set; } 
          
            /// <summary> 
		    /// ??????ID
            /// </summary>
            [Description("??????ID")]			
		    public int DataPermId { get; set; } 
          
            /// <summary> 
		    /// ??????(1.??,2??????,3???,4????,5?????,6?)
            /// </summary>
            [Description("??????(1.??,2??????,3???,4????,5?????,6?)")]			
		    public int DataPermType { get; set; } 
          
            /// <summary> 
		    /// ?????ID
            /// </summary>
            [Description("?????ID")]			
		    public int DepartmentId { get; set; } 
		 
	}
}
