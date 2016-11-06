using System;
using System.Windows;

namespace Pirac
{
    public interface IObserveView
    {
        IObservable<object> ViewAttached { get; }

        IObservable<FrameworkElement> ViewLoaded { get; }

        void AttachView(object view);
    }
}