using System;
using System.Windows;

namespace Pirac
{
    public interface IObserveView
    {
        IObservable<object> WhenViewAttached();

        IObservable<FrameworkElement> WhenViewLoaded();

        void AttachView(object view);
    }
}