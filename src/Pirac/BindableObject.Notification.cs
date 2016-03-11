using System;
using System.ComponentModel;
using System.Threading;

namespace Pirac
{
    partial class BindableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private PropertyChangedEventHandler propertyChanged;
        private PropertyChangingEventHandler propertyChanging;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                PropertyChangedEventHandler handler2;
                var newEvent = propertyChanged;
                do
                {
                    handler2 = newEvent;
                    var handler3 = (PropertyChangedEventHandler)Delegate.Combine(handler2, value);
                    Interlocked.CompareExchange(ref propertyChanged, handler3, handler2);
                } while (newEvent != handler2);
            }
            remove
            {
                PropertyChangedEventHandler handler2;
                var newEvent = propertyChanged;
                do
                {
                    handler2 = newEvent;
                    var handler3 = (PropertyChangedEventHandler)Delegate.Remove(handler2, value);
                    Interlocked.CompareExchange(ref propertyChanged, handler3, handler2);
                } while (newEvent != handler2);
            }
        }

        event PropertyChangingEventHandler INotifyPropertyChanging.PropertyChanging
        {
            add
            {
                PropertyChangingEventHandler handler2;
                var newEvent = propertyChanging;
                do
                {
                    handler2 = newEvent;
                    var handler3 = (PropertyChangingEventHandler)Delegate.Combine(handler2, value);
                    Interlocked.CompareExchange(ref propertyChanging, handler3, handler2);
                } while (newEvent != handler2);
            }
            remove
            {
                PropertyChangingEventHandler handler2;
                var newEvent = propertyChanging;
                do
                {
                    handler2 = newEvent;
                    var handler3 = (PropertyChangingEventHandler)Delegate.Remove(handler2, value);
                    Interlocked.CompareExchange(ref propertyChanging, handler3, handler2);
                } while (newEvent != handler2);
            }
        }
    }
}