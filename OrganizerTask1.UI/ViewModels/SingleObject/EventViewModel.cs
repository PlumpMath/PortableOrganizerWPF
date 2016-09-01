using System;
using OrganizerTask1.UI.ViewModels.Interfaces;
using OrganizerTasks1.Model;
using OrganizerTasks1.Model.Interfaces;

namespace OrganizerTask1.UI.ViewModels
{
    public class EventViewModel : ViewModelBase, IViewModelObject
    {
        private readonly Event _event;
        public IDataModelObject Model { get; private set; }

        public EventViewModel(Event eventData)
        {
            _event = eventData;
            Model = eventData;
        }

        public string Name
        {
            get { return _event.Name; }
            set
            {
                _event.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public DateTime Date
        {
            get { return _event.Date; }
            set
            {
                _event.Date = value;
                OnPropertyChanged("Date");
            }
        }

        public void Update(IViewModelObject sourceInstance)
        {
            EventViewModel source = sourceInstance as EventViewModel;
            if (source == null)
                throw new ArgumentNullException("sourceInstance");
            Name = source.Name;
            Date = source.Date;
        }
    }
}
