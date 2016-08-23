using AutoMapper;
using OrganizerTask1.UI.ViewModels;
using OrganizerTasks1.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Unity;
using OrganizerTask1.UI.Misc;
using OrganizerTasks1;
using OrganizerTasks1.DAL;

namespace OrganizerTask1.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IDataProvider dataProvider;

        public App()        
        {
            var bs = new BootstrapperUnity();
            dataProvider = bs.Container.Resolve<IDataProvider>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                dataProvider.Save();
            }
            finally
            {
                base.OnExit(e);
            }
        }
    }
}
