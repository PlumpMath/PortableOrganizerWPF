using System;

namespace OrganizerTasks1.Model
{
    [Serializable]
    public class Event : DataModelObject
    {
        public DateTime Date { set; get; }
    }
}
