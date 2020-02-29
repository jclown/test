using Dto.House;
using System;

namespace Bll.House
{
    public interface IHouseManager
    {
        PagedList<HouseListDto> Search(HouseQueryDto queryDto);
    }
}