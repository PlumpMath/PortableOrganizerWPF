using System;

namespace OrganizerTask1.UI.Misc
{
    public interface INotificationCenter
    {
        void AddMessageHandler(Action<Notification> action, NotificationName notificationName);
        void RemoveMessageHandler(Action<Notification> action, NotificationName name);
        void RemoveMessageHandler(Action<Notification> action);
        void PostNotification(NotificationName name, NotificationArgs args = null);
        void PostNotification(Notification newNotification);
    }
}
