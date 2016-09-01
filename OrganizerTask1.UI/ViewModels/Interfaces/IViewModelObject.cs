using OrganizerTasks1.Model.Interfaces;

namespace OrganizerTask1.UI.ViewModels.Interfaces
{
    public interface IViewModelObject
    {
        IDataModelObject Model { get; }
        void Update(IViewModelObject sourceInstance);
    }
}
