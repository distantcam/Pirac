using System;
using System.Reactive.Disposables;
using System.Threading;
using System.Windows.Input;

namespace Pirac.Commands
{
    public abstract class BaseCommand<T> : IRaiseCanExecuteChanged
    {
        private readonly Func<T, bool> canExecuteMethod;
        private SemaphoreSlim isExecuting = new SemaphoreSlim(1);

        public BaseCommand(Func<T, bool> canExecuteMethod = null)
        {
            this.canExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public bool CanExecute(T parameter)
        {
            if (isExecuting.CurrentCount == 0)
                return false;
            if (canExecuteMethod == null)
                return true;

            return canExecuteMethod(parameter);
        }

        protected IDisposable StartExecuting()
        {
            isExecuting.Wait();
            RaiseCanExecuteChanged();

            return Disposable.Create(() =>
            {
                isExecuting.Release();
                RaiseCanExecuteChanged();
            });
        }
    }
}