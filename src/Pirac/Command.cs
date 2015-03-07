using System;
using System.Threading.Tasks;
using Pirac.Internal;

namespace Pirac
{
    public static class Command
    {
        public static ICommand Create(Action executeMethod) =>
            new DelegateCommand(_ => executeMethod());

        public static ICommand Create(Action executeMethod, Func<bool> canExecuteMethod) =>
            new DelegateCommand(_ => executeMethod(), _ => canExecuteMethod());

        public static ICommand<T> Create<T>(Action<T> executeMethod) =>
            new DelegateCommand<T>(executeMethod);

        public static ICommand<T> Create<T>(Action<T> executeMethod, Func<T, bool> canExecuteMethod) =>
            new DelegateCommand<T>(executeMethod, canExecuteMethod);

        public static IAsyncCommand Create(Func<Task> executeMethod) =>
            new AwaitableDelegateCommand(_ => executeMethod());

        public static IAsyncCommand Create(Func<Task> executeMethod, Func<bool> canExecuteMethod) =>
            new AwaitableDelegateCommand(_ => executeMethod(), _ => canExecuteMethod());

        public static IAsyncCommand<T> Create<T>(Func<T, Task> executeMethod) =>
            new AwaitableDelegateCommand<T>(executeMethod);

        public static IAsyncCommand<T> Create<T>(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod) =>
            new AwaitableDelegateCommand<T>(executeMethod, canExecuteMethod);
    }
}