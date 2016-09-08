using System.Windows.Input;

namespace OrganizerTask1.UI.ViewModels.Interfaces
{
    public interface IItemEditorVM
    {
        ICommand CloseCommand { get; }
        ICommand SaveCommand { get; }
        ViewModelBase EditingItem { get; set; }
    }
}
