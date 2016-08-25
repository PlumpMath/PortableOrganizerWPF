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
            Notes = new List<Note>()
            {
                new Note() { Name = "New note name goes here", Description = "New note description" },
                new Note() { Name = "Make soup", Description = "Some day Im gonna make soup" },
                new Note() { Name = "Another note", Description = "A humorous out-loud reminder/aside meant to indicate your future plans or a " +
                                                                  "lesson you have just learned, dependent on the disparity between one's actions and one's inner thoughts. " +
                                                                  "Originated with Norm McDonald's into-the-mini-cassette-recorder bits between jokes on Saturday Night Live." },
                
            };
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
