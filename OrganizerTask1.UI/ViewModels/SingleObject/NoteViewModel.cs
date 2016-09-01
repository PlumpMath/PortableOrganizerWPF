using System;
using OrganizerTask1.UI.ViewModels.Interfaces;
using OrganizerTasks1.Model;
using OrganizerTasks1.Model.Interfaces;

namespace OrganizerTask1.UI.ViewModels
{
    public class NoteViewModel : ViewModelBase, IViewModelObject
    {
        private readonly Note _note;
        public IDataModelObject Model { get; private set; }

        public NoteViewModel(Note note)
        {
            _note = note;
            Model = note;
        }

        public Note NoteModel
        {
            get { return _note;}
        }

        public string Name
        {
            get { return _note.Name; }
            set
            {
                _note.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get { return _note.Description; }
            set
            {
                _note.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public void Update(IViewModelObject sourceInstance)
        {
            NoteViewModel source = sourceInstance as NoteViewModel;
            if (source == null)
                throw new ArgumentNullException("sourceInstance");
            Name = source.Name;
            Description = source.Description;
        }
    }
}
