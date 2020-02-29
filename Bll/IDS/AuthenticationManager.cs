using Modobay;
using Model;
using Dto.IDS;
using System;
using System.Linq;
using Modobay;
using Microsoft.AspNetCore.Mvc;

namespace Bll.IDS
{
    [Modobay.Api.NonGateway]
    [System.ComponentModel.Description("身份认证")]
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly Dal.MLSDbContext db;
        private const int TokenCacheMinutes = 5;

        public AuthenticationManager(Dal.MLSDbContext dbContext)
        {
            this.db = dbContext;
        }

        /// <summary>
        /// 检查身份，token。
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [Modobay.Api.NonLog, Modobay.Api.NonToken]
        public UserDto CheckToken(string token)
        {
            return GetUserInfoByToken(token, "CheckToken");
        }

        private UserDto GetUserInfoByToken(string token, string scene)
        {
            if (string.IsNullOrEmpty(token)) throw new AppException("token不能为空");

            var identifier = int.Parse(Modobay.OwinHelper.GetIdentity(token));

            // 获取用户信息
            var userInfoQuery = from u in db.UsersQuery
                                join m in db.ChineseShopMemberQuery.Where(x => x.IsQuit == false) on u.Id equals m.UserId into a
                                from m in a.DefaultIfEmpty()
                                //join t in db.ChineseTraderInfoQuery on new { m.ShopId, UserId = u.Id } equals new { t.ShopId, t.UserId } into b
                                join t in db.ChineseTraderInfoQuery on u.Id equals t.UserId into b
                                from t in b.DefaultIfEmpty()
                                where u.Id == identifier
                                select new
                                {
                                    u.Id,
                                    t.TraderName,
                                    DepartmentId = (m != null ? m.DepartmentId : 0),
                                    ShopId = (m != null ? m.ShopId : 0),
                                    t.AuditStatus
                                };

            // 检查审核状态
            var userInfo = userInfoQuery.FirstOrDefault();
            if (userInfo == null) throw new TokenException(TokenExceptionType.Invalid);
            if (userInfo.AuditStatus == 5) throw new AppException("用户被冻结。");
                        
            var userDto = new UserDto()
            {
                UserID = userInfo.Id,
                UserName = userInfo.TraderName,
                //PhoneNumber = string.Concat(phoneNumber.Left(4),"****",phoneNumber.Right(4)),
                PhoneNumber = "",
                ShopId = userInfo.ShopId,
                DepartmentId = userInfo.DepartmentId
            };

            return userDto;            
        }
    }
}
