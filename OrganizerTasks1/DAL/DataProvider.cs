using System.Collections.Generic;
using OrganizerTasks1.DAL;
using OrganizerTasks1.Model;
using Task = OrganizerTasks1.Model.Task;

namespace OrganizerTasks1
{
    public class DataProvider : IDataProvider
    {
        private readonly DataModel _dataModel;

        public DataProvider()
        {
            _dataModel = DataModel.Load();
        }

        public IList<Task> Tasks
        {
            get { return _dataModel.Tasks; }
            set { _dataModel.Tasks = value; }
        }

        public IList<Event> Events
        {
            get { return _dataModel.Events; }
            set { _dataModel.Events = value; }
        }

        public IList<Note> Notes
        {
            get { return _dataModel.Notes; }
            set { _dataModel.Notes = value; }
        }

        //TODO:finish and refactor
        public IList<T> Convert<T, U>() 
            where U: class, IDataModelObject 
            where T : class
        {
            var list = new List<T>();

            var typeIn = typeof(U);
            if (typeIn == typeof(Task))
            {
                foreach (var task in Tasks)
                {
                    //list.Add(new T(task));
                }
            }
            else if (typeIn == typeof(Event))
            {
            }
            else if (typeIn == typeof(Note))
            {
                
            }

            return list;
        }

        public void Save()
        {
            _dataModel.Save();
        }
    }
}
