using System;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public class BaseCommand : ICommand
    {
        private readonly Action targetExecuteMethod;
        private readonly Func<bool> targetCanExecuteMethod;

        public BaseCommand(Action executeMethod)
        {
            targetExecuteMethod = executeMethod;
        }

        public BaseCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            targetExecuteMethod = executeMethod;
            targetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (targetCanExecuteMethod != null)
            {
                return targetCanExecuteMethod();
            }

            if (targetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            targetExecuteMethod?.Invoke();
        }
    }
}
