using Dto;
using Dto.Demo;
using System;
using System.Collections.Generic;

namespace Bll.Demo
{
    public interface IFollowUpManager
    {
        int Add(FollowUpEditDto dto);
        void Update(FollowUpEditDto dto);
        void Delete(int followUpID);
        FollowUpInfoDto Get(int followUpID);
        PagedList<FollowUpInfoDto> Search(FollowUpQueryDto queryDto);
    }
}