using System;
using System.Windows.Input;
using OrganizerTask1.UI.Misc;
using OrganizerTask1.UI.ViewModels.Interfaces;
using OrganizerTask1.UI.ViewModels.Validation;

namespace OrganizerTask1.UI.ViewModels
{
    public class EditorViewModel : ViewModelBase, IItemEditorVM
    {
        public ICommand CloseCommand { get { return _closeCommand; } }
        public ICommand SaveCommand { get { return _saveCommand; } }

        private readonly ICommand _closeCommand;
        private readonly ICommand _saveCommand;
        private readonly INotificationCenter notificationCenter;

        private bool disabled = false;

        public EditorViewModel(INotificationCenter _notificationCenter)
        {
            _closeCommand = new RelayCommand(Close, CanClose);
            _saveCommand = new RelayCommand(Save, CanSave);
            notificationCenter = _notificationCenter;
        }

        private ViewModelBase _editingItem;
        public ViewModelBase EditingItem
        {
            get { return _editingItem; }
            set { _editingItem = value; }
        }

        private string _errorBarMessage;
        public string ErrorBarMessage
        {
            get { return _errorBarMessage; }
            set
            {
                _errorBarMessage = value;
                OnPropertyChanged("ErrorBarMessage");
            }
        }

        private void Close(object args)
        {
            notificationCenter.PostNotification(NotificationName.CLOSE_ITEM_EDIT_MODAL);
        }

        private void Save(object args)
        {
            disabled = true;
            ErrorBarMessage = "waiting for validation to complete...";
            notificationCenter.AddMessageHandler(SaveRequestResult, NotificationName.SAVE_ITEM_VALIDATE_RESPONSE);
            notificationCenter.PostNotification(NotificationName.SAVE_ITEM_VALIDATE_REQUEST, new NotificationArgsValidateItemSaveRequest(EditingItem));
        }

        private void SaveRequestResult(Notification n)
        {
            notificationCenter.RemoveMessageHandler(SaveRequestResult, NotificationName.SAVE_ITEM_VALIDATE_RESPONSE);
            var args = n.GetArgs<NotificationArgsValidateItemSaveResponse>();
            if (args != null)
            {
                if (string.IsNullOrEmpty(args.ErrorMessage))
                {
                    notificationCenter.PostNotification(NotificationName.CLOSE_ITEM_EDIT_MODAL, new NotificationArgsItemEditModalClose(EditingItem));
                    RemoveError("ErrorBarMessage", "HasErrors");
                }
                else
                {
                    AddError(new ValidationError("ErrorBarMessage", "HasErrors", args.ErrorMessage));
                }
            }
            else
            {
                throw new ArgumentNullException("n", @"Notification args is not valid NotificationArgsValidateItemSaveResponse type");
            }

            disabled = false;
        }

        private bool CanSave(object obj)
        {
            return !EditingItem.HasErrors && !HasErrors && !disabled;
        }

        private bool CanClose(object obj)
        {
            return !disabled;
        }
    }
}
