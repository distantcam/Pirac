using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pirac
{
    public static class DelegateCommand
    {
        public static DelegateCommand<T> Create<T>(Action<T> executeMethod) =>
            new DelegateCommand<T>(executeMethod);

        public static DelegateCommand<T> Create<T>(Action<T> executeMethod, Func<T, bool> canExecuteMethod) =>
            new DelegateCommand<T>(executeMethod, canExecuteMethod);

        public static DelegateCommand<object> Create(Action executeMethod) =>
            new DelegateCommand<object>(_ => executeMethod());

        public static DelegateCommand<object> Create(Action executeMethod, Func<bool> canExecuteMethod) =>
            new DelegateCommand<object>(_ => executeMethod(), _ => canExecuteMethod());

        public static AwaitableDelegateCommand<T> Create<T>(Func<T, Task> executeMethod) =>
            new AwaitableDelegateCommand<T>(executeMethod);

        public static AwaitableDelegateCommand<T> Create<T>(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod) =>
            new AwaitableDelegateCommand<T>(executeMethod, canExecuteMethod);

        public static AwaitableDelegateCommand<object> Create(Func<Task> executeMethod) =>
            new AwaitableDelegateCommand<object>(_ => executeMethod());

        public static AwaitableDelegateCommand<object> Create(Func<Task> executeMethod, Func<bool> canExecuteMethod) =>
            new AwaitableDelegateCommand<object>(_ => executeMethod(), _ => canExecuteMethod());
    }

    public class DelegateCommand<T> : BaseCommand<T>, ICommand
    {
        private readonly Action<T> executeMethod;

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod = null) : base(canExecuteMethod)
        {
            if (executeMethod == null)
                throw new ArgumentNullException(nameof(executeMethod), @"Execute Method cannot be null");

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