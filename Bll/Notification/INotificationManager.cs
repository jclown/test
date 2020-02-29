using Dto.Notification;
using System;
using System.Collections.Generic;

namespace Bll.Notification
{
    public interface INotificationManager
    {
        int Build(Dto.Notification.MessageTemplaceEnum messageTemplace, MessageTypeEnum messageType, NotificationTypeEnum notificationType, List<int> userIdList);
    }
}