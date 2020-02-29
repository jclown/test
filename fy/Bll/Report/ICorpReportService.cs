using Dto.Report;
using System.Collections.Generic;
using Dto;

namespace Bll.Report
{
    public interface ICorpReportService
    {
        Modobay.ExtList<RankItemDto> GetWorkRank(StatItemTypeEnum statItemType, DateRangeEnum dateRange, string startDate, string endDate);
        List<StatItemDto> GetWorkReport(DateRangeEnum dateRange, string startDate, string endDate);
    }
}