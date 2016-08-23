using OrganizerTask1.UI.ViewModels.Interfaces;
using OrganizerTasks1.DAL;

namespace OrganizerTask1.UI.ViewModels
{
    public class EventsViewModel : CollectionViewModel<EventViewModel>, IEventsViewModel
    {
        public EventsViewModel(IDataProvider dataProvider)
            : base(dataProvider)
        {

        }

        protected override void PopulateData()
        {
            foreach (var eventData in _dataProvider.Events)
            {
                base.Entities.Add(new EventViewModel(eventData));
            }
        }
    }
}
