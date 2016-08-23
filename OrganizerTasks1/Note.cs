using System;

namespace OrganizerTasks1.Model
{
    [Serializable]
    public class Note : IDataModelObject
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
