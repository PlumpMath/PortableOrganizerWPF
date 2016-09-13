using System;
using System.Windows.Input;
using OrganizerTask1.UI.Misc;
using OrganizerTask1.UI.ViewModels.Interfaces;

namespace OrganizerTask1.UI.ViewModels
{
    public class EditorViewModel : ViewModelBase, IItemEditorVM
    {
        public ICommand CloseCommand { get { return _closeCommand; } }
        public ICommand SaveCommand { get { return _saveCommand; } }

        private readonly ICommand _closeCommand;
        private readonly ICommand _saveCommand;
        private readonly INotificationCenter notificationCenter;

        public EditorViewModel(INotificationCenter _notificationCenter)
        {
            _closeCommand = new RelayCommand(Close);
            _saveCommand = new RelayCommand(Save, CanSave);
            notificationCenter = _notificationCenter;
        }

        private ViewModelBase _editingItem;
        public ViewModelBase EditingItem
        {
            get { return _editingItem; }
            set { _editingItem = value; }
        }

        private void Close(object args)
        {
            notificationCenter.PostNotification(NotificationName.CLOSE_ITEM_EDIT_MODAL);
        }

        private void Save(object args)
        {
            notificationCenter.PostNotification(NotificationName.CLOSE_ITEM_EDIT_MODAL, new NotificationArgsItemEditModalClose(EditingItem));
        }

        private bool CanSave(object obj)
        {
            return !EditingItem.HasErrors;
        }
    }
}
