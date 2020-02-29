using Dto.Demand;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Contact
{
    public class HouseByContactDto
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
        [JsonIgnore]
        public int BedRoomsCount { get; set; }

        ///<summary>
        /// 客厅数
        ///</summary> 
        [JsonIgnore]
        public int TingRoomsCount { get; set; }

        ///<summary>
        /// 卫生间数量
        ///</summary> 
        [JsonIgnore]
        public int BathRoomsCount { get; set; }

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
        /// 价格
        /// 出租房:XXX元
        /// 二手房:YYY万元
        ///</summary> 
        public string HousePrice
        {
            get
            {
                string res = string.Empty;
                switch (this.ResaleHouseCommodityType)
                {
                    case EnumResaleHouseCommodityType.Rent:
                        res = $"{this.RentPrice.GetValueOrDefault(0).ToString("f0")}元";
                        break;
                    case EnumResaleHouseCommodityType.Sale:
                        res = $"{(this.TotalPrice.GetValueOrDefault(0) / 10000).ToString("f0")}万元";
                        break;
                    case EnumResaleHouseCommodityType.RentAndSale:
                        goto case EnumResaleHouseCommodityType.Sale;
                    default:
                        break;
                }
                return res;
            }
        }

        /// <summary> 
        /// 租价
        /// </summary>
        [JsonIgnore]
        public decimal? RentPrice { get; set; }

        /// <summary> 
        /// 售价
        /// </summary>
        [JsonIgnore]
        public decimal? TotalPrice { get; set; }

        /// <summary>
        /// 二手房商品类型
        /// </summary>
        [JsonIgnore]
        public EnumResaleHouseCommodityType ResaleHouseCommodityType { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string CityName
        { get; set; }

        /// <summary>
        /// 行政区
        /// </summary>
        public string AreaName { get; set; }

        ///<summary>
        ///合租顾问佣金类型
        ///关联枚举：HouseCommissionType
        ///比例
        ///固定金额
        ///</summary> 
        [JsonIgnore]
        public int CommissionPartnerType { get; set; }

        ///<summary>
        /// 顾问佣金：比例
        ///</summary> 
        [JsonIgnore]
        public double CommissionPartnerPercent { get; set; }

        ///<summary>
        ///顾问佣金：固定金额
        ///</summary> 
        [JsonIgnore]
        public double CommissionPartner { get; set; }

        /// <summary>
        /// 客源方佣金
        /// </summary>
        public string Commission
        {
            get
            {
                if (this.CommissionPartnerType <= 0)
                {
                    return string.Empty;
                    // todo djl 不要在dto抛异常
                    //throw new Exception("合租顾问佣金类型不能为空");
                }
                if (this.CommissionPartnerType == 1)//比例
                {
                    return $"{Convert.ToInt32(this.CommissionPartnerPercent * 100)}%";
                }
                if (this.CommissionPartnerType == 3)//固定金额
                {
                    return $"{this.CommissionPartnerPercent}元";
                }
                if (this.CommissionPartnerType == 5)//各收各佣金
                {
                    return "各收各佣";
                }
                return string.Empty;
            }
        }

        ///<summary>
        ///是否合作发布
        ///</summary> 
        public bool IsMls { get; set; }
    }
}
