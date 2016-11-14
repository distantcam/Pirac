using System;
using System.Reactive;

namespace Pirac
{
    public interface IObserveClose
    {
        IObservable<Unit> WhenClosed();

        Func<bool> CanCloseCheck { get; set; }
    }
}