using System.Collections.Generic;
using OrganizerTasks1;
using OrganizerTasks1.DAL;

namespace OrganizerTask1.UI.ViewModels
{
    public abstract class CollectionViewModel<T> : ViewModelBase where T: class
    {
        protected readonly IDataProvider _dataProvider;
        protected IList<T> _entities = new List<T>();

        public CollectionViewModel(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            PopulateData();
        }

        protected abstract void PopulateData();

        public IList<T> Entities
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
    }
}
