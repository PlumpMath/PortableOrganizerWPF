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

    public class NotificationArgsValidateItemSaveRequest : NotificationArgs
    {
        public ViewModelBase ItemEntity { get; private set; }

        public NotificationArgsValidateItemSaveRequest(ViewModelBase itemEntity)
        {
            ItemEntity = itemEntity;
        }
    }

    public class NotificationArgsValidateItemSaveResponse : NotificationArgs
    {
        public string ErrorMessage { get; private set; }

        public NotificationArgsValidateItemSaveResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
