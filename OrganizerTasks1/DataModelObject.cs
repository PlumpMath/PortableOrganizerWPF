using System;

namespace OrganizerTasks1.Model
{
    [Serializable]
    public abstract class DataModelObject
    {
        public Guid Id { get; protected set; }
        public string Name { get; set; }

        protected DataModelObject()
        {
            Id = Guid.NewGuid();
        }
    }
}
