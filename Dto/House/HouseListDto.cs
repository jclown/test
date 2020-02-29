using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.House
{
    public class HouseListDto
    {
        /// <summary>
        /// 房源Id
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
        ///房源类型2：二手房,3:租售
        ///</summary> 
        public int? HouseSource { get; set; }

        ///<summary>
        ///是否合作发布
        ///</summary> 
        public int? IsMls { get; set; }

        ///<summary>
        /// 卧室数量
        ///</summary> 
        [JsonIgnore]
        public int? BedRoomsCount { get; set; }

        ///<summary>
        /// 客厅数
        ///</summary> 
        [JsonIgnore]
        public int? TingRoomsCount { get; set; }

        ///<summary>
        /// 卫生间数量
        ///</summary> 
        [JsonIgnore]
        public int? BathRoomsCount { get; set; }

        /// <summary>
        /// 户型
        /// </summary>
        public string HouseLayout
        {
            get
            {
                string res = $"{this.BedRoomsCount}室";
                if (TingRoomsCount > 0)
                {
                    res += $"{this.TingRoomsCount}厅";
                }
                //if (BathRoomsCount > 0)
                //{
                //    res += $"{this.BathRoomsCount}卫";
                //}
                return res;
            }
        }

        ///<summary>
        /// 面积
        ///</summary> 
        public double? Areas { get; set; }

        ///<summary>
        /// 总价(单位:元)
        ///</summary> 
        public decimal? TotalPrice { get; set; }

        ///<summary>
        /// 租金
        ///</summary> 
        public decimal? RentPrice { get; set; }

        /// <summary>
        /// 均价
        /// </summary>
        public int? AveragePrice { get; set; }

        /// <summary>
        /// 是否有VR
        /// </summary>
        public int? IsUseVR { get; set; }
        /// <summary>
        /// 朝向
        /// </summary>
        [JsonIgnore]
        public EnumOrientationType OrientationType { get; set; }

        /// <summary>
        /// 朝向
        /// </summary>
        public string Orientation
        {
            get
            {
                return this.OrientationType.GetDescription();
            }
        }

        /////<summary>
        ///// 楼层高低(code:FloorType)
        /////</summary> 
        //public EnumFloorType? FloorType { get; set; }

        /// <summary>
        /// 总楼层
        /// </summary>
        public int? TotalFloor { get; set; }
        /// <summary>
        /// 所在楼层
        /// </summary>
        public int? Floor { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        ///// <summary>
        ///// 首次合作发布时间
        ///// </summary>
        //public DateTime? FirstMlsTime { get; set; }

        /// <summary>
        /// 是否独家
        /// </summary>
        public int? IsSoleAgent { get; set; }
    }
}
