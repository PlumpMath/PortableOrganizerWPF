using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using OrganizerTask1.UI.Misc;
using OrganizerTask1.UI.ViewModels.Interfaces;
using OrganizerTasks1.DAL;
using OrganizerTasks1.Model;
using OrganizerTasks1.Model.Interfaces;

namespace OrganizerTask1.UI.ViewModels
{
    public abstract class CollectionViewModel<T, U> : ViewModelBase
        where T : ViewModelBase, IViewModelObject
        where U : class, IDataModelObject, new()
    {
        protected readonly IDataProvider _dataProvider;
        protected readonly NotificationCenter _notificationCenter;
        

        protected CollectionViewModel(IDataProvider dataProvider, NotificationCenter notificationCenter)
        {
            _dataProvider = dataProvider;
            _notificationCenter = notificationCenter;
            PopulateData();
            InitCommands();
            AddMessageHandlers();
        }

        private void InitCommands()
        {
            NewCommand = new RelayCommand(New);
            EditCommand = new RelayCommand(Edit, CanEdit);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
        }

        private void AddMessageHandlers()
        {
            _notificationCenter.AddMessageHandler(OnItemEdited, NotificationName.CLOSE_ITEM_EDIT_MODAL);
        }

        #region Properties

        protected ObservableCollection<T> _entities = new ObservableCollection<T>();
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

        #endregion

        protected abstract T CreateViewModelEntity(U data);

        protected void PopulateData()
        {
            foreach (var modelEntity in _dataProvider.GetCollection<U>())
            {
                _entities.Add(CreateViewModelEntity(modelEntity));
            }
        }

        private void RequestShowItemEditor(ViewModelBase itemVM)
        {
            ViewModelBase editor = new EditorViewModel(_notificationCenter) { EditingItem = itemVM };
            _notificationCenter.PostNotification(NotificationName.SHOW_ITEM_EDIT_MODAL, new NotificationArgsItemEditModalShow(editor));
        }

        #region Commands

        public ICommand NewCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public void New(object args)
        {
            var modelObj = new U() {Id = Guid.NewGuid()};
            RequestShowItemEditor(CreateViewModelEntity(modelObj));
        }

        public void Edit(object args)
        {
            IDataModelObject noteSource = SelectedEntity.Model;
            var modelObjCopy = noteSource.Clone() as U;
            RequestShowItemEditor(CreateViewModelEntity(modelObjCopy));
        }

        public bool CanEdit(object args)
        {
            return SelectedEntity != null;
        }

        public void Delete(object args)
        {
            _dataProvider.GetCollection<U>().Remove(SelectedEntity.Model as U);
            Entities.Remove(SelectedEntity);
        }

        public bool CanDelete(object args)
        {
            return SelectedEntity != null;
        }        

        #endregion

        #region Message handlers

        private void OnItemEdited(Notification n)
        {
            var args = n.GetArgs<NotificationArgsItemEditModalClose>();
            if (args == null)
                return;

            T editedItem = args.ResultEntity as T;
            if (editedItem == null)
                return;

            T editedVM = Entities.ToList().Find((en) => en.Model.Id == editedItem.Model.Id);
            if (editedVM == null)
            {
                Entities.Add(editedItem);
                _dataProvider.GetCollection<U>().Add(editedItem.Model as U);
                return;
            }

            editedVM.Update(editedItem);
        }

        #endregion

    }
}
