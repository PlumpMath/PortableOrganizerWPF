using System;

namespace OrganizerTasks1.Model
{
    [Serializable]
    public class Note : DataModelObject
    {
        public string Description { get; set; }

        protected override void HandleCloned(DataModelObject clone)
        {
            base.HandleCloned(clone);

            Note obj = (Note)clone;
            if (Description != null)
                obj.Description = string.Copy(this.Description);
        }
    }
}
