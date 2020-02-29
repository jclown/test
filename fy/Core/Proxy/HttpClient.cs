using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Modobay.Proxy
{
    public class HttpClient : WebClient
    {
        private int _timeout;

        /// <summary>
            /// 超时时间(毫秒)
            /// </summary>
        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }

        public HttpClient()
        {
            this._timeout = 1800000;
        }

        public HttpClient(int timeout)
        {
            this._timeout = timeout;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            //var result = WebRequest.CreateHttp(address);
            //result.ProtocolVersion = new Version(2, 0);
            var result = base.GetWebRequest(address);
            result.Timeout = this._timeout;
            result.Headers["Connection"] = "keep-alive";
            return result;
        }
    }
}
