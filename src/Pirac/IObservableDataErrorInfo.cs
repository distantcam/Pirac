using System;

namespace Pirac
{
    public interface IObservableDataErrorInfo
    {
        IObservable<DataErrorChanged> ErrorsChanged { get; }
    }
}