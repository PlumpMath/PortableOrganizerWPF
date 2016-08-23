using System;
using System.Collections.Generic;
using System.IO;
using OrganizerTasks1.DAL;

namespace OrganizerTasks1.Model
{
    [Serializable]
    public class DataModel
    {
        public static string DataPath = "organizer.dat";

        public IList<Task> Tasks { get; set; }
        public IList<Note> Notes { get; set; }
        public IList<Event> Events { get; set; }

        public DataModel()
        {
            Tasks = new List<Task>() { new Task() { Name = "Enter new task name here", Description = "Enter task description" } };
            Notes = new List<Note>() { new Note() { Name = "New naote name goes here", Description = "New note description" } };
            Events = new List<Event>() { new Event() {Name = "My Birthday", Date = new DateTime(2016, 1, 8, 14, 10, 10)}};

        }

        public static DataModel Load()
        {
            if (File.Exists(DataPath))
            {
                return DataSerializer.DeserializeItem(DataPath);
            }
            return new DataModel();
        }

        public void Save()
        {
            DataSerializer.SerializeData(DataPath, this);
        }
    }
}
