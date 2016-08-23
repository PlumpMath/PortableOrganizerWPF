using System.Windows.Input;
using OrganizerTask1.UI.Misc;
using OrganizerTask1.UI.ViewModels.Interfaces;
using OrganizerTasks1.DAL;
using TaskStatus = OrganizerTasks1.Model.TaskStatus;

namespace OrganizerTask1.UI.ViewModels
{
    public class TasksViewModel : CollectionViewModel<TaskViewModel>, ITasksViewModel
    {
        public TasksViewModel(IDataProvider dataProvider)
            : base(dataProvider)
        {
            CloseTaskCommand = new RelayCommand(CloseTask, CanCloseTask);
        }

        protected override void PopulateData()
        {
            foreach (var task in _dataProvider.Tasks)
            {
                base.Entities.Add(new TaskViewModel(task));
            }
        }

        #region Commands

        public ICommand CloseTaskCommand { get; set; }

        public bool CanCloseTask(object args)
        {
            return SelectedEntity != null && SelectedEntity.Status != TaskStatus.Closed;
        }

        public void CloseTask(object args)
        {
            SelectedEntity.Status = TaskStatus.Closed;
        }

        #endregion


    }
}
