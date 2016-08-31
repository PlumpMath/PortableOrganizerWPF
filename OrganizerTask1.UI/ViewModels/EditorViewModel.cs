using System;
using System.Windows.Input;
using OrganizerTask1.UI.Misc;
using OrganizerTask1.UI.ViewModels.Interfaces;

namespace OrganizerTask1.UI.ViewModels
{
    public class EditorViewModel : ViewModelBase
    {
        public event EventHandler CloseRequested;
        public event EventHandler<object> SaveRequested;

        public EditorViewModel()
        {
            _closeCommand = new RelayCommand(Close);
            _saveCommand = new RelayCommand(Save);
        }

        private ViewModelBase _editingItem;
        public ViewModelBase EditingItem 
        {
            get { return _editingItem;}
            set { _editingItem = value; }
        }

        public void SetEditData<TData>(TData inData) where TData : ViewModelBase
        {
            _editingItem = inData;
        }

        private readonly ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get { return _closeCommand; }
        }

        private void Close(object args)
        {
            var handler = CloseRequested;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private readonly ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand; }
        }

        private void Save(object args)
        {
            var handler = SaveRequested;
            if (handler != null)
                handler(this, args);
        }
    }

}
