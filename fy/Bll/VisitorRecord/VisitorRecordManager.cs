using Dal;
using Dto.House;
using Modobay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Dto.VisitorRecord;

namespace Bll.VisitorRecord
{
    [System.ComponentModel.Description("行为采集")]
    public class VisitorRecordManager : IVisitorRecordManager
    {
        private readonly MLSDbContext db;
        private readonly IAppContext app;

        public VisitorRecordManager(Dal.MLSDbContext dbContext, Modobay.IAppContext appContext)
        {
            this.db = dbContext;
            this.app = appContext;
        }

        /// <summary>
        /// 获客明细（访问明细）
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public PagedList<VisitorRecordListDto> SearchByBrower(VisitorRecordQueryDto queryDto)
        {
            var query = from visitUser in db.VisitorBehaviorRecordQuery
                        join share in db.ShareRecordQuery on visitUser.ShareUUID equals share.UUID
                        join wechatUser in db.WechatUserInfoQuery on visitUser.VisitorTag equals wechatUser.UnionId
                        where visitUser.VisitorType == 2 && visitUser.BehaviorType == (int)BehaviorTypeEnum.浏览
                        && (share.SharerTag == queryDto.UserId.ToString() || share.OringnalShareUserId == queryDto.UserId)
                        && (visitUser.CreateTime >= queryDto.DateS && visitUser.CreateTime <= queryDto.DateE)
                        group visitUser by new
                        {
                            visitUser.VisitorTag,
                            wechatUser.Name,
                            wechatUser.ProfilePhotoPath,
                        } into g
                        select new VisitorRecordListDto
                        {
                            VisitorTag = g.Key.VisitorTag,
                            Name = g.Key.Name,
                            ProfilePhotoPath = g.Key.ProfilePhotoPath,
                            LastTime = g.Max(c => c.CreateTime),
                            BrowserCount = g.Count()
                        };
            return query.OrderByDescending(c => c.LastTime).GetPagedList(queryDto.PageIndex, queryDto.PageSize);
        }
    }
}
