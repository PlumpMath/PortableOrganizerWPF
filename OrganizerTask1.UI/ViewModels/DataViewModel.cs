using System.Windows.Input;
using OrganizerTask1.UI.Misc;
using OrganizerTask1.UI.ViewModels.Interfaces;

namespace OrganizerTask1.UI.ViewModels
{
    public class DataViewModel : ViewModelBase, IMainWindowVM
    {
        public ICommand SetControlVisibility { get; set; }

        public DataViewModel(ITasksViewModel tasksViewModel, INotesViewModel notesViewModel, IEventsViewModel eventsViewModel)
        {
            TasksViewModel = tasksViewModel;
            NotesViewModel = notesViewModel;
            EventsViewModel = eventsViewModel;

            SetControlVisibility = new RelayCommand(ControlVisibility);
        }

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

        private IVewModel _currentVM;
        public IVewModel CurrentVM
        {
            get { return _currentVM; }
            set { _currentVM = value; OnPropertyChanged("CurrentVM"); }
        }

        private string _visibleControl = "Tasks";
        public string VisibleControl
        {
            get { return _visibleControl; }
            set 
            {
                _visibleControl = value;
                OnPropertyChanged("VisibleControl");
            }
        }

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
    }

    public enum Categories
    {
        Tasks,
        Events,
        Notes,
    }

}
