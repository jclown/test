using Dto.VisitorRecord;
using System;

namespace Bll.VisitorRecord
{
    public interface IVisitorRecordManager
    {
        PagedList<VisitorRecordListDto> SearchByBrower(VisitorRecordQueryDto queryDto);
    }
}