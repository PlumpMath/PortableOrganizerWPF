using System;
using OrganizerTasks1.Model.Interfaces;

namespace OrganizerTasks1.Model
{
    [Serializable]
    public abstract class DataModelObject : IDataModelObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public object Clone()
        {
            var clone = (DataModelObject) this.MemberwiseClone();
            if (!string.IsNullOrEmpty(Name))
                clone.Name = string.Copy(Name);
            HandleCloned(clone);
            return clone;
        }

        protected virtual void HandleCloned(DataModelObject clone)
        {
        }

    }
}
