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
	public partial class CustomerDemandCities
	{	 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int CustomerDemandId { get; set; } 
          
            /// <summary> 
		    /// 
            /// </summary>
            [Description("")]			
		    public int CityId { get; set; } 
		 
	}
}
