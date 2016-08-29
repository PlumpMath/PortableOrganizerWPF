using System.Collections.ObjectModel;
using OrganizerTasks1.DAL;

namespace OrganizerTask1.UI.ViewModels
{
    public abstract class CollectionViewModel<T, U> : ViewModelBase where T : class
    {
        protected readonly IDataProvider _dataProvider;
        protected ObservableCollection<T> _entities = new ObservableCollection<T>();

        public CollectionViewModel(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
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

        private EditorViewModel _editor;
        public EditorViewModel Editor
        {
            get { return _editor; }
            set
            {
                _editor = value;
                OnPropertyChanged("Editor");
            }
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
    }
}
