using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.VisitorRecord
{
    public class HouseByVisitorRecordDto
    {

        /// <summary>
        /// 二手房Id 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 主图
        /// </summary>
        public string MainPhotoUrl { get; set; }

        /// <summary>
        /// 小区名称
        /// </summary>
        public string CommunityName { get; set; }

        ///<summary>
        /// 卧室数量
        ///</summary> 
        public int BedRoomsCount { get; set; }

        ///<summary>
        /// 客厅数
        ///</summary> 
        public int TingRoomsCount { get; set; }

        ///<summary>
        /// 卫生间数量
        ///</summary> 
        public int BathRoomsCount { get; set; }

        ///<summary>
        /// 面积
        ///</summary> 
        public double Areas { get; set; }

        ///<summary>
        /// 总价(单位:元)
        ///</summary> 
        public decimal? TotalPrice { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 行政区
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 客源方佣金 (单位%)
        /// </summary>
        public short CommissionPartner { get; set; }
    }
}
