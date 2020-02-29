using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay.Api
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class OpenApiAttribute : Attribute
    {
        //public string ProfileName { get; set; }
        public bool IsSign { get; set; } = false;

        public bool IsNeedAccessToken { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signFieldExpression">指定值或获取值的表达式，如IN:quoteInfo.InquiryID。</param>
        public OpenApiAttribute(params string[] signFieldExpression)
        {
            if (signFieldExpression.Length > 0)
            {
                IsSign = true;
            }
        }

        /// <summary>
        /// 指定值或获取值的表达式，如IN:quoteInfo.InquiryID。
        /// </summary>
        public string SignFieldExpression { get; set; }
    }
}