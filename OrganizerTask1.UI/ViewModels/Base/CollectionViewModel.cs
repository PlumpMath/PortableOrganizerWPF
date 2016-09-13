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
        protected readonly INotificationCenter _notificationCenter;


        protected CollectionViewModel(IDataProvider dataProvider, INotificationCenter notificationCenter)
        {
            _dataProvider = dataProvider;
            _notificationCenter = notificationCenter;
            PopulateData();
            InitCommands();
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
            _notificationCenter.AddMessageHandler(OnItemValidateRequest, NotificationName.SAVE_ITEM_VALIDATE_REQUEST);
        }

        private void RemoveMessageHandlers()
        {
            _notificationCenter.RemoveMessageHandler(OnItemEdited, NotificationName.CLOSE_ITEM_EDIT_MODAL);
            _notificationCenter.RemoveMessageHandler(OnItemValidateRequest, NotificationName.SAVE_ITEM_VALIDATE_REQUEST);
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

            AddMessageHandlers();
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

        protected T FindEntity(Guid id)
        {
            return Entities.ToList().Find((en) => en.Model.Id == id);
        }

        protected T FindEntity(string name)
        {
            return Entities.ToList().Find((en) => en.Model.Name == name);
        }

        private void OnItemEdited(Notification n)
        {
            var args = n.GetArgs<NotificationArgsItemEditModalClose>();
            if (args == null)
                return;

            T editedItem = args.ResultEntity as T;
            if (editedItem == null)
                return;

            T editedVM = FindEntity(editedItem.Model.Id);
            if (editedVM == null)
            {
                Entities.Add(editedItem);
                _dataProvider.GetCollection<U>().Add(editedItem.Model as U);
                return;
            }

            editedVM.Update(editedItem);

            RemoveMessageHandlers();
        }

        private void OnItemValidateRequest(Notification n)
        {
            var args = n.GetArgs<NotificationArgsValidateItemSaveRequest>();
            T vmItem = args.ItemEntity as T;

            if (vmItem == null)
                return;

            if (FindEntity(vmItem.Model.Id) == null)
            {
                if (FindEntity(vmItem.Model.Name) != null)
                {
                    _notificationCenter.PostNotification(NotificationName.SAVE_ITEM_VALIDATE_RESPONSE,
                        new NotificationArgsValidateItemSaveResponse(string.Format("not unique name: {0}! Please, select other name.", vmItem.Model.Name)));
                    return;
                }
            }
            _notificationCenter.PostNotification(NotificationName.SAVE_ITEM_VALIDATE_RESPONSE, new NotificationArgsValidateItemSaveResponse(""));
        }

        #endregion

    }
}
