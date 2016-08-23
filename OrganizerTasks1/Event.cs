using System;

namespace OrganizerTasks1.Model
{
    [Serializable]
    public class Event : IDataModelObject
    {
        public string Name { get; set; }

        public DateTime Date { set; get; }
    }
}
