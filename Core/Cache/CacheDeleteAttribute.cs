//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Modobay.Cache
//{
//    [AttributeUsage(AttributeTargets.Method)]
//    public class CacheDeleteAttribute : Attribute
//    {
//        /// <summary>
//        /// 系统消息标识。与前端约定的标识，如Event_QuoteUpdated。
//        /// </summary>
//        public string MessageType { get; set; }

//        /// <summary>
//        /// 系统消息，如报价已更新。
//        /// </summary>
//        public string Content { get; set; }

//        /// <summary>
//        /// 关联对象类型，如Inquiry。
//        /// </summary>
//        public string RelationType { get; set; }

//        /// <summary>
//        /// 指定值或获取值的表达式，如IN{quoteInfo.InquiryID}。
//        /// </summary>
//        public string RelationIDExpression { get; set; }

//        /// <summary>
//        /// 指定值或获取值的表达式，如OUT{}。
//        /// </summary>
//        public string ToUserIDExpression { get; set; }

//        //public string CloseEvent { get; set; }

//        public int RelationID { get; internal set; }

//        /// <summary>
//        /// 指定值或获取值的表达式，如OUT{}。
//        /// </summary>
//        public int ToUserID { get; internal set; }

//    }
//}