using OrganizerTask1.UI.ViewModels.Interfaces;
using OrganizerTasks1.DAL;

namespace OrganizerTask1.UI.ViewModels
{
    class NotesViewModel : CollectionViewModel<NoteViewModel>, INotesViewModel
    {
        public NotesViewModel(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        protected override void PopulateData()
        {
            foreach (var note in _dataProvider.Notes)
            {
                base.Entities.Add(new NoteViewModel(note));
            }
        }
    }
}
