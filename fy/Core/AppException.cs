using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modobay
{
    public class AppException : Exception
    {
        public AppException(string errorMessage, string errorCode = "") : base(errorMessage)
        {
            ErrorCode = errorCode;
        }

        public AppException(string errorMessage, AppExceptionType exceptionType) : base(errorMessage)
        {
            ErrorCode = ((int)exceptionType).ToString();
        }

        public AppException(Exception ex, AppExceptionType exceptionType) : base(((int)exceptionType).GetDescription<AppExceptionType>(),ex)
        {
            ErrorCode = ((int)exceptionType).ToString();
        }

        public AppException(AppExceptionType exceptionType) : base(((int)exceptionType).GetDescription<AppExceptionType>())
        {
            ErrorCode = ((int)exceptionType).ToString();
        }

        public string ErrorCode { get; set; }
    }
}
