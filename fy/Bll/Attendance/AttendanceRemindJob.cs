using Quartz;
using System;
using System.Threading.Tasks;
using Modobay;
using System.Linq;
using System.Collections.Generic;
using Dto.Notification;

namespace Bll.Attendance
{
    [Modobay.Api.NonController]
    [DisallowConcurrentExecution]
    public class AttendanceRemindJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var attendanceManager = AppManager.GetServiceFromRoot<IAttendanceManager>();
            var remindList = attendanceManager.GetAttendanceRemindList(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //var remindList = attendanceManager.GetAttendanceRemindList("2020-02-22 18:05:01");
            if (remindList.Count == 0) return;
            Lib.Log.WriteOperationLog($"AttendanceRemindJob:匹配提醒规则的记录数 {remindList.Count}");

            // todo pxg 重复获取了已签到的，GetAttendanceRemindList已经判断了
            var notificationList = new List<Model.ChineseUserNotifications>();
            var notificationManager = AppManager.GetServiceFromRoot<Notification.INotificationManager>();
            var checkInUserIdList = remindList.Where(x => x.CheckType == 1).Select(x => x.UserId).ToList();
            if (checkInUserIdList.Count > 0)
            {
                notificationManager.Build(MessageTemplaceEnum.AttendanceCheckIn, MessageTypeEnum.AtWork, NotificationTypeEnum.Work, checkInUserIdList);
            }

            var checkOutUserIdList = remindList.Where(x => x.CheckType == 2).Select(x => x.UserId).ToList();
            if (checkOutUserIdList.Count > 0)
            {
                notificationManager.Build(MessageTemplaceEnum.AttendanceCheckOut, MessageTypeEnum.OffDuty, NotificationTypeEnum.Work, checkOutUserIdList);
            }
        }
    }
}
