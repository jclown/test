using Dto.HouseGroup;
using System;
using System.Collections.Generic;

namespace Bll.HouseGroup
{
    public interface IHouseGroupManager
    {
        List<PrincipalDetailDto> Principal();
    }
}