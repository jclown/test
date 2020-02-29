using Dto;
using Modobay;
using Modobay.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bll.Demo
{
    [System.ComponentModel.Description("Demo：此类仅用于产生通用相应dto的文档说明")]
    public class DemoTemp : IDemoTemp
    {
        /// <summary>
        /// 此接口仅用于产生通用相应dto的文档说明
        /// </summary>
        /// <param name="queryDto"></param>
        public void ApiRequestDto1(QueryDto queryDto)
        {            
        }

        /// <summary>
        /// 此接口仅用于产生通用相应dto的文档说明
        /// </summary>
        /// <param name="dictQueryDto"></param>
        public void ApiRequestDto2(DictQueryDto dictQueryDto)
        {            
        }

        /// <summary>
        /// 此接口仅用于产生通用相应dto的文档说明
        /// </summary>
        /// <param name="statusQueryDto"></param>
        public void ApiRequestDto3(StatusQueryDto statusQueryDto)
        {            
        }

        /// <summary>
        /// 此接口仅用于产生通用相应dto的文档说明
        /// </summary>
        /// <param name="resultDto"></param>
        /// <param name="appExceptionType"></param>
        /// <returns></returns>
        public ExceptionResultDto ApiResultDto(ResultDto<object> resultDto, AppExceptionType appExceptionType)
        {
            return null;
        }        
    }
}
