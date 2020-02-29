using Quartz;
using System;
using System.Threading.Tasks;
using Modobay;

namespace Bll.Demand
{
    [Modobay.Api.NonController]
    [DisallowConcurrentExecution]
    public class DemandRemindJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            //var demandManager = AppManager.GetServiceFromRoot<IDemandManager>();
            //var expiryDemandList = demandManager.GetExpiryDemandList();
            //if (expiryDemandList.Count == null) return;

            //var notificationManager = AppManager.GetServiceFromRoot<Notification.INotificationManager>();
            //var template = notificationManager.GetMessageTemplaceContent(Dto.Notification.MessageTemplaceEnum.DemandExpriy);

        }
    }
}
