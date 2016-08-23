using System.Collections.Generic;
using OrganizerTasks1.Model;

namespace OrganizerTasks1.DAL
{
    public interface IDataProvider
    {
        IList<Task> Tasks { get; set; }
        IList<Event> Events { get; set; }
        IList<Note> Notes { get; set; }

        void Save();
    }
}
