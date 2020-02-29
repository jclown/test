using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.IDS
{
    public class TokenException : Modobay.AppException
    {
        public TokenException(TokenExceptionType exceptionType = TokenExceptionType.Null) : base(GetErrorMessage(exceptionType))
        {
            ErrorCode = ((int)exceptionType).ToString();
        }

        private static string GetErrorMessage(TokenExceptionType exceptionType)
        {
            switch (exceptionType)
            {
                case TokenExceptionType.Null:
                    return "token不能为空。";
                case TokenExceptionType.Invalid:
                    return "token无效。";
                case TokenExceptionType.Expired:
                    return "token已过期。";
                case TokenExceptionType.Duplicate:
                    return "token重复。";
                default:
                    return string.Empty;
            }
        }

    }
}
