using System;

namespace OrganizerTasks1.Model
{
    [Serializable]
    public class Task : DataModelObject
    {
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
    }

    [Serializable]
    public enum TaskStatus
    { 
        New,
        InProgress,
        Closed
    }
}
