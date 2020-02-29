using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UserBehaviorAttribute : Attribute
    {
        /// <summary>
        /// 用户行为类型：点赞、邮件发送。
        /// </summary>
        public string BehaviorType { get; set; }

        /// <summary>
        /// 用户行为标识：邮件询价。
        /// </summary>
        public string BehaviorCode { get; set; }

        /// <summary>
        /// 用户行为描述，摘要。
        /// </summary>
        public string BehaviorDesc { get; set; }

        /// <summary>
        /// 用户行为数据。
        /// </summary>
        public string BehaviorValue { get; set; }

        /// <summary>
        /// 指定值或获取值的表达式，如IN:quoteInfo.InquiryID。
        /// </summary>
        public string BehaviorValueExpression { get; set; }

        /// <summary>
        /// 关联对象类型，如Inquiry。
        /// </summary>
        public string RelationType { get; set; }


        public int RelationID { get; set; }

        public string RelationIDExpression { get; set; }
    }
}