using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pirac.Commands
{
    class ObservableCommand : IObservableCommand, IRaiseCanExecuteChanged
    {
        private Func<object, Task> action;
        private IDisposable canExecuteSubscription;
        private bool latest;
        private bool isExecuting;

        public ObservableCommand(IObservable<bool> canExecuteObservable, Func<object, Task> action)
        {
            this.action = action;

            canExecuteSubscription = canExecuteObservable
                .Distinct()
                .ObserveOnPiracMain()
                .Subscribe(b =>
                {
                    latest = b;
                    RaiseCanExecuteChanged();
                });
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => !isExecuting && latest;

        public async void Execute(object parameter) => await ExecuteAsync(parameter);

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            Interlocked.Exchange(ref canExecuteSubscription, null)?.Dispose();
        }

        private async Task ExecuteAsync(object parameter)
        {
            using (StartExecuting())
                await action(parameter);
        }

        private IDisposable StartExecuting()
        {
            isExecuting = true;
            RaiseCanExecuteChanged();

            return Disposable.Create(() =>
            {
                isExecuting = false;
                RaiseCanExecuteChanged();
            });
        }
    }
}