using System;

namespace OrganizerTask1.UI.Misc
{
    public class Notification
    {
        public NotificationName name;
        public NotificationArgs args;

        public Notification(NotificationName _name, NotificationArgs _args = null)
        {
            name = _name;
            args = _args;
        }

        public T GetArgs<T>() where T : NotificationArgs
        {
            if (args != null)
            {
                if (!(args is T))
                {
                    throw new InvalidCastException(string.Format("Try cast {0} to {1} for {2}", args.GetType(),
                        typeof(T), name));
                }                
            }
            return args as T;
        }
    }

    public class NotificationHandler : IEquatable<NotificationHandler>
    {
        public Action<Notification> action;
        public bool Invalid;

        public NotificationHandler(Action<Notification> action)
        {
            this.action = action;
        }

        public bool Equals(NotificationHandler other)
        {
            if (other == null)
            {
                return false;
            }
            return other.action == action;
        }
    }

    public enum NotificationName
    {
        NOT_SET,
        SHOW_ITEM_EDIT_MODAL,
        CLOSE_ITEM_EDIT_MODAL,
        SAVE_ITEM_VALIDATE_REQUEST,
        SAVE_ITEM_VALIDATE_RESPONSE,
    }
}
