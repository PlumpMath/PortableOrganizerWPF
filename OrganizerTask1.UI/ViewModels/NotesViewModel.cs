using System.Windows.Input;
using OrganizerTask1.UI.Misc;
using OrganizerTask1.UI.ViewModels.Interfaces;
using OrganizerTasks1.DAL;
using OrganizerTasks1.Model;

namespace OrganizerTask1.UI.ViewModels
{
    class NotesViewModel : CollectionViewModel<NoteViewModel>, INotesViewModel
    {
        public NotesViewModel(IDataProvider dataProvider) : base(dataProvider)
        {
            NewCommand = new RelayCommand(New);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
        }

        protected override void PopulateData()
        {
            foreach (var note in _dataProvider.Notes)
            {
                base.Entities.Add(new NoteViewModel(note));
            }
        }

        public ICommand NewCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public void New(object args)
        {
            var nNote = new Note() {Name = "New note", Description = "New note description"};
            _dataProvider.Notes.Add(nNote);
            Entities.Add(new NoteViewModel(nNote));
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
