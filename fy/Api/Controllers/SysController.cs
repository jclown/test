using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modobay;

namespace Api.Controllers
{
    [Route("[controller]")]
    [Modobay.Api.NonGateway]
    public class SysController : Controller
    {
        private static bool isEnableRealTimeLog = false;
        const string HtmlColor = "<font color=\"{0}\">{1}</font>";
        private readonly IAppContext app;

        public SysController(IAppContext appContext)
        {
            app = appContext;
            if (!isEnableRealTimeLog)
            {
                isEnableRealTimeLog = app.Configuration[$"EnableRealTimeLog"] == "1";
            }
        }

        [HttpGet]
        [Route("Check")]
        [Modobay.Api.NonPackageResultDto,Modobay.Api.NonToken]
        public string Check(string code)
        {
            if (code != "modobayMLS") return "access denied";
            // todo pxg 自检
            return "success";
        }

        /// <summary>
        /// 实时日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ServerSentEvents")]
        [Modobay.Api.NonLog, Modobay.Api.NonToken, Modobay.Api.NonCookie, Modobay.Api.NonPackageResultDto]
        public async Task ServerSentEventsAsync()
        {
            if (!isEnableRealTimeLog) return;

            Response.OnStarting(() =>
            {
                Response.Headers["Content-Type"] = "text/event-stream;charset=UTF-8";
                Response.Headers["Cache-Control"] = "no-cache";
                Response.Headers["Connection"] = "keep-alive";

                var requestInfoList = app.GetAllEndRequestInfo();
                var info = new StringBuilder();
                foreach (var item in requestInfoList)
                {
                    info.Append($"--------------RequestId:{item.RequestId ?? string.Empty}----------<br>");
                    info.Append($"Token:{item.Token ?? string.Empty}<br>");
                    info.Append($"SessionId:{item.SessionID ?? string.Empty}<br>");
                    info.Append($"Language:{item.Language ?? string.Empty}<br>");
                    info.Append($"ClientInfo:{item.ClientIpAddress ?? string.Empty}<br>");
                    info.Append($"StartTime:{item.StartTime.ToString("yyyy-MM-dd HH:mm:ss fff")}<br>");
                    info.Append($"EndTime:{(item.EndTime == null ? string.Empty : item.EndTime.ToString("yyyy-MM-dd HH:mm:ss fff"))}<br>");
                    info.Append($"RequestClassMethon:{item.RequestClassMethon ?? string.Empty}<br>");
                    info.Append($"RequestApi:{item.RequestApi ?? string.Empty}<br>");
                    info.Append($"RequestParams:{item.RequestParams ?? string.Empty}<br>");
                    info.Append($"Response:{item.ResponseResult ?? string.Empty}<br>");
                    info.Append($"Exception:{item.ExceptionLog ?? string.Empty}<br>");
                    info.Append($"AppLog:{item.AppLog ?? string.Empty}<br>");
                    info.Append($"DatabaseLog:{item.DbSqlLog ?? string.Empty}<br>");
                    info.Append($"---------------end---------------<br><br>");
                }

                var data = Convert.ToBase64String(Encoding.UTF8.GetBytes(info.ToString()));
                data = ServerSentEventData(data, DateTime.Now.Ticks.ToString());
                Response.HttpContext.Response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes(data));
                return Task.FromResult(0);
            });
        }

        /// <summary>
        /// 实时日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ServerSentEvents3")]
        [Modobay.Api.NonLog, Modobay.Api.NonToken, Modobay.Api.NonCookie, Modobay.Api.NonPackageResultDto]
        public async Task ServerSentEvents3Async()
        {
            if (!isEnableRealTimeLog) return;            
            Response.OnStarting(() =>
            {
                Response.Headers["Content-Type"] = "text/event-stream;charset=UTF-8";
                Response.Headers["Cache-Control"] = "no-cache";
                Response.Headers["Connection"] = "keep-alive";
                
                var requestInfoList = app.GetAllRequestInfo();
                var info = new StringBuilder();
                if (requestInfoList.Count > 0)
                {
                    var ipList = requestInfoList.GroupBy(x => x.ClientIpAddress).Select(x => new { ClientIpAddress = x.Key, Count = x.Count() }).ToList();
                    var sessionCount = requestInfoList.GroupBy(x => x.SessionID).Count();

                    info.Append($"<h4>{(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))}　接口{HtmlColor.FormatWith("#0000FF", requestInfoList.Count)}　");
                    info.Append($"客户端{HtmlColor.FormatWith("#0000FF", sessionCount)}　");
                    info.Append($"IP{HtmlColor.FormatWith("#0000FF", ipList.Count)}</h4>");
                    
                    foreach (var item in ipList)
                    {
                        info.Append($"<a href=\"http://www.ip138.com/ips138.asp?ip={item.ClientIpAddress}&action=2\" target=\"_blank\">{item.ClientIpAddress ?? "ClientIpAddress ??"}</a>({item.Count})　　　");
                    }
                    info.Append("<br>");

                    foreach (var item in requestInfoList)
                    {
                        var times = (int)((item.EndTime == DateTime.MinValue ? DateTime.Now : item.EndTime) - item.StartTime).TotalMilliseconds;
                        var timesDesc = (times > 2000 ? HtmlColor.FormatWith("#FF0000", times) : (times > 300 ? HtmlColor.FormatWith("#FFB354", times) : (item.EndTime == DateTime.MinValue ?  times.ToString() : HtmlColor.FormatWith("#00FF00", times))));
                        info.Append($"{(string.IsNullOrEmpty(item.ClientIpAddress) ? "未知用户" : item.ClientIpAddress)}　");
                        info.Append($"{(item.UserID == 0 ? (string.IsNullOrEmpty(item.SessionID) ? "未知用户" : item.SessionID) : "用户" + item.UserID.ToString())}　");
                        info.Append($"{item.StartTime.ToString("HH:mm:ss")}　");
                        info.Append($"{timesDesc}　");
                        info.Append($"RequestApi:{item.RequestApi ?? string.Empty}<br>");
                    }
                }

                string data = Convert.ToBase64String(Encoding.UTF8.GetBytes(info.ToString()));
                data = ServerSentEventData(data, DateTime.Now.Ticks.ToString());//, "message", 300);
                Response.HttpContext.Response.Body.Write(Encoding.UTF8.GetBytes(data), 0, data.Length);
                return Task.FromResult(0);
            });
        }

        private string ServerSentEventData(string data, string id, string _event = "message", long retry = 1000)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("retry:{0}\n", retry);
            sb.AppendFormat("event:{0}\n", _event);
            sb.AppendFormat("id:{0}\n", id);
            sb.AppendFormat("data:{0}\n\n", data);
            return sb.ToString();
        }

        /// <summary>
        /// 临时。将会关闭。
        /// </summary>
        /// <param name="toMail"></param>
        /// <param name="emailContent"></param>
        /// <param name="emailTitle"></param>
        [HttpGet]
        [Route("SendEmail")]
        public void SendEmail(string toMail, string emailContent, string emailTitle)
        {
            emailContent = Uri.UnescapeDataString(emailContent);
            var mailHelper = new Lib.MailHelper();
            mailHelper.Host = app.Configuration["EmailConfig:EmailServer"];
            mailHelper.Port = int.Parse(app.Configuration["EmailConfig:EmailServerPort"]);
            mailHelper.Send(app.Configuration["EmailConfig:Email"], app.Configuration["EmailConfig:EmailPassword"], toMail, emailContent, emailTitle);
        }

        [HttpGet]
        [Route("Test")]
        public void Test()
        {
            //Lib.ExcelHelper.ImportToDataTable()
        }
    }
}
