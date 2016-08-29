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
        public ICommand SaveCommand { get; set; }
        public ICommand CloseAddViewCommand { get; set; }

        public void New(object args)
        {
            Editor = new EditorViewModel {EditingItem = new NoteViewModel(new Note())};
            //var nNote = new Note();
            ////_dataProvider.Notes.Add(nNote);
            ////Entities.Add(new NoteViewModel(nNote));
            //SelectedEntity = new NoteViewModel(nNote);
            //CurMode = CurrentMode.Add;
        }

        public void Save(object args)
        {
            _dataProvider.Notes.Add(SelectedEntity.NoteModel);
            Entities.Add(SelectedEntity);
            SelectedEntity = null;

            CurMode = CurrentMode.Add;
        }

        public bool CanSave(object args)
        {
            return SelectedEntity != null && !string.IsNullOrEmpty(SelectedEntity.Name) && !string.IsNullOrEmpty(SelectedEntity.Description);
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

        private CurrentMode _currentMode = CurrentMode.Normal;
        public CurrentMode CurMode
        {
            get { return _currentMode; }
            set
            {
                _currentMode = value;
                OnPropertyChanged("CurMode");
            }
        }

        public enum CurrentMode
        {
            Add,
            Edit,
            Normal
        }

    }
}
