using System;
using System.Reactive;

namespace Pirac
{
    public interface IObserveActivation : IObserveClose
    {
        IObservable<Unit> Initialized { get; }
        IObservable<bool> Activated { get; }
        IObservable<bool> Deactivated { get; }

        void Activate();

        void Deactivate(bool close);

        bool CanCloseAll();
    }
}