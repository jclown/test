using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Dto;
using Dto.Demo;
using System.Linq;
using Modobay.Cache;
using Model;
using Modobay;

namespace Dal
{
    public partial class MLSDbContext
    {
        protected override void OnModelCreatingExt(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FollowUpInfoDto>().HasKey(x => new { x.FollowUpID });// 示例数据
            modelBuilder.Entity<RelationKey>().HasKey(x => new { x.Type, x.PrimaryKey, x.ForeignKey });
            modelBuilder.Entity<IdDto>().HasKey(x => new { x.Id });

            modelBuilder.Entity<Dto.Demand.DemandDetailDto>().HasKey(x => new { x.CustomerDemandId, x.Type, x.Id });
            modelBuilder.Entity<CustomerDemandCities>().HasKey(x => new { x.CustomerDemandId, x.CityId });
            modelBuilder.Entity<CustomerDemandBedrooms>().HasKey(x => new { x.CustomerDemandId, x.BedroomId });
            modelBuilder.Entity<CustomerDemandAreas>().HasKey(x => new { x.CustomerDemandId, x.AreaId });
            modelBuilder.Entity<CustomerDemandCardinalDirections>().HasKey(x => new { x.CustomerDemandId, x.CardinalDirectionId });
            modelBuilder.Entity<CustomerDemandSubDistricts>().HasKey(x => new { x.CustomerDemandId, x.SubDistrictId });
            modelBuilder.Entity<CustomerDemandPropertyTypes>().HasKey(x => new { x.CustomerDemandId, x.PropertyTypeId });
            modelBuilder.Entity<CustomerDemandHouseFeatures>().HasKey(x => new { x.CustomerDemandId, x.HouseFeatureId });
            modelBuilder.Entity<CustomerDemandFloorTypes>().HasKey(x => new { x.CustomerDemandId, x.FloorTypeId });
            modelBuilder.Entity<CustomerDemandCommunities>().HasKey(x => new { x.CustomerDemandId, x.CommunityId });
        }

        #region 示例数据

        //public new IQueryable<FollowUpInfoDto> SystemRoleQuery => Role.IgnoreQueryFilters().AsNoTracking().Where(x => x.IsDeleted == false && x.IsSystem == true).AsQueryable();

        public DbSet<FollowUpInfoDto> FollowUpInfoDataModelSample { get; set; }

        private IQueryable<FollowUpInfoDto> DataModelSample(int followUpId)
        {
            return FollowUpInfoDataModelSample.FromSqlRaw("select xxx from xxx where xx={0}", followUpId);
        }

        #endregion

        #region 当前机构用户列表（缓存）

        /// <summary>
        /// 当前用户所在机构的用户列表，key就是用户id。注意！！！！已清空了手机号码，可以直接输出给客户端。
        /// </summary>
        public Dictionary<int, UserInfo> CurrentCorpUserInfoList
        {
            get
            {
                var userList = CurrentCorpUserList;
                var userInfoList = new Dictionary<int, UserInfo>();
                userList.ForEach(item =>
                {
                    if (!userInfoList.ContainsKey((int)item.UserId)) userInfoList.Add((int)item.UserId, item);
                });
                return userInfoList;
            }
        }

        /// <summary>
        /// 当前用户所在机构的用户列表，key就是手机号码的数值格式。注意！！！包含手机号码，避免直接输出给客户端（建议仅用于服务端需要手机号码的场景）
        /// </summary>
        public Dictionary<long, UserInfo> CurrentCorpUserPhoneNumberList
        {
            get
            {
                var userList = CurrentCorpUserList;
                var userInfoList = new Dictionary<long, UserInfo>();
                userList.ForEach(item =>
                {
                    if (!string.IsNullOrEmpty(item.PhoneNumber) && !userInfoList.ContainsKey(long.Parse(item.PhoneNumber)))
                    {
                        userInfoList.Add(long.Parse(item.PhoneNumber), item);
                    }
                });
                return userInfoList;
            }
        }

        /// <summary>
        /// 当前用户所在机构的用户列表。注意！！！包含手机号码，避免直接输出给客户端（建议仅用于服务端需要手机号码的场景）
        /// </summary>
        public List<UserInfo> CurrentCorpUserList
        {
            get
            {
                var shopId = Modobay.AppManager.CurrentAppContext?.User?.ShopId;
                if (shopId <= 0) throw new AppException("无权访问。shopId无效");
                return CacheManager.CacheWeek<List<UserInfo>>(GetCurrentCorpUserList, $"GetCurrentCorpUserList:{shopId}", shopId);
            }
        }

        private static Func<int, List<UserInfo>> GetCurrentCorpUserList = (shopId) =>
        {
            if (!(shopId > 0)) return null;
            using (var db = Dal.DbContextExt.GetDbContext<Dal.MLSDbContext>())
            {
                var query = from trader in db.ChineseTraderInfoQuery
                            join m in db.ChineseShopMemberQuery.Where(x => x.IsQuit == false) on trader.UserId equals m.UserId into a
                            from m in a.DefaultIfEmpty()
                            join dept in db.ChineseShopDepartmentQuery on m.DepartmentId equals dept.Id into b
                            from dept in b.DefaultIfEmpty()
                            join shop in db.ChineseShopQuery on shopId equals shop.Id into c
                            from shop in c.DefaultIfEmpty()
                            join user in db.UsersQuery on trader.UserId equals user.Id into d
                            from user in d.DefaultIfEmpty()
                            where trader.ShopId == shopId && m.ShopId == shopId
                            select new UserInfo
                            {
                                UserId = trader.UserId,
                                UserName = trader.TraderName,
                                AvatarUrl = trader.ProfilePhotoPath,
                                DeptName = dept.Name,
                                CompanyName = shop.Name,
                                PhoneNumber = user.PhoneNumber
                            };
                var userList = query.Distinct().ToList();
                return userList;
            }
        };

        public IQueryable<UserInfo> GetUserQuery()
        {
            var db = this;
            var query = from trader in db.ChineseTraderInfoQuery
                        join m in db.ChineseShopMemberQuery.Where(x => x.IsQuit == false) on trader.UserId equals m.UserId into a
                        from m in a.DefaultIfEmpty()
                        join dept in db.ChineseShopDepartmentQuery on m.DepartmentId equals dept.Id into b
                        from dept in b.DefaultIfEmpty()
                        join shop in db.ChineseShopQuery on m.ShopId equals shop.Id into c
                        from shop in c.DefaultIfEmpty()
                        join user in db.UsersQuery on trader.UserId equals user.Id into d
                        from user in d.DefaultIfEmpty()
                        select new UserInfo
                        {
                            UserId = trader.UserId,
                            UserName = trader.TraderName,
                            AvatarUrl = trader.ProfilePhotoPath,
                            DeptName = dept.Name,
                            ShopId = shop.Id,
                            CompanyName = shop.Name,
                            PhoneNumber = user.PhoneNumber
                        };
            return query;
        }

        #endregion

        public DbSet<IdDto> IdDto { get; set; }

        #region 需求

        public DbSet<RelationKey> RelationKey { get; set; }

        public DbSet<IDValueDto> IDValueDto { get; set; }

        public DbSet<Dto.Demand.DemandDetailDto> DemandDetail { get; set; }

        public IQueryable<Dto.Demand.DemandDetailDto> GetDemandDetailQuery(List<int> primaryKeyList)
        {
            var sql = @"select 'CityArray' Type,CustomerDemandCities.CustomerDemandId,CityId Id,ChineseAreas.Name 
	                        from CustomerDemandCities 
	                        left join ChineseAreas on ChineseAreas.Id=CustomerDemandCities.CityId
	                        where CustomerDemandCities.CustomerDemandId in ({0})
                        union 
                        select 'AreaArray' Type,CustomerDemandAreas.CustomerDemandId,AreaId,ChineseAreas.Name 
	                        from CustomerDemandAreas 
	                        left join ChineseAreas on ChineseAreas.Id=CustomerDemandAreas.AreaId
	                        where CustomerDemandAreas.CustomerDemandId in ({0})
                        union 
                        select 'SubArray' Type,CustomerDemandSubDistricts.CustomerDemandId,SubDistrictId,ChineseAreas.Name 
	                        from CustomerDemandSubDistricts 
	                        left join ChineseAreas on ChineseAreas.Id=CustomerDemandSubDistricts.SubDistrictId
	                        where CustomerDemandSubDistricts.CustomerDemandId in ({0})
                        union 
                        select 'CommunityNameArray' Type,CustomerDemandCommunities.CustomerDemandId,SubDistrictId,ChineseCommunities.Name 
	                        from CustomerDemandCommunities 
	                        left join ChineseCommunities on ChineseCommunities.Id=CustomerDemandCommunities.CommunityId
	                        where CustomerDemandCommunities.CustomerDemandId in ({0})
                        union 
                        select 'DemandTagArray' Type,CustomerDemandHouseFeatures.CustomerDemandId,CustomerDemandHouseFeatures.HouseFeatureId,'' Name 
	                        from CustomerDemandHouseFeatures 
	                        where CustomerDemandHouseFeatures.CustomerDemandId in ({0})
                        union 
						select 'MatchHouseCount' Type,CustomerDemands.Id,
							(SELECT COUNT(0) FROM F_MatchHouseByHouseGroup(CustomerDemands.ShopId) WHERE CustomerDemandId =CustomerDemands.Id AND (HouseIsMLS=1 OR HouseShopId=CustomerDemands.ShopId)) AS MatchHouseCount,'' Name 
							from CustomerDemands 
							where CustomerDemands.Id in ({0})                        
                        union 
                        select 'BedroomsArray' Type,CustomerDemandBedrooms.CustomerDemandId,CustomerDemandBedrooms.BedroomId,'' Name 
	                        from CustomerDemandBedrooms 
	                        where CustomerDemandBedrooms.CustomerDemandId in ({0})";
            return DemandDetail.FromSqlRaw(sql.FormatWith(string.Join(",", primaryKeyList)));
            //return DemandDetail.FromSqlRaw(sql, string.Join(",", primaryKeyList));

            /*
             * select 'MatchHouseCount' Type,CustomerDemandMatches.CustomerDemandId,count(*),'' Name 
	                        from CustomerDemandMatches 
	                        where CustomerDemandMatches.CustomerDemandId in ({0})
	                        group by CustomerDemandMatches.CustomerDemandId
             */
        }

        public DbSet<CustomerDemandCities> CustomerDemandCities { get; set; }
        public IQueryable<CustomerDemandCities> CustomerDemandCitiesQuery => CustomerDemandCities.AsNoTracking().AsQueryable();
        public DbSet<CustomerDemandAreas> CustomerDemandAreas { get; set; }
        public IQueryable<CustomerDemandAreas> CustomerDemandAreasQuery => CustomerDemandAreas.AsNoTracking().AsQueryable();
        public DbSet<CustomerDemandBedrooms> CustomerDemandBedrooms { get; set; }
        public IQueryable<CustomerDemandBedrooms> CustomerDemandBedroomsQuery => CustomerDemandBedrooms.AsNoTracking().AsQueryable();
        public DbSet<CustomerDemandCardinalDirections> CustomerDemandCardinalDirections { get; set; }
        public IQueryable<CustomerDemandCardinalDirections> CustomerDemandCardinalDirectionsQuery => CustomerDemandCardinalDirections.AsNoTracking().AsQueryable();
        public DbSet<CustomerDemandSubDistricts> CustomerDemandSubDistricts { get; set; }
        public IQueryable<CustomerDemandSubDistricts> CustomerDemandSubDistrictsQuery => CustomerDemandSubDistricts.AsNoTracking().AsQueryable();
        public DbSet<CustomerDemandPropertyTypes> CustomerDemandPropertyTypes { get; set; }
        public IQueryable<CustomerDemandPropertyTypes> CustomerDemandPropertyTypesQuery => CustomerDemandPropertyTypes.AsNoTracking().AsQueryable();
        public DbSet<CustomerDemandHouseFeatures> CustomerDemandHouseFeatures { get; set; }
        public IQueryable<CustomerDemandHouseFeatures> CustomerDemandHouseFeaturesQuery => CustomerDemandHouseFeatures.AsNoTracking().AsQueryable();
        public DbSet<CustomerDemandFloorTypes> CustomerDemandFloorTypes { get; set; }
        public IQueryable<CustomerDemandFloorTypes> CustomerDemandFloorTypesQuery => CustomerDemandFloorTypes.AsNoTracking().AsQueryable();
        public DbSet<CustomerDemandCommunities> CustomerDemandCommunities { get; set; }
        public IQueryable<CustomerDemandCommunities> CustomerDemandCommunitiesQuery => CustomerDemandCommunities.AsNoTracking().AsQueryable();

        #endregion
    }
}
