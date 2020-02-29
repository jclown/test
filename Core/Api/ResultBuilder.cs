using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace Modobay.Api
{
    public static class ResultBuilder
    {
        private static readonly string Crlf = ((char)13).ToString() + ((char)10).ToString();

        public static ResultDto<T> AsSuccess<T>(T data)
        {
            var resultDto = new ResultDto<T>();
            resultDto.Data = data;
            if (data.GetType().Name.StartsWith("ExtList"))
            {                
                resultDto.DataExt = (data as dynamic).DataExt;
            }
            resultDto.Message = "操作成功";
            resultDto.Success = true;
            return resultDto;
        }

        public static ExceptionResultDto AsFail(Exception ex,string requestId)
        {

            var resultDto = new ExceptionResultDto() { Success = false, RequestId = requestId };
            if (ex == null) return resultDto;

            var type = ex.GetType();

            if (ex is AppException || ex.GetType().BaseType is AppException)
            {
                resultDto.ErrorCode = (ex as AppException).ErrorCode;
                resultDto.ErrorMessage = ex.Message;
            }
            else
            {
                // 非业务异常的，转为友好提示
                resultDto.ErrorMessage = $"系统繁忙。{requestId}";

                if (ex.InnerException == null)
                {
                    if (ConfigManager.Configuration["ErrLog"] == "1") resultDto.ErrorMessage = $"{ex.Message}  <br> StackTrace:{ex.StackTrace}";
                    Lib.Log.WriteExceptionLog($"requestId:{requestId} {Crlf}  Message:{ex.Message}  {Crlf}  StackTrace:{ex.StackTrace}");
                }
                else
                {
                    if (ConfigManager.Configuration["ErrLog"] == "1") resultDto.ErrorMessage = $"{ex.InnerException.Message}  <br> StackTrace:{ex.StackTrace}";
                    Lib.Log.WriteExceptionLog($"requestId:{requestId} {Crlf}  Message:{ex.InnerException.Message}  {Crlf}  StackTrace:{ex.InnerException.StackTrace}");
                }
            }

            return resultDto;
        }
    }
}