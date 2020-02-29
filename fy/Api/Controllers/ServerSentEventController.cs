using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("/sse")]
    public class ServerSentEventController : Controller
    {
        [HttpGet]
        public async Task Get()
        {
            var response = Response;
            response.Headers.Add("Content-Type", "text/event-stream");

            for (var i = 0; true; ++i)
            {
                await response
                    .WriteAsync($"data: Controller {i} at {DateTime.Now}\r\r");

                response.Body.Flush();
                await Task.Delay(5 * 1000);
            }
        }
    }
}