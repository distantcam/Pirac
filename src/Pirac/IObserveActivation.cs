using System;
using System.Reactive;

namespace Pirac
{
    public interface IObserveActivation : IObserveClose
    {
        IObservable<Unit> WhenInitialized();

        IObservable<bool> WhenActivated();

        IObservable<bool> WhenDeactivated();

        void Activate();

        void Deactivate(bool close);

        bool CanCloseAll();
    }
}