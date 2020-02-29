using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.House
{
    /// <summary>
	///ChineseHouseNew数据实体
	/// </summary>
    public class HouseNewItem
    {

        ///<summary>
        ///
        ///</summary> 
        public int HouseId
        {
            get; set;
        }


        ///<summary>
        ///
        ///</summary> 
        public int HouseSource
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public int IsPublic
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public int CommunityId
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public string CommunityName
        {
            get; set;
        }

        public int FloorType { get; set; }


        ///<summary>
        ///
        ///</summary> 
        public string TotalFloor
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public string Floor
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public string BedRoomsCount
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public string TingRoomsCount
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public string BathRoomsCount
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public string Areas
        {
            get; set;
        }

        public int OrientationType
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public string TotalPrice
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public string RentPrice
        {
            get; set;
        }


        ///<summary>
        ///
        ///</summary> 
        public string[] RentTypeArray
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public string[] FeatureArray
        {
            get; set;
        }






        ///<summary>
        ///
        ///</summary> 
        public string Title
        {
            get; set;
        }


        ///<summary>
        ///
        ///</summary> 
        public int ShareToFdb
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public string MainPhoto
        {
            get; set;
        }


        public string MainPhoto_S
        {
            get; set;
        }

        public string MainPhoto_M
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public string CommissionType
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public string CommissionPercent
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public string CommissionMonth
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public string CommissionTotal
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public string CommissionPartnerType
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public string CommissionPartnerPercent
        {
            get; set;
        }

        ///<summary>
        ///
        ///</summary> 
        public string CommissionPartner
        {
            get; set;
        }


        ///<summary>
        ///
        ///</summary> 
        public int MaintainUserId
        {
            get; set;
        }


        ///<summary>
        ///
        ///</summary> 
        public DateTime CreatedDate
        {
            get; set;
        }


        ///<summary>
        ///
        ///</summary> 
        public int Status
        {
            get; set;
        }

        /// <summary>
        /// 维护人名称
        /// </summary>
        public string MaintainName
        {
            get; set;
        }

        /// <summary>
        /// 单价
        /// </summary>
        public string UnitPrice
        {
            get; set;
        }

        /// <summary>
        /// 详情链接
        /// </summary>
        public string DetailUrl
        {
            get; set;
        }


        public int PayBy { get; set; }

        public int IsSoleAgent { get; set; }

        public bool IsHasFollowUpAuth { get; set; }
        public bool IsHasEditAuth { get; set; }

        public int LikeNum { get; set; }
        public int ClickNum { get; set; }
        public int CollectNum { get; set; }

        ///<summary>
        ///是否已收藏
        ///</summary> 
        public int IsCollect
        {
            get; set;
        }


        public int IsLike
        {
            get; set;
        }

        public int IsUseVR
        {
            get; set;
        }

        public string VrUrlPath
        {
            get; set;
        }

        public string VrMainPhoto
        {
            get; set;
        }


        public string MainPhotoUrl
        {
            get; set;
        }

        public int? HasKey
        {
            get; set;
        }

        public int? KeyId
        {
            get; set;
        }


        public string HoldYearsType { get; set; }
        public string IsElevator { get; set; }
        public string DesignType { get; set; }
        public string IsOnlyProperty { get; set; }
        public string[] GenderLimitArray { get; set; }
        public string[] FacilitysArray
        {
            get; set;
        }
        public string NumberPlate { get; set; }
        public int NumberPlateType { get; set; }
        public string NumberPlateSec { get; set; }
        public int NumberPlateSecType { get; set; }
        public string Address { get; set; }
        public string FullAddress { get; set; }
        public int IsMustFollowUp { get; set; }
        public int CanActive { get; set; }
        public int IsValidActivation { get; set; }
        public int IsCollective { get; set; }
        public int? DeleterId { get; set; }
        public string DeleterName { get; set; }
        public DateTime? LastDeleteTime { get; set; }
        public string StatusName { get; set; }
        public string HouseSourceName { get; set; }
        public DateTime? FollowUpLastData { get; set; }
        public DateTime? LastActiveTime { get; set; }
        public string Contacter { get; set; }
        public string LastFollowUpName { get; set; }
        public int IsTop { get; set; }

        //public HouseNewItem(HouseNewExt model)
        //{
        //    if (model != null)
        //    {
        //        try
        //        {
        //            this.HouseId = model.Id;
        //            this.HouseSource = model.HouseSource;
        //            this.IsPublic = model.IsPublic;
        //            this.CommunityId = model.CommunityId;
        //            this.CommunityName = model.CommunityName.ToStr().Trim();
        //            this.FloorType = model.FloorType;
        //            this.TotalFloor = model.TotalFloor.ToStr("");
        //            this.Floor = model.Floor.ToStr("");
        //            this.BedRoomsCount = model.BedRoomsCount.ToStr("");
        //            this.TingRoomsCount = model.TingRoomsCount.ToStr("");
        //            this.BathRoomsCount = model.BathRoomsCount.ToStr("");
        //            this.Areas = model.Areas.ToStr("");
        //            this.TotalPrice = (model.TotalPrice != null && model.TotalPrice != 0) ? model.TotalPrice.ToString() : "";
        //            this.RentPrice = (model.RentPrice != null && model.RentPrice != 0) ? model.RentPrice.ToString() : "";
        //            this.Title = model.Title.ToStr();
        //            this.ShareToFdb = model.ShareToFdb;
        //            this.CommissionType = model.CommissionType.ToStr("");
        //            this.CommissionPercent = model.CommissionPercent.ToStr("");
        //            this.CommissionMonth = model.CommissionMonth.ToStr("");
        //            this.CommissionTotal = model.CommissionTotal.ToStr("");
        //            this.CommissionPartnerType = model.CommissionPartnerType.ToStr("");
        //            this.CommissionPartnerPercent = model.CommissionPartnerPercent.ToStr("");
        //            this.CommissionPartner = model.CommissionPartner.ToStr("");
        //            this.MaintainUserId = model.MaintainUserId;
        //            this.CreatedDate = model.CreatedDate;
        //            this.Status = model.Status;
        //            this.RentTypeArray = JsonConvertHelper.DeserializeObject<string[]>(model.RentTypes, new string[] { });
        //            this.FeatureArray = JsonConvertHelper.DeserializeObject<string[]>(model.Features, new string[] { });
        //            this.MaintainName = model.MaintainName.ToStr();
        //            if (string.IsNullOrEmpty(this.MaintainName))
        //            {
        //                this.MaintainName = model.MaintainUserName;
        //            }
        //            this.PayBy = model.PayBy;
        //            this.OrientationType = model.OrientationType.ToInt();
        //            this.IsSoleAgent = model.IsSoleAgent.ToInt();

        //            if (model.ActiveWay == (int)ActiveWayEnum.经纪人激活)
        //            {
        //                if (model.LastActiveTime != null)
        //                {
        //                    this.LastActiveTime = model.LastActiveTime;
        //                }
        //            }

        //            if (model.TotalPrice > 0 && model.Areas > 0)
        //            {
        //                this.UnitPrice = ((int)(model.TotalPrice / (decimal)model.Areas)).ToString();
        //            }


        //            /*   switch (model.HouseSource)
        //               {
        //                   case (int)Modobay.Core.Entity.Enums.HouseSource.在售:
        //                       this.RentPrice = "";
        //                       break;
        //                   case (int)Modobay.Core.Entity.Enums.HouseSource.在租:
        //                       this.TotalPrice = "";
        //                       this.UnitPrice = "";
        //                       break;
        //               }*/

        //            if (!string.IsNullOrEmpty(this.RentPrice) && DataValidator.IsIntByDecimal(this.RentPrice))
        //            {
        //                this.RentPrice = this.RentPrice.ToInt().ToString();
        //            }


        //            if (model.FollowUpLastData != null)
        //            {
        //                this.FollowUpLastData = model.FollowUpLastData;
        //            }


        //            if (this.MaintainUserId <= 0 && !this.MaintainName.IsNotEmpty())
        //            {
        //                this.MaintainName = "公共盘源";
        //            }


        //            this.DetailUrl = string.Format("/modohouse/company-house-detail?id={0}", model.Id).GetSiteUrl();

        //            if (!string.IsNullOrEmpty(model.MainPhoto))
        //            {
        //                this.MainPhoto = model.MainPhoto.GetImgPath();
        //            }
        //            else
        //            {
        //                if (!string.IsNullOrEmpty(model.Photos) && model.Photos != "[]")
        //                {
        //                    this.MainPhoto = model.Photos.DeserializeObject<List<string>>().Select(s => s.GetImgPath()).ToList()[0];
        //                }
        //            }
        //            this.MainPhotoUrl = this.MainPhoto;
        //            this.MainPhoto_S = IoHelper.GetImgBySize(this.MainPhotoUrl.GetImgPath(), "60x60");
        //            this.MainPhoto_M = IoHelper.GetImgBySize(this.MainPhotoUrl.GetImgPath(), "220x220");

        //            this.LikeNum = model.LikeNum;
        //            this.ClickNum = model.ClickNum;
        //            this.CollectNum = model.CollectNum;
        //            this.IsLike = model.IsLike;
        //            this.IsCollect = model.IsCollect;
        //            this.IsUseVR = model.IsUseVR;
        //            this.VrUrlPath = model.VrUrlPath != null ? model.VrUrlPath : "";
        //            this.VrMainPhoto = model.VrMainPhoto.GetImgPath() != null ? model.VrMainPhoto.GetImgPath() : "";
        //            this.HasKey = model.HasKey != null ? model.HasKey : 0;
        //            this.KeyId = model.KeyId != null ? model.KeyId : 0;

        //            this.HoldYearsType = model.HoldYearsType.ToStr();
        //            this.IsElevator = model.IsElevator.ToStr();
        //            this.DesignType = model.DesignType.ToStr();
        //            this.IsOnlyProperty = model.IsOnlyProperty.ToStr();
        //            this.FacilitysArray = JsonConvertHelper.DeserializeObject<string[]>(model.Facilities, new string[] { });
        //            this.GenderLimitArray = JsonConvertHelper.DeserializeObject<string[]>(model.GenderLimit, new string[] { });
        //            //this.RentingBedroom = model.RentingBedroom;

        //            this.NumberPlate = model.NumberPlate.ToStr();
        //            this.NumberPlateType = model.NumberPlateType;
        //            this.NumberPlateSec = model.NumberPlateSec.ToStr();
        //            this.NumberPlateSecType = model.NumberPlateSecType;
        //            this.Address = model.Address.ToStr();
        //            this.FullAddress = AddressConver(model);

        //            if (model.ActiveWay != null)
        //            {
        //                if (model.ActiveWay == (int)ActiveWayEnum.经纪人激活)
        //                {
        //                    this.IsValidActivation = (int)ActiveState.有效激活;
        //                }
        //            }
        //            this.IsCollective = model.IsCollective;

        //            if (model.IsDel == 1)
        //            {
        //                this.DeleterId = model.DeleterId != null ? model.DeleterId : 0;
        //                this.DeleterName = model.DeleterName != null ? model.DeleterName : "";
        //                if (model.LastDeleteTime != null)
        //                {
        //                    this.LastDeleteTime = model.LastDeleteTime;
        //                }
        //            }

        //            this.StatusName = ((Status)model.Status).ToString();

        //            this.HouseSourceName = ((Modobay.Core.Entity.Enums.HouseSource)model.HouseSource).ToString();
        //            this.Contacter = model.Contacter;
        //            this.LastFollowUpName = model.LastFollowUpName;
        //            this.IsTop = model.IsTop;
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }

        //    }
        //}

        //string AddressConver(HouseNewExt model)
        //{
        //    model.FullAddress = "";
        //    if (model.NumberPlateType != 8)
        //    {
        //        model.FullAddress += model.NumberPlate + ((NumberPlateType)model.NumberPlateType).ToString();
        //    }
        //    if (model.NumberPlateType == 8 && model.NumberPlate.IsNotEmpty())
        //    {
        //        model.FullAddress += model.NumberPlate;
        //    }


        //    if (model.NumberPlateSecType != 6)
        //    {
        //        model.FullAddress += model.NumberPlateSec + ((NumberPlateSecType)model.NumberPlateSecType).ToString();
        //    }
        //    model.FullAddress += "" + model.Address;
        //    return model.FullAddress;
        //}
    }
}
