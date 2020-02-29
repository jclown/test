using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;

namespace Modobay.Api
{
    internal class ExceptionFilter : IExceptionFilter
    {
        private readonly IAppContext app;

        public ExceptionFilter(IAppContext appContext)
        {
            this.app = appContext;
        }

        public void OnException(ExceptionContext context)
        {
            try
            {
                var resultValue = ResultBuilder.AsFail(context.Exception, app.ReuqestID);
                context.Result = new JsonResult(resultValue);

                // id为空的是忽略记录日志的接口
                try
                {
                    if (!string.IsNullOrEmpty(app.ReuqestID))
                    {
                        app.RequestInfo.ExceptionLog = "记录请求异常时报错";
                        app.RequestInfo.ExceptionLog = JsonConvert.SerializeObject(resultValue);
                        app.EndRequest();
                    }
                }
                catch { }
            }
            catch { }
        }
    }
}
