using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Dal;
using Dto.Demo;
using Model;
using Microsoft.AspNetCore.Mvc;
using Modobay.Cache;
using Dto;

namespace Bll.Demo
{
    [Modobay.Api.NonToken]
    [System.ComponentModel.Description("Demo：跟进管理")]
    public class FollowUpManager : BaseManager<Model.FollowUp>, IFollowUpManager
    {

        public FollowUpManager(Dal.MLSDbContext dbContext, Modobay.IAppContext appContext) : base(dbContext, appContext)
        {
        }

        /// <summary>
        /// 新增跟进信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public int Add(FollowUpEditDto dto)
        {
            return base.AddByDto(dto);
        }

        /// <summary>
        /// 修改跟进信息
        /// </summary>
        /// <param name="dto"></param>
        public void Update(FollowUpEditDto dto)
        {
            if (base.UpdateByDto(dto) <= 0) app.ThrowException<FollowUpInfoDto>(Modobay.AppExceptionType.UpdateFail);
        }

        /// <summary>
        /// 删除跟进信息
        /// </summary>
        /// <param name="followUpID"></param>
        public void Delete(int followUpID)
        {
            if (base.DeleteByKey(followUpID) <= 0) app.ThrowException<FollowUpInfoDto>(Modobay.AppExceptionType.DeleteFail);
        }

        /// <summary>
        /// 获取跟进信息
        /// </summary>
        /// <param name="followUpID"></param>
        /// <returns></returns>
        [Modobay.Cache.Cache(CacheProfileName = Modobay.Cache.CacheTimeSettings.Minute)]
        public FollowUpInfoDto Get(int followUpID)
        {
            if (followUpID <= 0) return app.ThrowException<FollowUpInfoDto>(Modobay.AppExceptionType.ParameterError);

            // 方法1
            var list = Search(new FollowUpQueryDto() { FollowUpID = followUpID });
            if (list.Count <= 0) return app.ThrowException<FollowUpInfoDto>(Modobay.AppExceptionType.NotFound);
            return list[0];

            // 方法2：获取实体，并直接转为dto返回
            return base.GetDtoByKey<FollowUpInfoDto>(followUpID);

            // 方法3：获取实体，自行转换为dto后返回，可补充扩展信息
            var entity = base.GetByKey(followUpID);
            return ToDto(entity, null);
        }

        /// <summary>
        /// 查询跟进信息，根据查询条件
        /// </summary>
        /// <param name="queryDto">查询条件</param>
        /// <returns></returns>
        public PagedList<FollowUpInfoDto> Search(FollowUpQueryDto queryDto)
        {
            var query = from followUp in db.FollowUpQuery
                        join user in db.UsersQuery on followUp.CreateUserID equals user.Id into a
                        from user in a.DefaultIfEmpty()
                        join userUpdate in db.UsersQuery on followUp.UpdateUserID equals userUpdate.Id into b
                        from userUpdate in b.DefaultIfEmpty()
                        orderby followUp.CreateTime descending
                        select new
                        {
                            followUp,
                            extInfo = new
                            {
                                CreateUserName = user.UserName,
                                UpdateUserName = userUpdate.UserName
                            }
                        };

            if (queryDto.FollowUpID > 0)
            {
                query = query.Where(x => x.followUp.FollowUpID == queryDto.FollowUpID);
            }
            if (app.User?.UserID > 0)
            { // 通过上下文获取当前用户信息

            }

            var list = query.GetPagedList(queryDto.PageIndex, queryDto.PageSize);
            var data = new PagedList<FollowUpInfoDto>(list.CurrentPageIndex, list.PageSize, list.TotalItemCount);
            list.ForEach(item => data.Add(ToDto(item.followUp, item.extInfo)));
            return data;
        }

        private FollowUpInfoDto ToDto(FollowUp entity, dynamic extInfo = null)
        {
            var dto = Lib.Mapper<FollowUp, FollowUpInfoDto>.Map(entity);
            dto.CreateUserName = extInfo?.CreateUserName ?? string.Empty;
            dto.UpdateUserName = extInfo?.UpdateUserName ?? string.Empty;
            dto.StatusName = dto.Status.GetDescription();
            return dto;
        }
    }
}
