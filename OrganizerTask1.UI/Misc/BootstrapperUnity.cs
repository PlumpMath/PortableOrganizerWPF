using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using OrganizerTask1.UI.ViewModels;
using OrganizerTask1.UI.ViewModels.Interfaces;
using OrganizerTasks1;
using OrganizerTasks1.DAL;

namespace OrganizerTask1.UI.Misc
{
    public class BootstrapperUnity
    {
        private UnityContainer _container;

        public BootstrapperUnity()
        {
            _container = new UnityContainer();
            Configure();
            Run();
        }

        public UnityContainer Container
        {
            get { return _container; }
        }

        private void Configure()
        {
            _container.RegisterType<IMainWindowVM, DataViewModel>();
            _container.RegisterType<IDataProvider, DataProvider>(new ContainerControlledLifetimeManager(), new InjectionFactory(x => new DataProvider()));
            _container.RegisterType<ITasksViewModel, TasksViewModel>();
            _container.RegisterType<IEventsViewModel, EventsViewModel>();
            _container.RegisterType<INotesViewModel, NotesViewModel>();
        }

        private void Run()
        {
            _container.Resolve<MainWindow>().Show();
        }

    }
}
