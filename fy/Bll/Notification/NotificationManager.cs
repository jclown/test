using Dal;
using Modobay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Model;
using Dto;
using Dto.Notification;
//using Dto.Notification;

namespace Bll.Notification
{
    [Modobay.Api.NonController]
    [System.ComponentModel.Description("需求（AI找房）")]
    public class NotificationManager : INotificationManager
    {
        private readonly MLSDbContext db;
        private readonly IAppContext app;

        public NotificationManager(Dal.MLSDbContext dbContext, Modobay.IAppContext appContext)
        {
            this.db = dbContext;
            this.app = appContext;
        }

        #region 消息模版

        public int Build(MessageTemplaceEnum messageTemplace, MessageTypeEnum messageType, NotificationTypeEnum notificationType, List<int> userIdList)
        {
            var template = db.MessageTemplateQuery.FirstOrDefault(x => x.Code == messageTemplace.ToString());
            if (template == null)
            {
                Lib.Log.WriteExceptionLog($"NotificationManager.Build：找不到消息模版{messageTemplace.ToString()}");
                return default;
            }

            var notificationList = new List<ChineseUserNotifications>();
            foreach (var userId in userIdList)
            {
                var notification = db.Create<ChineseUserNotifications>();
                notification.UserId = userId;
                notification.Title = template.MessageTitle;
                notification.Content = template.MessageContent;
                notification.Type = (short)notificationType;
                notification.IsDel = false;
                // todo pxg 暂时写死默认已读
                notification.IsRead = true;
                notification.NotificationId = 0;
                notification.MessageType = (int)messageType;
            }

            return db.SaveChanges();
        }
        

        #endregion


    }
}
