using System.Windows.Input;
using OrganizerTask1.UI.Misc;
using OrganizerTask1.UI.ViewModels.Interfaces;
using OrganizerTasks1.DAL;
using OrganizerTasks1.Model;

namespace OrganizerTask1.UI.ViewModels
{
    class NotesViewModel : CollectionViewModel<NoteViewModel, Note>, INotesViewModel
    {
        public NotesViewModel(IDataProvider dataProvider, NotificationCenter notificationCenter)
            : base(dataProvider, notificationCenter)
        {
            NewCommand = new RelayCommand(New);
            EditCommand = new RelayCommand(Edit, CanEdit);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            notificationCenter.AddMessageHandler(OnItemEdited, NotificationName.CLOSE_ITEM_EDIT_MODAL);
        }

        protected override NoteViewModel CreateViewModelEntity(Note data)
        {
            return new NoteViewModel(data);
        }

        private void OnItemEdited(Notification n)
        {
            var args = n.GetArgs<NotificationArgsItemEditModalClose>();
            if (args == null)
                return;

            NoteViewModel editedItem = args.ResultEntity as NoteViewModel;
            if (editedItem == null)
                return;

            Entities.Add(editedItem);
        }

        public ICommand NewCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public void New(object args)
        {
            ViewModelBase editor = new EditorViewModel(_notificationCenter) { EditingItem = new NoteViewModel(new Note()) };
            _notificationCenter.PostNotification(NotificationName.SHOW_ITEM_EDIT_MODAL, new NotificationArgsItemEditModalShow(editor));
        }

        public void Edit(object args)
        {
            ViewModelBase editor = new EditorViewModel(_notificationCenter) { EditingItem = SelectedEntity };
            _notificationCenter.PostNotification(NotificationName.SHOW_ITEM_EDIT_MODAL, new NotificationArgsItemEditModalShow(editor));
        }

        public bool CanEdit(object args)
        {
            return SelectedEntity != null;
        }

        public void Delete(object args)
        {
            _dataProvider.Notes.Remove(SelectedEntity._note);
            Entities.Remove(SelectedEntity);
        }

        public bool CanDelete(object args)
        {
            return SelectedEntity != null;
        }
    }
}
