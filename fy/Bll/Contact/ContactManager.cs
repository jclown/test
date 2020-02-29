using Dal;
using Dto.House;
using Modobay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Dto.Contact;
using Model;
using Dto;
using Dto.Demand;
using Microsoft.EntityFrameworkCore;

namespace Bll.Contact
{
    [System.ComponentModel.Description("电话/IM联系")]
    public class ContactManager : IContactManager
    {
        private readonly MLSDbContext db;
        private readonly IAppContext app;

        public ContactManager(Dal.MLSDbContext dbContext, Modobay.IAppContext appContext)
        {
            this.db = dbContext;
            this.app = appContext;
        }

        /// <summary>
        /// 获取联络列表
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public PagedList<ContactListDto> Search(ContactQueryDto queryDto)
        {
            if (queryDto.StatItemType == Dto.Report.StatItemTypeEnum.Telephone)
            {
                return SearchByOutCall(queryDto);
            }
            else
            {
                return SearchByInCall(queryDto);
            }
        }

        /// <summary>
        /// 去电
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        private PagedList<ContactListDto> SearchByOutCall(ContactQueryDto queryDto)
        {
            var userQuery = db.GetUserQuery();
            var query = from contact in db.MlsContactRecordLogQuery
                        join housenew in db.ChineseHouseNewQuery on contact.ResourcesId equals housenew.Id into h
                        from housenew in h.DefaultIfEmpty()
                        join communities in db.ChineseCommunitiesQuery on housenew.CommunityId equals communities.Id into c
                        from communities in c.DefaultIfEmpty()
                        join user in userQuery on contact.Contacter equals user.PhoneNumber into aa
                        from user in aa.DefaultIfEmpty()
                        where (contact.CreateTime >= queryDto.DateS && contact.CreateTime <= queryDto.DateE)
                        //&& contact.HouseSourceType == 2
                        && user.ShopId > 0
                        orderby contact.CreateTime descending
                        select new
                        {
                            contact,
                            user,
                            extInfo = new
                            {
                                housenew.HouseNewId,
                                housenew.Areas,
                                housenew.TotalPrice,
                                housenew.CommissionPartner,
                                housenew.CommissionPartnerType,
                                housenew.CommissionPartnerPercent,
                                housenew.BedRoomsCount,
                                housenew.TingRoomsCount,
                                housenew.BathRoomsCount,
                                MainPhotoUrl = housenew.MainPhoto,
                                IsMls = housenew.ShareToFdb == 2,
                                CommunityName = communities.Name,
                                communities.CityName,
                                communities.AreaName
                            }
                        };

            //query = query.Where(x => x.contact.ShopId == app.User.ShopId && x.contact.UserId == queryDto.UserId);
            query = query.Where(x => x.contact.UserId == queryDto.UserId);

            var list = query.GetPagedList(queryDto.PageIndex, queryDto.PageSize);
            var data = new PagedList<ContactListDto>(list.CurrentPageIndex, list.PageSize, list.TotalItemCount);
            var userInfoList = db.CurrentCorpUserInfoList;
            list.ForEach(item => data.Add(ToDto(item.contact, item.user, item.extInfo)));
            return data;
        }

        /// <summary>
        /// 来电
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        private PagedList<ContactListDto> SearchByInCall(ContactQueryDto queryDto)
        {
            var userQuery = db.GetUserQuery();
            var query = from contact in db.MlsContactRecordLogQuery
                        join housenew in db.ChineseHouseNewQuery on contact.ResourcesId equals housenew.Id into h
                        from housenew in h.DefaultIfEmpty()
                        join communities in db.ChineseCommunitiesQuery on housenew.CommunityId equals communities.Id into c
                        from communities in c.DefaultIfEmpty()
                        join user in userQuery on contact.UserId equals user.UserId into aa
                        from user in aa.DefaultIfEmpty()
                        where (contact.CreateTime >= queryDto.DateS && contact.CreateTime <= queryDto.DateE)
                        //&& contact.HouseSourceType == 2
                         && user.ShopId > 0
                        orderby contact.CreateTime descending
                        select new
                        {
                            contact,
                            user,
                            extInfo = new
                            {
                                housenew.HouseNewId,
                                housenew.Areas,
                                housenew.TotalPrice,
                                housenew.CommissionPartner,
                                housenew.CommissionPartnerType,
                                housenew.CommissionPartnerPercent,
                                housenew.BedRoomsCount,
                                housenew.TingRoomsCount,
                                housenew.BathRoomsCount,
                                housenew.HouseSource,
                                housenew.RentPrice,
                                MainPhotoUrl = housenew.MainPhoto,
                                IsMls = housenew.ShareToFdb == 2,
                                CommunityName = communities.Name,
                                communities.CityName,
                                communities.AreaName
                            }
                        };

            var phoneNumber = db.CurrentCorpUserList.FirstOrDefault(x => x.UserId == queryDto.UserId)?.PhoneNumber;
            query = query.Where(x => x.contact.Contacter == phoneNumber);

            var list = query.GetPagedList(queryDto.PageIndex, queryDto.PageSize);
            var data = new PagedList<ContactListDto>(list.CurrentPageIndex, list.PageSize, list.TotalItemCount);
            var userInfoList = db.CurrentCorpUserInfoList;
            list.ForEach(item => data.Add(ToDto(item.contact, item.user, item.extInfo)));
            return data;
        }

        private ContactListDto ToDto(MlsContactRecordLog entity, UserInfo user, dynamic extInfo = null)
        {
            ContactListDto dto = default;
            try
            {
                dto = Lib.Mapper<MlsContactRecordLog, ContactListDto>.CloneDto(entity);
                dto.UserInfo = user;
                dto.HouseInfo = new HouseByContactDto();
                dto.HouseInfo.Id = extInfo.HouseNewId;
                dto.HouseInfo.Areas = extInfo.Areas;
                dto.HouseInfo.TotalPrice = extInfo.TotalPrice;
                dto.HouseInfo.CommissionPartnerType = extInfo.CommissionPartnerType ?? 0;
                dto.HouseInfo.CommissionPartner = extInfo.CommissionPartner ?? 0;
                dto.HouseInfo.CommissionPartnerPercent = extInfo.CommissionPartnerPercent ?? 0;
                dto.HouseInfo.BedRoomsCount = extInfo.BedRoomsCount ?? 0;
                dto.HouseInfo.RentPrice = extInfo.RentPrice ?? 0;
                dto.HouseInfo.ResaleHouseCommodityType = (EnumResaleHouseCommodityType)(extInfo.HousePrice ?? 0);
                dto.HouseInfo.TingRoomsCount = extInfo.TingRoomsCount ?? 0;
                dto.HouseInfo.BathRoomsCount = extInfo.BathRoomsCount ?? 0;
                dto.HouseInfo.MainPhotoUrl = extInfo.MainPhotoUrl ?? string.Empty;
                dto.HouseInfo.CommunityName = extInfo.CommunityName ?? string.Empty;
                dto.HouseInfo.AreaName = extInfo.AreaName ?? string.Empty;
                dto.HouseInfo.CityName = extInfo.CityName ?? string.Empty;
                dto.HouseInfo.IsMls = extInfo.IsMls ?? false;
            }
            catch (Exception ex) { Lib.Log.WriteExceptionLog($"ContactManager.ToDto:Message:{ex.Message}  <br> StackTrace:{ex.StackTrace}"); }
            return dto;
        }

        /// <summary>
        /// 二手房的联络列表
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public ContactByResaleHouseDto SearchByResaleHouseId(ContactByResaleHouseQueryDto queryDto)
        {
            var userQuery = db.GetUserQuery();
            var query = from contact in db.MlsContactRecordLogQuery
                        join user in userQuery on contact.UserId equals user.UserId
                        where contact.ResourcesId == queryDto.HouseId && contact.Type == (int)EnumHosueCommonType.ResaleHouse
                        orderby contact.CreateTime descending
                        select new ResaleHouseContactListDto
                        {
                            UserInfo = user,
                            OperationType = (ContactEnum)contact.OperationType,
                            CreateTime = contact.CreateTime,
                            //HowTimes = 10,
                            Id = contact.Id
                        };
            PagedList<ResaleHouseContactListDto> list = query.GetPagedList(queryDto.PageIndex, queryDto.PageSize);

            if (list.Any())
            {
                var sqlList = new List<string>();
                foreach (var item in list)
                {
                    sqlList.Add($"SELECT CONVERT(bigint,{item.Id}) AS ID ,COUNT(0) AS Value,NULL AS Code FROM  MlsContactRecordLog WHERE Id<={item.Id} AND UserId={item.UserInfo.UserId} AND OperationType={(int)item.OperationType}");
                }
                var sql = string.Join(" UNION ALL ", sqlList);
                foreach (var item in this.db.IDValueDto.FromSqlRaw(sql))
                {
                    list.Where(c => c.Id == (int)item.ID).First().HowTimes = item.Value;
                }
            }

            var data = new PagedList<ResaleHouseContactListDto>(list.CurrentPageIndex, list.PageSize, list.TotalItemCount);
            data.AddRange(list);

            var res = new ContactByResaleHouseDto()
            {
                ContactPage = data
            };

            res.HouseInfo = (from housenew in db.ChineseHouseNewQuery
                             join communities in db.ChineseCommunitiesQuery on housenew.CommunityId equals communities.Id
                             where housenew.Id == queryDto.HouseId
                             select new HouseByContactDto
                             {
                                 Id = housenew.Id,
                                 Areas = housenew.Areas,
                                 RentPrice = housenew.RentPrice,
                                 TotalPrice = housenew.TotalPrice,
                                 ResaleHouseCommodityType = (EnumResaleHouseCommodityType)housenew.HouseSource,
                                 CommissionPartnerType = housenew.CommissionPartnerType.GetValueOrDefault(0),
                                 CommissionPartner = housenew.CommissionPartner.GetValueOrDefault(0),
                                 CommissionPartnerPercent = housenew.CommissionPartnerPercent.GetValueOrDefault(0),
                                 BedRoomsCount = housenew.BedRoomsCount.GetValueOrDefault(0),
                                 BathRoomsCount = housenew.BathRoomsCount.GetValueOrDefault(0),
                                 MainPhotoUrl = housenew.MainPhoto,
                                 CommunityName = housenew.CommunityName,
                                 AreaName = communities.AreaName,
                                 CityName = communities.CityName,
                                 IsMls = housenew.ShareToFdb == 2
                             }).First();
            return res;
        }
    }
}