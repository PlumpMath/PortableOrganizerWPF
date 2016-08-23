using OrganizerTask1.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OrganizerTask1.UI.Views
{
    /// <summary>
    /// Interaction logic for TasksUserControl.xaml
    /// </summary>
    public partial class TasksUserControl : UserControl
    {
        public TasksUserControl()
        {
            InitializeComponent();
        }

        //private void buttonCloseTask_Click(object sender, RoutedEventArgs e)
        //{
        //    var task = dataGridTasks.SelectedEntity as TaskViewModel;
        //    if (task == null)
        //        return;
        //    task.Status = OrganizerTasks1.Model.TaskStatus.Closed;
        //}
    }
}
