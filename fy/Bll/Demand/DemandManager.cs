using Dal;
using Dto.House;
using Modobay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Dto.Demand;
using Model;
using Dto;

namespace Bll.Demand
{
    [System.ComponentModel.Description("需求（AI找房）")]
    public class DemandManager : IDemandManager
    {
        private readonly MLSDbContext db;
        private readonly IAppContext app;

        public DemandManager(Dal.MLSDbContext dbContext, Modobay.IAppContext appContext)
        {
            this.db = dbContext;
            this.app = appContext;
        }

        /// <summary>
        /// 获取需求列表（AI找房明细）
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public PagedList<DemandListDto> Search(DemandQueryDto queryDto)
        {
            var query = from demand in db.CustomerDemandsQuery
                            //where demand.ShopId == app.User.ShopId && demand.CreatedBy == queryDto.UserId
                        where demand.CreatedBy == queryDto.UserId
                        && (demand.CreatedDate >= queryDto.DateS && demand.CreatedDate <= queryDto.DateE)
                        orderby demand.CreatedDate descending
                        select new 
                        {
                            demand
                        };
                        
            var list = query.GetPagedList(queryDto.PageIndex, queryDto.PageSize);
            var idList = list.Select(x => x.demand.Id).ToList();
            var detail = (idList.Count > 0) ? db.GetDemandDetailQuery(idList).ToList() : new List<DemandDetailDto>();
            var data = new PagedList<DemandListDto>(list.CurrentPageIndex, list.PageSize, list.TotalItemCount);
            list.ForEach(item => data.Add(ToDto(item.demand, detail)));//, item.extInfo
            return data;
        }

        private DemandListDto ToDto(CustomerDemands entity,List<DemandDetailDto> detail, dynamic extInfo = null)
        {
            var dto = Lib.Mapper<CustomerDemands, DemandListDto>.CloneDto(entity);
            //dto.CustomerName = extInfo.CustomerName;
            dto.CustomerName = entity.Title;
            dto.CityArray = detail.Where(x => x.CustomerDemandId == dto.Id && x.Type == "CityArray").Select(x => x.Name).ToList();
            dto.AreaArray = detail.Where(x => x.CustomerDemandId == dto.Id && x.Type == "AreaArray").Select(x => x.Name).ToList();
            dto.BedroomsArray = detail.Where(x => x.CustomerDemandId == dto.Id && x.Type == "BedroomsArray").Select(x => x.Id.GetDescription<EnumWantBedRoomsType>()).ToList();
            dto.CommunityNameArray = detail.Where(x => x.CustomerDemandId == dto.Id && x.Type == "CommunityNameArray").Select(x => x.Name).ToList();
            dto.DemandTagArray = detail.Where(x => x.CustomerDemandId == dto.Id && x.Type == "DemandTagArray").Select(x => x.Id.GetDescription<EnumDemandTagType>()).ToList();
            dto.SubArray = detail.Where(x => x.CustomerDemandId == dto.Id && x.Type == "SubArray").Select(x => x.Name).ToList();
            dto.MatchHouseCount = detail.FirstOrDefault(x => x.CustomerDemandId == dto.Id && x.Type == "MatchHouseCount")?.Id ?? 0;
            return dto;
        }

        public List<DemandListDto> GetExpiryDemandList()
        {
            return null;
            //var query = from demand in db.CustomerDemandsQuery
            //                //where demand.ShopId == app.User.ShopId && demand.CreatedBy == queryDto.UserId
            //            where demand.CreatedBy == queryDto.UserId
            //            && (demand.CreatedDate >= queryDto.DateS && demand.CreatedDate <= queryDto.DateE)
            //            orderby demand.CreatedDate descending
            //            select new
            //            {
            //                demand
            //            };

            //var list = query.GetPagedList(queryDto.PageIndex, queryDto.PageSize);
            //var idList = list.Select(x => x.demand.Id).ToList();
            //var detail = (idList.Count > 0) ? db.GetDemandDetailQuery(idList).ToList() : new List<DemandDetailDto>();
            //var data = new PagedList<DemandListDto>(list.CurrentPageIndex, list.PageSize, list.TotalItemCount);
            //list.ForEach(item => data.Add(ToDto(item.demand, detail)));//, item.extInfo
            //return data;
        }
    }
}
