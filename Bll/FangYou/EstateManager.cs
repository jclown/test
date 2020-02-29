using Dal;
using Dto.House;
using Modobay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Model;

namespace Bll.FangYou
{
    [System.ComponentModel.Description("二手房")]
    [Modobay.Api.NonToken]
    public class EstateManager : IEstateManager
    {
        private readonly FyDbContext fydb;
        private readonly MLSDbContext mlsdb;
        private readonly IAppContext app;

        public EstateManager(FyDbContext dbContext, Modobay.IAppContext appContext, MLSDbContext mLSDbContext)
        {
            this.fydb = dbContext;
            this.app = appContext;
            this.mlsdb = mLSDbContext;
        }
        /// <summary>
        /// 导入小区
        /// </summary>
        /// <returns></returns>
        public string ImportCommunities()
        {
            try
            {
                var Estatelist = fydb.EstateQuery.ToList();
                List<string> estatesnamelist = new List<string>();
                List<ChineseCommunities> Communitieslist = new List<ChineseCommunities>();//过滤后的小区集合
                foreach (var item in Estatelist)
                {

                    Communitieslist.Add(new ChineseCommunities()
                    {
                        CityId= 11223,
                        CityName= "广州",
                        AreaId= 11235,
                        AreaName= "花都",
                        SubDistrictId= 28512,
                        SubDistrictName="其他",
                        Name=item.EstateName,
                        Address=item.Address,
                        AveragePrice = (double)item.Price,
                        CreatedDate=Convert.ToDateTime( DateTime.Now.ToString("yyyy-MM-dd HH:mm")),
                        WaterElectricity=item.EstateID//保存房友小区id用于房源导入判断
                    });
                }
                mlsdb.ChineseCommunities.AddRange(Communitieslist);
                mlsdb.SaveChanges();
                return ("success");
            }
            catch (Exception ex)
            {

                return(ex.ToString());

            }
        }
        /// <summary>
        /// 导入房源
        /// </summary>
        /// <returns></returns>
        public string ImportHouseNew()
        {
           

            try
            {
                var CommunitiesList = mlsdb.ChineseCommunities.Where(x => x.SubDistrictId == 28512).ToList();
                var q = (from h in CommunitiesList
                         join p in fydb.Property on h.WaterElectricity equals p.EstateID
                        select new {
                            h.Id,
                            h.Name,
                            p.RegPerson,
                            p.Remark,
                            p.Address,
                            p.Trade
                        }).ToList();
                List<ChineseHouseNew> HouseNew = new List<ChineseHouseNew>();
                foreach (var item in q)
                {
                    HouseNew.Add(new ChineseHouseNew()
                    {
                        CityId = 11223,
                        HouseSource=item.Trade=="出售"?2:1,
                        CommunityId= item.Id,
                        CommunityName=item.Name,
                        Name=item.RegPerson,
                        Remarks=item.Remark,
                        Address = item.Address,
                        FloorType=1,
                        IsOpenDoorAddress=false,
                        IsTop=0,
                        CreatedBy= 7502,
                        MaintainUserId = 7502,
                        UpdatedBy = 7502,
                        CreatedDate= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm")),
                        UpdatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm")),
                        ShopId= 2407,
                        DepartmentId= 1738,
                        HouseCreaterFrom=0

                    });
                }
                mlsdb.ChineseHouseNew.AddRange(HouseNew);
                mlsdb.SaveChanges();
                return ("success");
            }
            catch (Exception ex)
            {

                return (ex.ToString());
            }

            
            return  "";

        }


    }
}
