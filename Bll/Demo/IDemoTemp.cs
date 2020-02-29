using Dto;
using Dto.Demo;
using System;
using System.Collections.Generic;

namespace Bll.Demo
{
    public interface IDemoTemp
    {
        Modobay.Api.ExceptionResultDto ApiResultDto(Modobay.Api.ResultDto<object> resultDto, Modobay.AppExceptionType appExceptionType);
        void ApiRequestDto1(QueryDto queryDto);
        void ApiRequestDto2(DictQueryDto dictQueryDto);
        void ApiRequestDto3(StatusQueryDto statusQueryDto);
    }
}