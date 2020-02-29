using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace Lib
{
    public class HttpHelper
    {
        #region Static Field
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        #endregion

        #region public Method
        public static string PostHttpResponseJson(string url)
        {
            string json = string.Empty;
            Encoding encoding = Encoding.UTF8;
            HttpWebResponse Response = CreatePostHttpResponseJson(url, null, null, null, null, encoding, null);
            json = GetStream(Response, encoding);
            return json;
        }


        public static string PostHttpResponseJson(string url, string postJson)
        {
            string json = string.Empty;
            Encoding encoding = Encoding.UTF8;
            HttpWebResponse Response = CreatePostHttpResponseJson(url, postJson, null, null, null, encoding, null);
            json = GetStream(Response, encoding);
            return json;
        }


        public static string GetHttpResponse(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }



        /// <summary>
        /// 创建POST方式Json数据的HTTP请求（包括了https站点请求） 
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>
        /// <returns></returns>
        public static HttpWebResponse CreatePostHttpResponseJson(string url, string postJson, string parameters, int? timeout, string userAgent, Encoding requestEncoding, string referer)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }


            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }




            request.Method = "POST";
            //服务端 判断 客户端 提交的是否是 JSON数据 时
            request.ContentType = "application/json;charset=UTF-8";
            request.KeepAlive = true;


            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }


            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }


            //如果需要POST数据  
            #region post parameter  类似querystring格式
            if (parameters != null)
            {
                byte[] data = requestEncoding.GetBytes(parameters);
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                    stream.Close();
                }
            }
            #endregion


            #region post json
            if (!string.IsNullOrEmpty(postJson))
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    //string json = "{\"user\":\"test\"," +
                    //              "\"password\":\"bla\"}";


                    streamWriter.Write(postJson);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            #endregion




            if (!string.IsNullOrEmpty(referer))
            {
                request.Referer = referer;
            }


            HttpWebResponse response = request.GetResponse() as HttpWebResponse;


            if (request.CookieContainer != null)
            {
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
            }


            return response;
        }


        #endregion

        #region Private Method
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }


        /// <summary>
        /// 将response转换成文本
        /// </summary>
        /// <param name="response"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        private static string GetStream(HttpWebResponse response, Encoding encoding)
        {
            try
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    switch (response.ContentEncoding.ToLower())
                    {
                        case "gzip":
                            {
                                string result = Decompress(response.GetResponseStream(), encoding);
                                response.Close();
                                return result;
                            }
                        default:
                            {
                                using (StreamReader sr = new StreamReader(response.GetResponseStream(), encoding))
                                {
                                    string result = sr.ReadToEnd();
                                    sr.Close();
                                    sr.Dispose();
                                    response.Close();
                                    return result;
                                }
                            }
                    }
                }
                else
                {
                    response.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return "";
        }


        private static string Decompress(Stream stream, Encoding encoding)
        {
            byte[] buffer = new byte[100];
            //int length = 0;


            using (GZipStream gz = new GZipStream(stream, CompressionMode.Decompress))
            {
                //GZipStream gzip = new GZipStream(res.GetResponseStream(), CompressionMode.Decompress);
                using (StreamReader reader = new StreamReader(gz, encoding))
                {
                    return reader.ReadToEnd();
                }
                /*
                using (MemoryStream msTemp = new MemoryStream())
                {
                    //解压时直接使用Read方法读取内容，不能调用GZipStream实例的Length等属性，否则会出错：System.NotSupportedException: 不支持此操作；
                    while ((length = gz.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        msTemp.Write(buffer, 0, length);
                    }


                    return encoding.GetString(msTemp.ToArray());
                }
                 * */
            }
        }


        #endregion

        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>  
        /// <param name="timeout">请求的超时时间</param>  
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>  
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>  
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
        /// <returns></returns>  
        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, int? timeout, string userAgent, Encoding requestEncoding, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";

            request.Headers.Add("X_REG_CODE", "288a633ccc1");
            request.Headers.Add("X_MACHINE_ID", "a306b7c51254cfc5e22c7ac0702cdf87");
            request.Headers.Add("X_REG_SECRET", "de308301cf381bd4a37a184854035475d4c64946");
            request.Headers.Add("X_STORE", "0001");
            request.Headers.Add("X_BAY", "0001-01");
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("Accept-Language", "zh-CN");
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.Accept = "*/*";

            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            // if (cookies != null)
            // {
            request.CookieContainer = new CookieContainer();
            // request.CookieContainer.Add(cookies);
            // }
            //如果需要POST数据  
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }

                byte[] data = requestEncoding.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
            }

            return res;
        }
        public static string PostHttpResponse(string url, string postJson)
        {
            using (HttpWebResponse response = CreatePostHttpResponse(url, postJson, null, Encoding.UTF8, null))
            {
                StreamReader read = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string content = read.ReadToEnd();
                return content;
            }           
        }

        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="parameters">随同请求POST的参数</param>  
        /// <param name="timeout">请求的超时时间</param>   
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>  
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
        /// <returns></returns>  
        public static HttpWebResponse CreatePostHttpResponse(string url, string parameters, int? timeout, Encoding requestEncoding, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            //如果需要POST数据  
            if (!string.IsNullOrEmpty(parameters))
            {
                byte[] data = requestEncoding.GetBytes(parameters);
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }

        public class ParametersDto
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        /// 创建POST方式的HTTP请求  
        public static HttpWebResponse CreatePostHttpResponse(string url, List<ParametersDto> parameters, int timeout, string userAgent, CookieCollection cookies)
        {
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";           
            request.ContentType = "application/x-www-form-urlencoded";
            //设置代理UserAgent和超时
            //request.UserAgent = userAgent;
            //request.Timeout = timeout; 

            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            //发送POST数据  
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (var item in parameters)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", item.Key, item.Value);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", item.Key, item.Value);
                        i++;
                    }
                }
                byte[] data = Encoding.ASCII.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            string[] values = request.Headers.GetValues("Content-Type");
            return request.GetResponse() as HttpWebResponse;
        }

        /// <summary>
        /// 获取请求的数据
        /// </summary>
        public static string GetResponseString(HttpWebResponse webresponse)
        {
            using (Stream s = webresponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(s, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        /// <summary>  
        /// GET请求与获取结果  
        /// </summary>  
        public static dynamic HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        #region POST方式实现

        public static string CommonHttpRequestPost(string serviceUrl, string data, List<ParametersDto> headers)
        {
            try
            {
                //构造http请求的对象
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
                //转成网络流
                byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(data);
                //设置
                myRequest.Method = "POST";
                myRequest.ContentLength = buf.Length;
                myRequest.ContentType = "application/json;charset=UTF-8";
                myRequest.MaximumAutomaticRedirections = 1;
                myRequest.AllowAutoRedirect = true;
                myRequest.Timeout = 5000;
                //自定义Headers
                if (headers != null && headers.Count > 0)
                {
                    foreach (var item in headers)
                    {
                        myRequest.Headers.Add(item.Key, item.Value);
                    }
                }
                // 发送请求
                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(buf, 0, buf.Length);
                newStream.Close();
                // 获得接口返回值
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Lib.Log.WriteExceptionLog("【CommonHttpRequestPost】" + ex.ToString());
                return null;
            }
        }

        #endregion

        /// <summary>
        /// 获取url参数
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static NameValueCollection GetUrlParameters(string url)
        {
            var nvc = new NameValueCollection();
            string ps = url;
            // 开始分析参数对  
            Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
            MatchCollection mc = re.Matches(ps);
            foreach (Match m in mc)
            {
                nvc.Add(m.Result("$2").ToLower(), m.Result("$3"));
            }

            return nvc;
        }
    }
}
