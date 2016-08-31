using System;

namespace OrganizerTasks1.Model
{
    [Serializable]
    public abstract class DataModelObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
