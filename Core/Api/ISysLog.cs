using Modobay.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Modobay.Api
{
    public interface ISysLog : IDisposable
    {
        Task WriteLog(params RequestInfoDto[] logItems);
        Task WriteSysExceptionLog(params RequestInfoDto[] logItems);
    }
}
