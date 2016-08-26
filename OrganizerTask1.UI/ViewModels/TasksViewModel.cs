using System.Windows.Input;
using OrganizerTask1.UI.Misc;
using OrganizerTask1.UI.ViewModels.Interfaces;
using OrganizerTasks1.DAL;
using OrganizerTasks1.Model;
using TaskStatus = OrganizerTasks1.Model.TaskStatus;

namespace OrganizerTask1.UI.ViewModels
{
    public class TasksViewModel : CollectionViewModel<TaskViewModel, Task>, ITasksViewModel
    {
        public TasksViewModel(IDataProvider dataProvider)
            : base(dataProvider)
        {
            CloseTaskCommand = new RelayCommand(CloseTask, CanCloseTask);
        }

        protected override TaskViewModel CreateViewModelEntity(Task data)
        {
            return new TaskViewModel(data);
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
