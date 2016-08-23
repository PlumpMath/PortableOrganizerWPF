using OrganizerTasks1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrganizerTask1.UI.ViewModels
{
    public class TaskViewModel : ViewModelBase
    {
        private readonly Task _task;
        public TaskViewModel(Task taskData)
        {
            _task = taskData;
        }

        public string Name
        {
            get { return _task.Name; }
            set
            {
                _task.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get { return _task.Description; }
            set
            {
                _task.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public TaskStatus Status
        {
            get { return _task.Status; }
            set
            {
                _task.Status = value;
                OnPropertyChanged("Status");
            }
        }
    }
}
