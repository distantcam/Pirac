using System;

namespace Pirac
{
    public interface IObservablePropertyChanging
    {
        IObservable<PropertyChangingData> Changing { get; }
    }
}