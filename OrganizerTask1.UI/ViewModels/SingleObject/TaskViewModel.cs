using System;
using System.Collections.Generic;
using OrganizerTask1.UI.ViewModels.Interfaces;
using OrganizerTasks1.Model;
using OrganizerTasks1.Model.Interfaces;

namespace OrganizerTask1.UI.ViewModels
{
    public class TaskViewModel : ViewModelBase, IViewModelObject
    {
        private readonly Task _task;
        public IDataModelObject Model { get; private set; }

        public TaskViewModel(Task taskData)
        {
            _task = taskData;
            Model = _task;
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

        public void Update(IViewModelObject sourceInstance)
        {
            TaskViewModel source = sourceInstance as TaskViewModel;
            if (source == null)
                throw new ArgumentNullException("sourceInstance");
            Name = source.Name;
            Description = source.Description;
            Status = source.Status;
        }

        public List<ComboBoxItemTaskStatus> TaskStatusesList
        {
            get
            {
                return new List<ComboBoxItemTaskStatus>
                {
                    new ComboBoxItemTaskStatus {EnumStatusValue = TaskStatus.New, StatusStringValue = "New"},
                    new ComboBoxItemTaskStatus {EnumStatusValue = TaskStatus.InProgress, StatusStringValue = "In progress"},
                    new ComboBoxItemTaskStatus {EnumStatusValue = TaskStatus.Closed, StatusStringValue = "Closed"},
                };
            }
        }
    }

    public class ComboBoxItemTaskStatus
    {
        public TaskStatus EnumStatusValue { get; set; }
        public string StatusStringValue { get; set; }
    }
}
