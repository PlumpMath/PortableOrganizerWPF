using System;
using System.Windows.Input;
using OrganizerTask1.UI.Misc;
using OrganizerTask1.UI.ViewModels.Interfaces;

namespace OrganizerTask1.UI.ViewModels
{
    public class EditorViewModel : ViewModelBase, IVewModel
    {
        public EditorViewModel()
        {
             _close = new RelayCommand((o) =>
                {
                    var handler = CloseRequested;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                });
        }

        private ViewModelBase _editingItem;
        public ViewModelBase EditingItem
        {
            get { return _editingItem; }
            set
            {
                _editingItem = value;
                OnPropertyChanged("EditingItem");
            }
        }

        private readonly ICommand _close;
        public ICommand Close
        {
            get { return _close; }
        }

        public event EventHandler CloseRequested;
        public event EventHandler SaveRequested;
    }
}
