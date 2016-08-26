using System.Windows;
using System.Windows.Input;
using OrganizerTask1.UI.Misc;
using OrganizerTask1.UI.ViewModels.Interfaces;
using OrganizerTasks1.DAL;
using OrganizerTasks1.Model;

namespace OrganizerTask1.UI.ViewModels
{
    class NotesViewModel : CollectionViewModel<NoteViewModel, Note>, INotesViewModel
    {
        public NotesViewModel(IDataProvider dataProvider) : base(dataProvider)
        {
            NewCommand = new RelayCommand(New);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
        }

        protected override NoteViewModel CreateViewModelEntity(Note data)
        {
            return new NoteViewModel(data);
        }

        public ICommand NewCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public void New(object args)
        {
            var nNote = new Note() {Name = "New note", Description = "New note description"};
            _dataProvider.Notes.Add(nNote);
            Entities.Add(new NoteViewModel(nNote));
            InAddMode = Visibility.Visible;
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

        private Visibility _inAddMode = Visibility.Hidden;
        public Visibility InAddMode 
        {
            get
            {
                return _inAddMode;
            }
            set
            {
                _inAddMode = value;
                OnPropertyChanged("InAddMode");
            }
        }

    }
}
