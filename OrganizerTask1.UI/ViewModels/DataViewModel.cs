using System;
using System.Windows.Input;
using OrganizerTask1.UI.Misc;
using OrganizerTask1.UI.ViewModels.Interfaces;

namespace OrganizerTask1.UI.ViewModels
{
    public class DataViewModel : ViewModelBase, IMainWindowVM, IDisposable
    {
        public ICommand SetControlVisibility { get; set; }
        private INotificationCenter notificationCenter;

        public DataViewModel(ITasksViewModel tasksViewModel, INotesViewModel notesViewModel, IEventsViewModel eventsViewModel, INotificationCenter notificationCenter)
        {
            TasksViewModel = tasksViewModel;
            NotesViewModel = notesViewModel;
            EventsViewModel = eventsViewModel;

            SetControlVisibility = new RelayCommand(ControlVisibility);

            this.notificationCenter = notificationCenter;
            notificationCenter.AddMessageHandler(ShowModal, NotificationName.SHOW_ITEM_EDIT_MODAL);
            notificationCenter.AddMessageHandler(CloseModal, NotificationName.CLOSE_ITEM_EDIT_MODAL);
        }
        
        private IVewModel _currentVM;
        public IVewModel CurrentVM
        {
            get { return _currentVM; }
            set { _currentVM = value; OnPropertyChanged("CurrentVM"); }
        }

        private ViewModelBase _modalWindow;
        public ViewModelBase ModalWindow
        {
            get { return _modalWindow; }
            set
            {
                _modalWindow = value;
                OnPropertyChanged("ModalWindow");
            }
        }

        #region CollectionVMs for Buttons

        private ITasksViewModel _tasksViewModel;
        public ITasksViewModel TasksViewModel
        {
            get { return _tasksViewModel; }
            set
            {
                _tasksViewModel = value;
                OnPropertyChanged("TasksViewModel");
            }
        }

        private IEventsViewModel _eventsViewModel;
        public IEventsViewModel EventsViewModel
        {
            get { return _eventsViewModel; }
            set
            {
                _eventsViewModel = value;
                OnPropertyChanged("EventsViewModel");
            }
        }

        private INotesViewModel _notesViewModel;
        public INotesViewModel NotesViewModel
        {
            get { return _notesViewModel; }
            set
            {
                _notesViewModel = value;
                OnPropertyChanged("NotesViewModel");
            }
        }

        #endregion

        #region MessageHandlers

        private void ShowModal(Notification n)
        {
            var args = n.GetArgs<NotificationArgsItemEditModalShow>();
            ModalWindow = args.ModalViewModel;
        }

        private void CloseModal(Notification n)
        {
            ModalWindow = null;
        }

        #endregion

        #region Commands

        public void ControlVisibility(object args)
        {
            switch ((Categories)args)
            {
                case Categories.Tasks:
                    CurrentVM = TasksViewModel;
                    break;
                case Categories.Events:
                    CurrentVM = EventsViewModel;
                    break;
                case Categories.Notes:
                    CurrentVM = NotesViewModel;
                    break;
                default:
                    CurrentVM = null;
                    break;
            }
        }

        #endregion

        public void Dispose()
        {
            if (notificationCenter != null)
            {
                notificationCenter.RemoveMessageHandler(ShowModal, NotificationName.CLOSE_ITEM_EDIT_MODAL);
                notificationCenter.RemoveMessageHandler(CloseModal, NotificationName.CLOSE_ITEM_EDIT_MODAL);
            }
        }
    }

    public enum Categories
    {
        Tasks,
        Events,
        Notes,
    }

}
