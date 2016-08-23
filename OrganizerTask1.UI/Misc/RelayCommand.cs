using System;
using System.Windows.Input;

namespace OrganizerTask1.UI.Misc
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _methodToExecute;
        private readonly Predicate<object> _canExecuteEvaluator;

        public RelayCommand(Action<object> methodToExecute, Predicate<object> canExecuteEvaluator)
        {
            this._methodToExecute = methodToExecute;
            this._canExecuteEvaluator = canExecuteEvaluator;
        }

        public RelayCommand(Action<object> methodToExecute)
            : this(methodToExecute, null)
        {
        }

        public bool CanExecute(object parameter)
        {
            if (this._canExecuteEvaluator == null)
            {
                return true;
            }
            else
            {
                bool result = this._canExecuteEvaluator.Invoke(parameter);
                return result;
            }
        }
        public void Execute(object parameter)
        {
            this._methodToExecute.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
