using Dto.Report;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Demand
{
    public class DemandQueryDto:QueryDto
    {
        /// <summary>
        /// 经纪人Id
        /// </summary>
        public int UserId { get; set; }
    }
}
