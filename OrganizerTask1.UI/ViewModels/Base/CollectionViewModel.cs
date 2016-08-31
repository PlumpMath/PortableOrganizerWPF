using System.Collections.ObjectModel;
using OrganizerTask1.UI.Misc;
using OrganizerTask1.UI.ViewModels.Interfaces;
using OrganizerTasks1.DAL;

namespace OrganizerTask1.UI.ViewModels
{
    public abstract class CollectionViewModel<T, U> : ViewModelBase where T : class
    {
        protected readonly IDataProvider _dataProvider;
        protected readonly NotificationCenter _notificationCenter;
        protected ObservableCollection<T> _entities = new ObservableCollection<T>();

        protected CollectionViewModel(IDataProvider dataProvider, NotificationCenter notificationCenter)
        {
            _dataProvider = dataProvider;
            _notificationCenter = notificationCenter;
            PopulateData();
        }

        protected virtual void PopulateData()
        {
            foreach (var modelEntity in _dataProvider.GetCollection<U>())
            {
                _entities.Add(CreateViewModelEntity(modelEntity));
            }
        }

        protected abstract T CreateViewModelEntity(U data);

        public ObservableCollection<T> Entities
        {
            get { return _entities; }
        }

        protected T _selectedEntity;
        public T SelectedEntity
        {
            get { return _selectedEntity; }
            set
            {
                _selectedEntity = value;
                OnPropertyChanged("SelectedEntity");
            }
        }

        //protected abstract void RequestEditor<T>(T entityVM);
    }
}
