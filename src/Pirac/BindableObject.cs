using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace Pirac
{
    public partial class BindableObject : IObservablePropertyChanged, IObservablePropertyChanging, IObservableDataErrorInfo, IDisposable
    {
        private long changeNotificationSuppressionCount;

        private Subject<PropertyChangedData> changed;
        private Subject<PropertyChangingData> changing;
        private Subject<DataErrorChanged> errorChanged;

        private IObservable<PropertyChangedData> whenChanged;
        private IObservable<PropertyChangingData> whenChanging;
        private IObservable<DataErrorChanged> whenErrorChanged;

        private volatile int disposeSignaled;

        public BindableObject()
        {
            changed = new Subject<PropertyChangedData>();
            whenChanged = changed.AsObservable();
            whenChanged.ObserveOnPiracMain()
                .Subscribe(args =>
                {
                    propertyChanged?.Invoke(this, new PropertyChangedEventArgs(args.PropertyName));
                });

            changing = new Subject<PropertyChangingData>();
            whenChanging = changing.AsObservable();
            whenChanging.ObserveOnPiracMain()
                .Subscribe(args =>
                {
                    propertyChanging?.Invoke(this, new PropertyChangingEventArgs(args.PropertyName));
                });

            errorChanged = new Subject<DataErrorChanged>();
            whenErrorChanged = errorChanged.AsObservable();
            whenErrorChanged.ObserveOnPiracMain()
                .Subscribe(args =>
                {
                    if (string.IsNullOrEmpty(args.Error))
                    {
                        string value;
                        errors.TryRemove(args.PropertyName, out value);
                    }
                    else
                    {
                        errors.AddOrUpdate(args.PropertyName, args.Error, (_, __) => args.Error);
                    }
                    errorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(args.PropertyName));
                });
        }

        IObservable<PropertyChangedData> IObservablePropertyChanged.Changed => whenChanged;

        IObservable<PropertyChangingData> IObservablePropertyChanging.Changing => whenChanging;

        IObservable<DataErrorChanged> IObservableDataErrorInfo.ErrorsChanged => whenErrorChanged;

        public bool ChangeNotificationEnabled => Interlocked.Read(ref changeNotificationSuppressionCount) == 0L;

        public IDisposable SuppressNotifications()
        {
            Interlocked.Increment(ref changeNotificationSuppressionCount);
            return Disposable.Create(() => Interlocked.Decrement(ref changeNotificationSuppressionCount));
        }

        public virtual void Dispose()
        {
            if (Interlocked.Exchange(ref disposeSignaled, 1) != 0)
            {
                return;
            }
            if (changing != null)
            {
                changing.OnCompleted();
                changing.Dispose();
                changing = null;
            }
            if (changed != null)
            {
                changed.OnCompleted();
                changed.Dispose();
                changed = null;
            }
            if (errorChanged != null)
            {
                errorChanged.OnCompleted();
                errorChanged.Dispose();
                errorChanged = null;
            }
        }

        protected void OnPropertyChanged(string propertyName, object before, object after)
        {
            if (ChangeNotificationEnabled)
                changed.OnNext(new PropertyChangedData(propertyName, before, after));
        }

        protected void OnPropertyChanging(string propertyName, object before)
        {
            if (ChangeNotificationEnabled)
                changing.OnNext(new PropertyChangingData(propertyName, before));
        }

        public void SetDataError(string propertyName, string error)
        {
            errorChanged.OnNext(new DataErrorChanged(propertyName, error));
        }

        public void ResetDataError(string propertyName)
        {
            errorChanged.OnNext(new DataErrorChanged(propertyName, ""));
        }
    }
}