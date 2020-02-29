using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using StackExchange.Redis;

namespace Modobay.Api
{
    [Serializable]
    public class RequestInfoDto
    {
        public string RequestId { get; set; }
        //public string TaskId { get; set; }
        public int UserID { get; set; }
        public string ClientIpAddress { get; set; }
        public string ClientIpPort { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string RequestClassMethon { get; set; }
        public string SessionID { get; set; }
        public string Token { get; set; }
        public string Language { get; set; }
        public string RequestApi { get; set; }
        public string RequestParams { get; set; }
        public string AppLog { get; set; }
        public string DbSqlLog { get; set; }
        public string ResponseResult { get; set; }
        public string ExceptionLog { get; set; }
    }
}
