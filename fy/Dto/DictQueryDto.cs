using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Dto
{
    public class DictQueryDto : QueryDto
    {
        /// <summary> 
        /// 状态
        /// </summary>
        public string DictItemCode { get; set; }
    }
}
