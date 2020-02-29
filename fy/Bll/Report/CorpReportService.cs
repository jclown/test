using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dal;
using Dto;
using Dto.Report;
using Dto.VisitorRecord;
using Modobay;

namespace Bll.Report
{
    [System.ComponentModel.Description("公司报表服务")]
    public class CorpReportService : ICorpReportService
    {
        private readonly MLSDbContext db;
        private readonly IAppContext app;

        public CorpReportService(Dal.MLSDbContext dbContext, Modobay.IAppContext appContext)
        {
            this.db = dbContext;
            this.app = appContext;
        }

        #region 数据源

        private IQueryable<IDValueDto> GetHouseQuery(bool isGroup, DateTime startDate, DateTime endDate)
        {
            // 录房
            var query = from house in db.ChineseHouseNewQuery
                        where house.IsDel == 0 && house.ShopId == app.User.ShopId
                        && (house.CreatedDate >= startDate && house.CreatedDate <= endDate)
                        && house.HouseSource > 0
                        select new IDValueDto
                        {
                            ID = house.CreatedBy,
                            Value = 1
                        };

            if (isGroup)
            {
                query = query.GroupBy(g => g.ID).Select(x => new IDValueDto { ID = x.Key, Value = x.Count() }).AsQueryable();
            }
            else
            {
                query = query.GroupBy(g => 0).Select(x => new IDValueDto { ID = (int)StatItemTypeEnum.InputHouse, Value = x.Count() }).AsQueryable();
            }
            return query;
        }

        private IQueryable<IDValueDto> GetMLSHouseQuery(bool isGroup, DateTime startDate, DateTime endDate)
        {
            // 合作房源
            var query = from house in db.ChineseHouseNewQuery
                        join attr in db.HouseCommonAttrQuery on house.Id equals attr.HouseId
                        where house.IsDel == 0 && house.ShopId == app.User.ShopId
                        && (attr.FirstMlsTime >= startDate && attr.FirstMlsTime <= endDate)
                        select new IDValueDto
                        {
                            ID = house.CreatedBy,
                            Value = 1
                        };

            if (isGroup)
            {
                query = query.GroupBy(g => g.ID).Select(x => new IDValueDto { ID = x.Key, Value = x.Count() }).AsQueryable();
            }
            else
            {
                query = query.GroupBy(g => 0).Select(x => new IDValueDto { ID = (int)StatItemTypeEnum.MlsHouse, Value = x.Count() }).AsQueryable();
            }
            return query;
        }

        private IQueryable<IDValueDto> GetDemandQuery(bool isGroup, DateTime startDate, DateTime endDate)
        {
            // 需求（AI找房）
            var query = from demand in db.CustomerDemandsQuery
                        where demand.ShopId == app.User.ShopId
                        && (demand.CreatedDate >= startDate && demand.CreatedDate <= endDate)
                        select new IDValueDto
                        {
                            ID = demand.CreatedBy,
                            Value = 1
                        };

            if (isGroup)
            {
                query = query.GroupBy(g => g.ID).Select(x => new IDValueDto { ID = x.Key, Value = x.Count() }).AsQueryable();
            }
            else
            {
                query = query.GroupBy(g => 0).Select(x => new IDValueDto { ID = (int)StatItemTypeEnum.InputDemnd, Value = x.Count() }).AsQueryable();
            }
            return query;
        }

        private IQueryable<IDValueDto> GetContactTelephoneQuery(bool isGroup, DateTime startDate, DateTime endDate)
        {
            // 联络-去电
            var query = from contact in db.MlsContactRecordLogQuery
                        where contact.ShopId == app.User.ShopId
                        && (contact.CreateTime >= startDate && contact.CreateTime <= endDate)
                        //&& contact.HouseSourceType == 2
                        select new IDValueDto
                        {
                            ID = contact.UserId,
                            Value = 1
                        };

            if (isGroup)
            {
                query = query.GroupBy(g => g.ID).Select(x => new IDValueDto { ID = x.Key, Value = x.Count() }).AsQueryable();
            }
            else
            {
                query = query.GroupBy(g => 0).Select(x => new IDValueDto { ID = (int)StatItemTypeEnum.Telephone, Value = x.Count() }).AsQueryable();
            }
            return query;
        }

        private IQueryable<IDValueDto> GetContactCallMeQuery(bool isGroup, DateTime startDate, DateTime endDate)
        {
            // 联络-来电（根据本机构人员的手机号码识别）            
            var phoneNumberList = db.CurrentCorpUserList.Select(x => x.PhoneNumber).ToList();
            var query = from contact in db.MlsContactRecordLogQuery
                        where phoneNumberList.Contains(contact.Contacter)
                        && (contact.CreateTime >= startDate && contact.CreateTime <= endDate)
                        //&& contact.HouseSourceType == 2
                        select new IDValueDto
                        {
                            Code = contact.Contacter,
                            Value = 1
                        };

            if (isGroup)
            {
                query = query.GroupBy(g => g.Code).Select(x => new IDValueDto { Code = x.Key, Value = x.Count() }).AsQueryable();
            }
            else
            {
                query = query.GroupBy(g => 0).Select(x => new IDValueDto { ID = (int)StatItemTypeEnum.CallMe, Value = x.Count() }).AsQueryable();
            }
            return query;
        }

        private IQueryable<IDValueDto> GetBrowerQuery(bool isGroup, DateTime startDate, DateTime endDate)
        {
            // 浏览
            //公司内部经纪人在平台浏览已合作发布的房源的总次数
            var browerSourcArray = new int[] {
                (int)BehaviorSourceEnum.房得宝APP,
                (int)BehaviorSourceEnum.房得宝PC端,
                (int)BehaviorSourceEnum.APPMLS平台,
                (int)BehaviorSourceEnum.PCMLS平台,
            };

            var query = from brower in db.VisitorBehaviorRecordQuery
                        join trader in db.ChineseTraderInfoQuery on brower.VisitorTag equals trader.UserId.ToString()
                        where brower.VisitorType == 1//经纪人
                        && browerSourcArray.Contains(brower.BehaviorSource)
                        && brower.BehaviorType == (int)BehaviorTypeEnum.浏览
                        && trader.ShopId == app.User.ShopId
                        && (brower.CreateTime >= startDate && brower.CreateTime <= endDate)
                        select new IDValueDto
                        {
                            ID = trader.UserId,
                            Value = 1
                        };

            if (isGroup)
            {
                query = query.GroupBy(g => g.ID).Select(x => new IDValueDto { ID = x.Key, Value = x.Count() }).AsQueryable();
            }
            else
            {
                query = query.GroupBy(g => 0).Select(x => new IDValueDto { ID = (int)StatItemTypeEnum.Brower, Value = x.Count() }).AsQueryable();
            }
            return query;
        }

        private IQueryable<IDValueDto> GetShareQuery(bool isGroup, DateTime startDate, DateTime endDate)
        {
            //分享
            //通过微信好友、朋友圈、房源海报分享房源(新房、二手房、出租房、租售)到微信平台的总次数。
            var behaviorArray = new int[] {
                (int)BehaviorTypeEnum.分享到微信好友,
                (int)BehaviorTypeEnum.分享到朋友圈,
                //(int)BehaviorTypeEnum.分享到QQ,
                (int)BehaviorTypeEnum.分享房源海报到微信,
                (int)BehaviorTypeEnum.分享房源海报到朋友圈,
                //(int)BehaviorTypeEnum.分享房源海报到其它平台,
            };
            var browerSourceArray = new int[] {
                 (int)BehaviorSourceEnum.房得宝APP,
                (int)BehaviorSourceEnum.APPMLS平台,
                (int)BehaviorSourceEnum.APP工作台,
            };

            var query = from share in db.VisitorBehaviorRecordQuery
                        join trader in db.ChineseTraderInfoQuery on share.VisitorTag equals trader.UserId.ToString()
                        where share.VisitorType == 1//经纪人
                        && browerSourceArray.Contains(share.BehaviorSource)
                         && behaviorArray.Contains(share.BehaviorType)
                        && trader.ShopId == app.User.ShopId
                        && (share.CreateTime >= startDate && share.CreateTime <= endDate)
                        select new IDValueDto
                        {
                            ID = trader.UserId,
                            Value = 1
                        };

            if (isGroup)
            {
                query = query.GroupBy(g => g.ID).Select(x => new IDValueDto { ID = x.Key, Value = x.Count() }).AsQueryable();
            }
            else
            {
                query = query.GroupBy(g => 0).Select(x => new IDValueDto { ID = (int)StatItemTypeEnum.Share, Value = x.Count() }).AsQueryable();
            }
            return query;
        }

        private IQueryable<IDValueDto> GetVisitUserQuery(bool isGroup, DateTime startDate, DateTime endDate)
        {
            //获客
            //分享房源(新房、二手房、出租房、租售)到微信平台获取客户的总人数。（如同一个微信客户在某天内分别看了2位经纪人的房源，统计为2个客户，2位经纪人的“获客明细”里都可以见到该客户）。

            //经纪人直接分享
            var query = from visitUser in db.VisitorBehaviorRecordQuery
                        join share in db.ShareRecordQuery on visitUser.ShareUUID equals share.UUID
                        join trader in db.ChineseTraderInfoQuery on share.SharerTag equals trader.UserId.ToString()
                        where visitUser.VisitorType == 2 && visitUser.BehaviorType == (int)BehaviorTypeEnum.浏览
                        && trader.ShopId == app.User.ShopId
                        && (visitUser.CreateTime >= startDate && visitUser.CreateTime <= endDate)
                        group visitUser by new { visitUser.VisitorTag, trader.UserId } into g
                        select new IDValueDto
                        {
                            ID = g.Key.UserId,
                            Value = g.Count()
                        };

            //源分享人为经纪人(间接分享)
            query.Concat(from visitUser in db.VisitorBehaviorRecordQuery
                         join share in db.ShareRecordQuery on visitUser.ShareUUID equals share.UUID
                         join trader in db.ChineseTraderInfoQuery on share.OringnalShareUserId equals trader.UserId
                         where visitUser.VisitorType == 2 && visitUser.BehaviorType == (int)BehaviorTypeEnum.浏览
                          && trader.ShopId == app.User.ShopId
                         && (visitUser.CreateTime >= startDate && visitUser.CreateTime <= endDate)
                         group visitUser by new { visitUser.VisitorTag, trader.UserId } into g
                         select new IDValueDto
                         {
                             ID = g.Key.UserId,
                             Value = g.Count()
                         });


            if (isGroup)
            {
                query = query.GroupBy(g => g.ID).Select(x => new IDValueDto { ID = x.Key, Value = x.Count() }).AsQueryable();
            }
            else
            {
                query = query.GroupBy(g => 0).Select(x => new IDValueDto { ID = (int)StatItemTypeEnum.VisitUser, Value = x.Count() }).AsQueryable();
            }
            return query;
        }

        private IQueryable<IDValueDto> GetConcernedQuery(bool isGroup, DateTime startDate, DateTime endDate)
        {
            var query = from concerned in db.VisitorBehaviorRecordQuery
                        join house in db.ChineseHouseNewQuery on concerned.HouseId equals house.Id
                        join traderBycreate in db.ChineseTraderInfoQuery on house.CreatedBy equals traderBycreate.Id
                        join traderByMain in db.ChineseTraderInfoQuery on house.MaintainUserId equals traderByMain.Id into _traderByMain
                        from traderByMain in _traderByMain.DefaultIfEmpty()
                        where concerned.HouseType == 1//二手房
                          && (concerned.CreateTime >= startDate && concerned.CreateTime <= endDate)
                        && house.ShopId == app.User.ShopId
                        select new IDValueDto
                        {
                            ID = traderByMain == null ? traderBycreate.Id : traderByMain.Id,
                            Value = 1
                        };

            query.Concat(from concerned in db.VisitorBehaviorRecordQuery
                         join house in db.ChineseNewHouseQuery on concerned.HouseId equals house.Id
                         join traderBycreate in db.ChineseTraderInfoQuery on house.CreaterId equals traderBycreate.Id
                         where concerned.HouseType == 2//新房
                         && house.ShopId == app.User.ShopId
                          && (concerned.CreateTime >= startDate && concerned.CreateTime <= endDate)
                         select new IDValueDto
                         {
                             ID = traderBycreate.Id,
                             Value = 1
                         });

            if (isGroup)
            {
                query = query.GroupBy(g => g.ID).Select(x => new IDValueDto { ID = x.Key, Value = x.Count() }).AsQueryable();
            }
            else
            {
                query = query.GroupBy(g => 0).Select(x => new IDValueDto { ID = (int)StatItemTypeEnum.Concerned, Value = x.Count() }).AsQueryable();
            }
            return query;
        }

        #endregion

        /// <summary>
        /// 获取统计报表
        /// </summary>
        /// <param name="dateRange">日期范围</param>
        /// <param name="startDate">开始日期，仅当dateRange为0时才生效（即其他）</param>
        /// <param name="endDate">结束日期，仅当dateRange为0时才生效（即其他）</param>
        /// <returns></returns>
        public List<StatItemDto> GetWorkReport(DateRangeEnum dateRange, string startDate, string endDate)
        {
            if (!QueryDto.CheckDateRange(dateRange, startDate, endDate, out var dateS, out var dateE))
                return app.ThrowException<List<StatItemDto>>("日期范围无效", AppExceptionType.ParameterError);

            var query = GetHouseQuery(false, dateS, dateE);
            query = query.Concat(GetMLSHouseQuery(false, dateS, dateE));
            query = query.Concat(GetDemandQuery(false, dateS, dateE));
            query = query.Concat(GetBrowerQuery(false, dateS, dateE));
            query = query.Concat(GetContactTelephoneQuery(false, dateS, dateE));
            query = query.Concat(GetContactCallMeQuery(false, dateS, dateE));
            query = query.Concat(GetShareQuery(false, dateS, dateE));
            query = query.Concat(GetVisitUserQuery(false, dateS, dateE));
            query = query.Concat(GetConcernedQuery(false, dateS, dateE));
            // todo djl 补充其他指标

            var list = query.Select(x => new StatItemDto { Type = (StatItemTypeEnum)x.ID, Value = x.Value }).ToList();

            // 联络指标
            var telephoneValue = list.FirstOrDefault(x => x.Type == StatItemTypeEnum.Telephone)?.Value ?? 0;
            var callMeValue = list.FirstOrDefault(x => x.Type == StatItemTypeEnum.CallMe)?.Value ?? 0;
            list.Insert(4, new StatItemDto() { Type = StatItemTypeEnum.Contact, Value = (telephoneValue + callMeValue) });

            return list;
        }

        /// <summary>
        /// 获取排行表。我的排行从扩展属性dataExt获取
        /// </summary>
        /// <param name="statItemType">统计项</param>
        /// <param name="dateRange">日期范围</param>
        /// <param name="startDate">开始日期，仅当dateRange为0时才生效（即其他）</param>
        /// <param name="endDate">结束日期，仅当dateRange为0时才生效（即其他）</param>
        /// <returns></returns>
        public ExtList<RankItemDto> GetWorkRank(StatItemTypeEnum statItemType, DateRangeEnum dateRange, string startDate, string endDate)
        {
            if (!QueryDto.CheckDateRange(dateRange, startDate, endDate, out var dateS, out var dateE))
                return app.ThrowException<ExtList<RankItemDto>>("日期范围无效", AppExceptionType.ParameterError);

            var query = statItemType switch
            {
                StatItemTypeEnum.InputHouse => GetHouseQuery(true, dateS, dateE),
                StatItemTypeEnum.MlsHouse => GetMLSHouseQuery(true, dateS, dateE),
                StatItemTypeEnum.InputDemnd => GetDemandQuery(true, dateS, dateE),
                StatItemTypeEnum.Telephone => GetContactTelephoneQuery(true, dateS, dateE),
                StatItemTypeEnum.CallMe => GetContactCallMeQuery(true, dateS, dateE),
                StatItemTypeEnum.Contact => GetContactTelephoneQuery(true, dateS, dateE),// 联络默认就是去电
                StatItemTypeEnum.Brower => GetBrowerQuery(true, dateS, dateE),
                StatItemTypeEnum.Share => GetShareQuery(true, dateS, dateE),
                StatItemTypeEnum.Concerned => GetConcernedQuery(true, dateS, dateE),
                StatItemTypeEnum.VisitUser => GetVisitUserQuery(true, dateS, dateE),
            };
            if (query == null) return app.ThrowException<ExtList<RankItemDto>>("功能暂未实现");
            Lib.StopwatchLog.RecordElapsedMilliseconds("query");

            var list = new ExtList<RankItemDto>();
            Dictionary<long, UserInfo> userPhoneNumberList = db.CurrentCorpUserPhoneNumberList;

            if (statItemType == StatItemTypeEnum.CallMe)
            {
                list.AddRange(query.OrderByDescending(o => o.Value).Select(x => new RankItemDto { Value = x.Value, User = new UserInfo { UserId = long.Parse(x.Code) } }));
            }
            else
            {
                list.AddRange(query.OrderByDescending(o => o.Value).Select(x => new RankItemDto { Value = x.Value, User = new UserInfo { UserId = (int)x.ID } }));
            }
            Lib.StopwatchLog.RecordElapsedMilliseconds("list");

            var userInfoList = db.CurrentCorpUserInfoList;
            var index = 1;
            foreach (var item in list)
            {
                item.RankIndex = index++;

                if (statItemType == StatItemTypeEnum.CallMe)
                {
                    item.User = userPhoneNumberList[item.User.UserId];
                }
                else
                {
                    item.User = userInfoList.GetValueOrDefault((int)item.User.UserId, item.User);
                }
            }
            var rankUserIdList = list.Select(x => x.User.UserId).ToList();
            var nonRankUserList = userInfoList.Where(x => !rankUserIdList.Contains(x.Key)).ToList();
            foreach (var user in nonRankUserList)
            {
                list.Add(new RankItemDto() { RankIndex = index++, User = user.Value, Value = 0 });
            }

            Lib.StopwatchLog.RecordElapsedMilliseconds("userinfo");

            // 我的排行
            list.DataExt = list.FirstOrDefault(x => x.User.UserId == app.User.UserID) ?? new RankItemDto()
            {
                User = userInfoList.GetValueOrDefault(app.User.UserID, new UserInfo()
                {
                    UserId = app.User.UserID,
                    UserName = app.User.UserName,
                    AvatarUrl = app.User.AvatarUrl
                })
            };

            Lib.StopwatchLog.WriteLog();
            return list;
        }
    }
}
