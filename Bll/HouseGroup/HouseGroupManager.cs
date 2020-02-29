using Dal;
using Dto.HouseGroup;
using Modobay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Model;

namespace Bll.HouseGroup
{
    [System.ComponentModel.Description("房源群")]
    public class HouseGroupManager : IHouseGroupManager
    {
        private readonly MLSDbContext db;
        private readonly IAppContext app;

        public HouseGroupManager(Dal.MLSDbContext dbContext, Modobay.IAppContext appContext)
        {
            this.db = dbContext;
            this.app = appContext;
        }

        /// <summary>
        /// 区域负责人
        /// </summary>
        /// <returns></returns>

        public List<PrincipalDetailDto> Principal()
        {
            var res = new List<PrincipalDetailDto>();
            int shopId = this.app.User.ShopId;
            if (shopId <= 0) return res;

            List<int> houseGroupids = this.db.HouseGroupByCompanyQuery.Where(c => c.CompanyId == shopId).Select(c => c.HouseGroupId).Distinct().OrderBy(c => c).ToList();

            string principalArrayStr = string.Empty;
            if (!houseGroupids.Any())
            {
                //取平台管理员
                Sys_UserInfo sys_UserInfo = this.db.Sys_UserInfoQuery.FirstOrDefault(c => c.Type == 0);
                if (sys_UserInfo != null) principalArrayStr = sys_UserInfo.Principal;
            }
            else
            {
                //取区域代理
                Sys_UserInfo sys_UserInfo = this.db.Sys_UserInfoQuery.FirstOrDefault(c => c.Principal == houseGroupids.GHSerializeObject());
                if (sys_UserInfo == null) sys_UserInfo = this.db.Sys_UserInfoQuery.FirstOrDefault(c => c.Type == 0); //取平台管理员
                if (sys_UserInfo != null) principalArrayStr = sys_UserInfo.Principal;
            }
            if (string.IsNullOrEmpty(principalArrayStr)) return res;

            List<int> principalArray = principalArrayStr.GHDeserializeObject<List<int>>();

            res = (from u in this.db.UsersQuery
                   join t in this.db.ChineseTraderInfoQuery on u.Id equals t.UserId
                   where principalArray.Contains(u.Id)
                   select new PrincipalDetailDto
                   {
                       TraderName = t.TraderName,
                       PhoneNumber = u.PhoneNumber,
                       IMUserName = u.UserName
                   }).ToList();

            return res;
        }
    }
}