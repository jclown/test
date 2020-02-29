using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Modobay.Proxy
{
    public class HttpClientHelper
    {
        private readonly string _baseurl;
        private readonly string _token;
        private readonly string _appid;
        private readonly string _sessionid;
        private readonly string _requestId;
        private readonly Encoding _encoding;

        public HttpClientHelper(string url, string token, string appid, string sessionid, string requestId)
        {
            _baseurl = url + (url.EndsWith("/") ? "" : "/");
            _token = token;
            _appid = appid;
            _sessionid = sessionid;
            _requestId = requestId;
            _encoding = Encoding.UTF8;
        }

        private WebClient GetClient()
        {
            var client = new HttpClient();
            client.Headers.Add("Content-Type", "application/json");
            client.Headers.Add("appid", _appid);
            client.Headers.Add("token", _token);
            client.Headers.Add("sessionId", _sessionid);
            client.Headers.Add("requestId", _requestId);
            return client;
        }

        private dynamic GetResult<T>(string resultString)
        {
            dynamic result = null;

            try
            {
                var typeName = typeof(T).Name;
                if ((typeName == "Task`1" && typeof(T).GenericTypeArguments.Length > 0) || typeName == "Task")
                {
                    var ttt = typeof(T).GenericTypeArguments[0];
                    var tt = typeof(Modobay.Api.ResultDto<>).MakeGenericType(ttt);
                    dynamic result2 = JsonConvert.DeserializeObject(resultString, tt, new JsonInt32Converter());

                    if (result2.Success)
                    {
                        var type = result2?.Data?.GetType();
                        if (type != null && type.FullName.StartsWith("System.PagedList"))
                        {
                            var totalCountProperty = type.GetProperty("TotalItemCount");
                            totalCountProperty.SetValue(result2.Data, result2.TotalCount, null);

                            var pageCountProperty = type.GetProperty("TotalPageCount");
                            pageCountProperty.SetValue(result2.Data, result2.PageCount, null);
                        }

                        if (result2.Data is null) return Task.FromResult(default(T));
                        if (ttt.FullName == "System.Object")
                        {
                            return Task.FromResult(result2.Data as object);
                        }
                        else
                        {
                            return Task.FromResult(result2.Data as dynamic);
                        }
                    }
                    else
                    {
                        AppManager._proxyAppException = new AppException(result2.Message, string.Empty);
                        return Task.FromResult(result2.Data);
                    }
                }
                else
                {
                    result = JsonConvert.DeserializeObject<Modobay.Api.ResultDto<T>>(resultString, new JsonInt32Converter());
                    if (result.Success)
                    {
                        var type = result?.Data?.GetType();
                        if (type != null && type.FullName.StartsWith("System.PagedList"))
                        {
                            var totalCountProperty = type.GetProperty("TotalItemCount");
                            totalCountProperty.SetValue(result.Data, result.TotalCount, null);

                            var pageCountProperty = type.GetProperty("TotalPageCount");
                            pageCountProperty.SetValue(result.Data, result.PageCount, null);
                        }

                        if (result.Data is null) return Task.FromResult(default(T));
                        return Task.FromResult(result.Data);
                    }
                    else
                    {
                        AppManager._proxyAppException = new AppException(result.Message, string.Empty);
                        return Task.FromResult(default(T));
                    }
                }
            }
            catch (Exception ex) { Lib.Log.WriteExceptionLog($"GetResult:Message:{ex.Message}  <br> StackTrace:{ex.StackTrace} <br> resultString:{resultString}"); }

            return result?.Data;
        }

        /// <summary>
        /// Post the specified resoure, data and paras.
        /// </summary>
        /// <param name="resoure">Resoure.</param>
        /// <param name="data">Data.</param>
        /// <param name="paras">Paras.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        /// <typeparam name="R">The 2nd type parameter.</typeparam>
        public T Post2<T, R>(string resoure, R data, string queryString)//NameValueCollection paras
        {
            //var queryString = string.Empty;
            //for (int i = 0; i < paras.Count; i++)
            //{
            //    var itemQueryString = $"{paras.Keys[i]}={paras[i]}";
            //    queryString += (queryString.Length > 0) ? ("&" + itemQueryString) : ("?" + itemQueryString);
            //}
            //foreach (var item in paras)
            //{                
            //    var itemQueryString = $"{item.Key}={item.Value}";
            //    queryString += (queryString.Length > 0) ? ("&" + itemQueryString) : ("?" + itemQueryString);
            //}
            var url = _baseurl + resoure + queryString;

            //Logging.Log.Debug("接口url", url);

            var client = GetClient();
            var postString = JsonConvert.SerializeObject(data);
            //Logging.Log.Debug("发送到接口的json数据", postString);
            byte[] result = null;

            if (data == null)
            {
                //var aa = client.DownloadString(url);
                result = client.DownloadData(url);
            }
            else
                result = client.UploadData(url, _encoding.GetBytes(postString));

            var resultString = _encoding.GetString(result);
            //Logging.Log.Debug("接口返回字符串", resultString);
            client.Dispose();
            var typeName = typeof(T).Name;

            //var returnResult = (typeName == "Task`1" || typeName == "Task") ? GetResult<T>(resultString) : GetResult<T>(resultString).Result;
            dynamic returnResult = null;
            try
            {
                if (typeName == "Task`1")
                {
                    returnResult = GetResult<T>(resultString);
                }
                else if (typeName == "Task")
                {

                }
                else
                {
                    returnResult = GetResult<T>(resultString).Result;
                }
            }
            catch
            {
                Lib.Log.WriteExceptionLog($"url {url}");
                returnResult = GetResult<T>(resultString).Result;
            }

            return returnResult;
        }


        //public async Task<T> AsyncPost<T, R>(string resoure, R data, NameValueCollection paras) where T : class
        //{
        //    foreach (var item in paras)
        //    {
        //        resoure = resoure.Replace("{" + item.Key + "}", item.Value);
        //    }
        //    var url = _baseurl + resoure;
        //    //Logging.Log.Debug("接口url", url);

        //    var postString = JsonConvert.SerializeObject(data);
        //    //Logging.Log.Debug("发送到接口的json数据", postString);

        //    System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        //    HttpResponseMessage response = await client.GetAsync(url);

        //    response.EnsureSuccessStatusCode();
        //    string resultString = await response.Content.ReadAsStringAsync();
        //    client.Dispose();

        //    //Logging.Log.Debug("接口返回字符串", resultString);

        //    return GetResult<T>(resultString);
        //}

        public T PostStream<T>(string resoure, Stream data) where T : class
        {
            var url = _baseurl + resoure;

            byte[] result;

            var client = GetClient();
            result = client.UploadData(url, StreamToBytes(data));
            var resultString = _encoding.GetString(result);
            //Logging.Log.Debug("接口返回字符串", resultString);
            client.Dispose();
            return GetResult<T>(resultString).Result;
        }

        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        public Stream GetStream<R>(string resoure, R data, Dictionary<string, string> paras)
        {
            foreach (var item in paras)
            {
                resoure = resoure.Replace("{" + item.Key + "}", item.Value);
            }
            var url = _baseurl + resoure;
            //Logging.Log.Debug("接口url", url);

            var client = GetClient();
            var postString = JsonConvert.SerializeObject(data);
            //Logging.Log.Debug("发送到接口的json数据", postString);
            byte[] result;


            if (data == null)
                result = client.DownloadData(url);
            else
                result = client.UploadData(url, _encoding.GetBytes(postString));
            Stream stream = null;

            if (result.Length != 0)
                stream = new MemoryStream(result);

            return stream;
        }

        public void GetStreamAsync<T>(object parameter, string resoure, T data, Dictionary<string, string> paras, System.Net.DownloadDataCompletedEventHandler downloadDataCallback)
        {
            foreach (var item in paras)
            {
                resoure = resoure.Replace("{" + item.Key + "}", item.Value);
            }
            var url = _baseurl + resoure;
            //Logging.Log.Debug("接口url", url);

            var client = GetClient();
            var postString = JsonConvert.SerializeObject(data);
            //Logging.Log.Debug("发送到接口的json数据", postString);
            byte[] result;

            if (data == null)
            {
                client.DownloadDataCompleted += downloadDataCallback;
                client.DownloadDataAsync(new Uri(url), parameter);
            }
            else
            {
                //upload
                client.DownloadDataCompleted += downloadDataCallback;
                client.DownloadDataAsync(new Uri(url));
            }
        }

        public string GetString(string url)
        {
            var client = GetClient();
            return client.UploadString(url, "");
        }

        //public async Task SendAsync(ApiActionContext context)
        //{
        //    if (context is HttpApiActionContext httpContext)
        //    {
        //        httpContext.ResponseMessage = await this.HttpClient.SendAsync(httpContext.RequestMessage);

        //        if (!httpContext.ResponseMessage.IsSuccessStatusCode)
        //            throw new HttpRequestException(httpContext.ResponseMessage.ReasonPhrase);
        //    }
        //}
    }
}
