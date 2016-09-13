using System.ComponentModel;
using OrganizerTask1.UI.ViewModels.Validation;

namespace OrganizerTask1.UI.ViewModels
{
    public abstract class ViewModelBase : ValidationErrorContainer, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
