using OrganizerTask1.UI.ViewModels;

namespace OrganizerTask1.UI.Misc
{
    public abstract class NotificationArgs
    {
    }

    public class NotificationArgsEmpty : NotificationArgs
    {
        public NotificationArgsEmpty()
        {
        }
    }

    public class NotificationArgsItemEditModalShow : NotificationArgs
    {
        public ViewModelBase ModalViewModel { get; private set; }

        public NotificationArgsItemEditModalShow(ViewModelBase modalViewModel)
        {
            ModalViewModel = modalViewModel;
        }
    }

    public class NotificationArgsItemEditModalClose : NotificationArgs
    {
        public ViewModelBase ResultEntity { get; private set; }

        public NotificationArgsItemEditModalClose(ViewModelBase resultEntity)
        {
            ResultEntity = resultEntity;
        }
    }
}
