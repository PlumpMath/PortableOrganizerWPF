using System;

namespace OrganizerTasks1.Model
{
    [Serializable]
    public class Task : DataModelObject
    {
        public string Description { get; set; }
        public TaskStatus Status { get; set; }

        protected override void HandleCloned(DataModelObject clone)
        {
            base.HandleCloned(clone);

            Task obj = (Task)clone;
            obj.Description = string.Copy(this.Description);
        }
    }

    [Serializable]
    public enum TaskStatus
    { 
        New,
        InProgress,
        Closed
    }
}
