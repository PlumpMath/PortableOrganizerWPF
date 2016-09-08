using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OrganizerTask1.UI.Misc
{
    public class NotificationCenter : INotificationCenter
    {
        private readonly Dictionary<NotificationName, List<NotificationHandler>> notifications =
            new Dictionary<NotificationName, List<NotificationHandler>>();

        public void AddMessageHandler(Action<Notification> action, NotificationName notificationName)
        {
            if (notificationName == NotificationName.NOT_SET)
            {
                Debug.WriteLine("Null name specified for notification in AddObserver.");
                return;
            }
            var notificationHandler = new NotificationHandler(action);

            if (!notifications.ContainsKey(notificationName))
            {
                notifications.Add(notificationName, new List<NotificationHandler>());
            }

            var notifyList = notifications[notificationName];
            notifyList.Add(notificationHandler);
        }

        public void RemoveMessageHandler(Action<Notification> action, NotificationName name)
        {
            List<NotificationHandler> notifyList;
            notifications.TryGetValue(name, out notifyList);

            if (notifyList == null || notifyList.Count == 0)
            {
                notifications.Remove(name);
                return;
            }

            var handler = notifyList.Find(n => n.action == action);

            if (handler != null)
            {
                notifyList.Remove(handler);
            }
        }

        public void RemoveMessageHandler(Action<Notification> action)
        {
            foreach (var notificationName in notifications.Keys)
            {
                RemoveMessageHandler(action, notificationName);
            }
        }

        public void PostNotification(NotificationName name, NotificationArgs args = null)
        {
            PostNotification(new Notification(name, args));
        }

        public void PostNotification(Notification newNotification)
        {
            if (newNotification.name == NotificationName.NOT_SET)
            {
                Debug.WriteLine("Null name sent to PostNotification.");
                return;
            }

            List<NotificationHandler> notifyList;
            notifications.TryGetValue(newNotification.name, out notifyList);

            if (notifyList == null)
            {
                notifications.Remove(newNotification.name);
                return;
            }

            var handlersToRemove = new List<NotificationHandler>();

            foreach (var handler in notifyList)
            {
                if (handler != null && handler.action != null)
                {
                    handler.action.Invoke(newNotification);
                }
                else
                {
                    handlersToRemove.Add(handler);
                }
            }

            foreach (var observer in handlersToRemove)
            {
                notifyList.Remove(observer);
            }
        }
    }
}
