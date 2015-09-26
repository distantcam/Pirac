using System;
using Pirac.Commands;

namespace Pirac
{
    public abstract class AbstractCommand<T> : BaseCommand<T>, ICommand<T>
    {
        public AbstractCommand(Func<T, bool> canExecuteMethod = null) : base(canExecuteMethod)
        {
        }

        public abstract void Execute(T obj);

        bool System.Windows.Input.ICommand.CanExecute(object parameter) => CanExecute((T)parameter);

        void ICommand<T>.Execute(T obj)
        {
            using (StartExecuting())
            {
                Execute(obj);
            }
        }

        void System.Windows.Input.ICommand.Execute(object parameter)
        {
            ((ICommand<T>)this).Execute((T)parameter);
        }
    }
}