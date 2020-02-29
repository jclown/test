using Quartz;
using System;
using System.Threading.Tasks;

namespace Modobay.Discovery
{
    [Modobay.Api.NonController]
    public class KeepServiceScheduleJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var discovery = AppManager.GetServiceFromRoot<Modobay.Discovery.IDiscoveryServer>();
            discovery.Keep(AppManager.ServiceAddress);
        }
    }
}
