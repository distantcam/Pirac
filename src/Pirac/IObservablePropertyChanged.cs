using System;

namespace Pirac
{
    public interface IObservablePropertyChanged
    {
        IObservable<PropertyChangedData> Changed { get; }
    }
}