using Dto.Demand;
using System;
using System.Collections.Generic;

namespace Bll.Demand
{
    public interface IDemandManager
    {
        PagedList<DemandListDto> Search(DemandQueryDto queryDto);
        List<DemandListDto> GetExpiryDemandList();
    }
}