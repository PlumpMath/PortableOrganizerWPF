using System;
using OrganizerTasks1.Model;

namespace OrganizerTask1.UI.ViewModels
{
    public class EventViewModel : ViewModelBase
    {
        private readonly Event _event;

        public EventViewModel(Event eventData)
        {
            _event = eventData;
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
    }
}
