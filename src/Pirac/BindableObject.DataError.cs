using System;
using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;

namespace Pirac
{
    partial class BindableObject : INotifyDataErrorInfo
    {
        private EventHandler<DataErrorsChangedEventArgs> errorsChanged;
        private ConcurrentDictionary<string, string> errors = new ConcurrentDictionary<string, string>();

        bool INotifyDataErrorInfo.HasErrors => errors.Any();

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            string value;
            return errors.TryGetValue(propertyName, out value) ? value : "";
        }

        event EventHandler<DataErrorsChangedEventArgs> INotifyDataErrorInfo.ErrorsChanged
        {
            add
            {
                EventHandler<DataErrorsChangedEventArgs> handler2;
                var newEvent = errorsChanged;
                do
                {
                    handler2 = newEvent;
                    var handler3 = (EventHandler<DataErrorsChangedEventArgs>)Delegate.Combine(handler2, value);
                    Interlocked.CompareExchange(ref errorsChanged, handler3, handler2);
                } while (newEvent != handler2);
            }
            remove
            {
                EventHandler<DataErrorsChangedEventArgs> handler2;
                var newEvent = errorsChanged;
                do
                {
                    handler2 = newEvent;
                    var handler3 = (EventHandler<DataErrorsChangedEventArgs>)Delegate.Remove(handler2, value);
                    Interlocked.CompareExchange(ref errorsChanged, handler3, handler2);
                } while (newEvent != handler2);
            }
        }
    }
}