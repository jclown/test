using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Dto
{
    public class StatusQueryDto : QueryDto
    {
        /// <summary> 
        /// 状态
        /// </summary>
        //[Required]
        //[Required(ErrorMessage = "请输入手机号码")]
        //[RegularExpression(@"^1[3|4|5|7|8][0-9]\d{8}$", ErrorMessage = "手机号格式错误")]
        //[Range(1, 3, ErrorMessage = "状态必须介于1-3")]
        public int Status { get; set; }                 
    }
   
}
