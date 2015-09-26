using System;
using System.Windows.Input;

namespace Pirac.Commands
{
    internal class DelegateCommand : DelegateCommand<object>, ICommand
    {
        public DelegateCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod = null) : base(executeMethod, canExecuteMethod)
        {
        }
    }

    internal class DelegateCommand<T> : BaseCommand<T>, ICommand<T>
    {
        private readonly Action<T> executeMethod;

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod = null) : base(canExecuteMethod)
        {
            if (executeMethod == null)
                throw new ArgumentNullException(nameof(executeMethod), $"{nameof(executeMethod)} is null.");

            this.executeMethod = executeMethod;
        }

        bool ICommand.CanExecute(object parameter) => CanExecute((T)parameter);

        void ICommand.Execute(object parameter) => Execute((T)parameter);

        public void Execute(T parameter)
        {
            using (StartExecuting())
            {
                executeMethod(parameter);
            }
        }
    }
}