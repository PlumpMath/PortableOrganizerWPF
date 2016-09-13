using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using OrganizerTask1.UI.ViewModels.Interfaces;
using OrganizerTask1.UI.ViewModels.Validation;
using OrganizerTasks1.Model;
using OrganizerTasks1.Model.Interfaces;

namespace OrganizerTask1.UI.ViewModels
{
    public class NoteViewModel : ViewModelBase, IViewModelObject
    {
        private readonly Note _note;
        public IDataModelObject Model { get; private set; }

        public NoteViewModel(Note note)
        {
            _note = note;
            Model = note;
            //ErrorsChanged += ErrorsChangedHandler;
            ValidateProperty("Name");
        }

        public Note NoteModel
        {
            get { return _note;}
        }

        public string Name
        {
            get { return _note.Name; }
            set
            {
                _note.Name = value;
                OnPropertyChanged("Name");
                ValidateProperty("Name");
            }
        }

        public string Description
        {
            get { return _note.Description; }
            set
            {
                _note.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public void Update(IViewModelObject sourceInstance)
        {
            NoteViewModel source = sourceInstance as NoteViewModel;
            if (source == null)
                throw new ArgumentNullException("sourceInstance");
            Name = source.Name;
            Description = source.Description;
        }

        #region Input Validation

        void ValidateMandatory(string value, string fieldName)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                AddError(new ValidationError(fieldName, "IsMandatory", "*" + fieldName + ": is mandatory"));
            else
                RemoveError(fieldName, "IsMandatory");
        }
        
        //private void ErrorsChangedHandler(object sender, DataErrorsChangedEventArgs e)
        //{
        //    // implemet in case you use error bar or such for displaying errors
        //}

        public void ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case "Name":
                {
                    ValidateMandatory(Name, "Name");
                }
                break;
            }
        }
        #endregion
    }
}
