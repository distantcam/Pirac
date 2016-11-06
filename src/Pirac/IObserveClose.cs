using System;

namespace Pirac
{
    public interface IObserveClose
    {
        Func<bool> CanCloseCheck { get; set; }
    }
}