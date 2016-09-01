using System.Windows;
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
