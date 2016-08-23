using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizerTasks1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrganizerTasks1.DAL;

namespace OrganizerTask1.Model.Test
{
    [TestClass]
    public class UnitTestSerializer
    {
        private string fileName = DataModel.DataPath;

        [TestMethod]
        public void TestMethodSerialize()
        {
            var model = new DataModel();
            model.Events = new List<Event>() {new Event() {Name = "My Birthday party", Date = new DateTime(2016, 01, 08) } };
            model.Notes = new List<Note>() { new Note() {Name = " WPF Organizer task"} };
            model.Tasks = new List<Task>() { new Task() { Name = "to do laundry" }, new Task() { Name = "go for shopping" } };

            DataSerializer.SerializeData(fileName, model);
        }

        [TestMethod]
        public void TestMethodDeserialize()
        {
            var model = DataSerializer.DeserializeItem(fileName);
        }
    }
}
