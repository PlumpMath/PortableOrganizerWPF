using System;
using System.Linq;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.Utility;

namespace OrganizerTask1.VMUnitTest.Base
{
    public class Windows
    {
        private Application application;

        public void Init(Application app)
        {
            application = app;
        }
        
        public MainWindow MainWindow
        {
            get
            {
                Window win = GetWindow("Oraganizer Task");
                return new MainWindow(win);
            }
        }

        private Window GetWindow(string title)
        {
            return Retry.For(
                () => application.GetWindows().First(x => x.Title.Contains(title)),
                TimeSpan.FromSeconds(5));
        }
    }
}
