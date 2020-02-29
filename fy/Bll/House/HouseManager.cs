using Dal;
using Dto.House;
using Modobay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Model;

namespace Bll.House
{
    [System.ComponentModel.Description("二手房")]
    public class HouseManager : IHouseManager
    {
        private readonly MLSDbContext db;
        private readonly IAppContext app;

        public HouseManager(Dal.MLSDbContext dbContext, Modobay.IAppContext appContext)
        {
            this.db = dbContext;
            this.app = appContext;
        }

        /// <summary>
        /// 获取录房、合作房源列表
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public PagedList<HouseListDto> Search(HouseQueryDto queryDto)
        {
            //var query = db.ChineseHouseNewQuery.Where(x => x.ShopId == app.User.ShopId && x.CreatedBy == queryDto.UserId && x.HouseSource > 0);
            var query = db.ChineseHouseNewQuery.Where(x => x.CreatedBy == queryDto.UserId && x.HouseSource > 0);

            if (queryDto.StatItemType == Dto.Report.StatItemTypeEnum.InputHouse)
            {
                query = query.Where(x => x.CreatedDate >= queryDto.DateS && x.CreatedDate <= queryDto.DateE);
            }
            else
            {
                query = query.Where(x => x.CreatedDate >= queryDto.DateS && x.CreatedDate <= queryDto.DateE);
            }

            var list = query.GetPagedList(queryDto.PageIndex, queryDto.PageSize);
            var data = new PagedList<HouseListDto>(list.CurrentPageIndex, list.PageSize, list.TotalItemCount);
            var userInfoList = db.CurrentCorpUserInfoList;
            list.ForEach(item =>
            {
                var dto = ToDto(item, null);
                data.Add(dto);
            });
            return data;
        }

        private HouseListDto ToDto(ChineseHouseNew entity, dynamic extInfo = null)
        {
            var dto = Lib.Mapper<ChineseHouseNew, HouseListDto>.CloneDto(entity);
            dto.MainPhotoUrl = entity.MainPhoto ?? string.Empty;
            return dto;
        }
    }
}