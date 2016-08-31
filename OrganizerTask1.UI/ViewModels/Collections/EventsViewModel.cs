using OrganizerTask1.UI.Misc;
using OrganizerTask1.UI.ViewModels.Interfaces;
using OrganizerTasks1.DAL;
using OrganizerTasks1.Model;

namespace OrganizerTask1.UI.ViewModels
{
    public class EventsViewModel : CollectionViewModel<EventViewModel, Event>, IEventsViewModel
    {
        public EventsViewModel(IDataProvider dataProvider, NotificationCenter notificationCenter)
            : base(dataProvider, notificationCenter)
        {

        }

        protected override EventViewModel CreateViewModelEntity(Event data)
        {
            return new EventViewModel(data);
        }
    }
}
