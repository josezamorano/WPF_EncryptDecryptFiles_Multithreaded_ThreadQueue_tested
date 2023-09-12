using System;
using System.Windows.Input;

namespace PresentationLayer.MVVM.Base
{
    public class CommandBaseViewModel : ICommand
    {
        //Fields
        private readonly Action<object> _executeAction;
        private readonly Predicate<object> _canExecuteAction;

        public CommandBaseViewModel( Action<object> executeAction)
        {
            _executeAction = executeAction;
        }

        public CommandBaseViewModel(Action<object> executeAction, Predicate<object> canExecuteAction )
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
            
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value;}
        }

        public bool CanExecute(object? parameter)
        {
            var result = (_canExecuteAction == null) ? true : _canExecuteAction(parameter);
            return result;
        }

       
        public void Execute(object? parameter)
        {
            _executeAction(parameter);
        }
    }
}
