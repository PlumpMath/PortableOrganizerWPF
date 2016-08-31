using System;

namespace OrganizerTasks1.Model
{
    [Serializable]
    public class Note : DataModelObject
    {
        public string Description { get; set; }
    }
}
