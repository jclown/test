using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dto.Demand
{
    public class DemandListDto
    {
        /// <summary>
        /// 需求Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 需求类型 
        /// </summary>
        public EnumDemandEstateTypes EstateType { get; set; }

        /// <summary>
        /// 需求类型
        /// </summary>
        public string EstateTypeName { get { return ((int)HouseType).GetDescription<EnumDemandEstateTypes>(); } }

        /// <summary>
        /// 需求的房子类型
        /// </summary>
        public EnumDemandHouseTypes HouseType { get; set; }

        /// <summary>
        /// 需求的房子类型
        /// </summary>
        public string HouseTypeName { get { return ((int)HouseType).GetDescription<EnumDemandHouseTypes>(); } }

        /// <summary>
        /// 租房需求类型
        /// </summary>
        public EnumDemandRentingPlaceTypes RentingPlaceType { get; set; }

        /// <summary>
        /// 租房需求类型
        /// </summary>
        public string RentingPlaceTypeName { get { return ((int)HouseType).GetDescription<EnumDemandRentingPlaceTypes>(); } }

        ///<summary>
        ///是否合作发布
        ///</summary> 
        public bool IsMls { get; set; }

        /// <summary>
        /// 最大预算
        /// </summary>
        public int MaximumBudget { get; set; }
        /// <summary>
        /// 最小预算
        /// </summary>
        public int MinimumBudget { get; set; }

        /// <summary>
        /// 房子最小面积
        /// </summary>
        public int? MinimumHouseSize { get; set; }

        /// <summary>
        /// 最大面积
        /// </summary>
        public int? MaximumHouseSize { get; set; }

        /// <summary>
        /// 城市集合
        /// </summary>
        public List<string> CityArray { get; set; }

        /// <summary>
        /// 行政区集合
        /// </summary>
        public List<string> AreaArray { get; set; }

        /// <summary>
        /// 商圈集合
        /// </summary>
        public List<string> SubArray { get; set; }

        /// <summary>
        /// 小区集合
        /// </summary>
        public List<string> CommunityNameArray { get; set; }

        //[JsonIgnore]
        //public List<EnumDemandTagType> DemandTag { get; set; }

        /// <summary>
        /// 需求特色
        /// </summary>
        public List<string> DemandTagArray { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 匹配房源数量
        /// </summary>
        public int MatchHouseCount { get; set; }

        /// <summary>
        /// 卧室数量集合
        /// </summary>
        public List<string> BedroomsArray { get; set; }
    }
}
