using System;
using OrganizerTasks1.Model;
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
using OrganizerTask1.UI.ViewModels;
using OrganizerTask1.UI.ViewModels.Interfaces;


namespace OrganizerTask1.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IMainWindowVM _mainWindowVm)
        {
            InitializeComponent();
            DataContext = _mainWindowVm;
        }

    }
}
