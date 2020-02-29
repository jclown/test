using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Demo
{
    public class FollowUpQueryDto : DictQueryDto
    {
        /// <summary> 
        /// 跟进ID。
        /// </summary>
        [JsonIgnore]
        public int FollowUpID { get; set; }
    }
}
