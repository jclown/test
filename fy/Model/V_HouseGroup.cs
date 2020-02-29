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
	public partial class V_HouseGroup
	{	 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int CompanyId { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int CurCompanyId { get; set; } 
		 
	}
}
