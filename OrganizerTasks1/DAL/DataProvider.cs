using System;
using System.Collections.Generic;
using OrganizerTasks1.Model;
using Task = OrganizerTasks1.Model.Task;

namespace OrganizerTasks1.DAL
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

        public IList<T> GetCollection<T>()
        {
            var typeT = typeof(T);

            if (typeof(Task) == typeT)
            {
                return Tasks as IList<T>;
            }

            if (typeof(Event) == typeT)
            {
                return Events as IList<T>;
            }

            if (typeof(Note) == typeT)
            {
                return Notes as IList<T>;
            }

            throw new NotImplementedException(string.Format("{0} is not supported type", typeT.Name));
        }

        public void Save()
        {
            _dataModel.Save();
        }
    }
}
