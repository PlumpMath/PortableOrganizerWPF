using System;

namespace OrganizerTasks1.Model.Interfaces
{
    public interface IDataModelObject : ICloneable
    {
        Guid Id { get; set; }
    }
}
