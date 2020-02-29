using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Report
{
    /// <summary>
    /// xxx
    /// </summary>
    public class RankItemDto 
    {
        /// <summary>
        /// 排行
        /// </summary>
        public int RankIndex { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo User { get; set; }
    }
}
