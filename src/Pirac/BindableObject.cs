using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace Pirac
{
    public class BindableObject : INotifyPropertyChanged, INotifyPropertyChanging, IObservablePropertyChanged, IObservablePropertyChanging, IDisposable
    {
        private long changeNotificationSuppressionCount;

        private Subject<PropertyChangedData> changed;
        private Subject<PropertyChangingData> changing;

        private volatile int disposeSignaled;

        public BindableObject()
        {
            changed = new Subject<PropertyChangedData>();
            changed.ObserveOn(SchedulerProvider.UIScheduler)
                .Subscribe(args =>
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(args.PropertyName));
                });
            Changed = changed.AsObservable();

            changing = new Subject<PropertyChangingData>();
            changing.ObserveOn(SchedulerProvider.UIScheduler)
                .Subscribe(args =>
                {
                    PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(args.PropertyName));
                });
            Changing = changing.AsObservable();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        public IObservable<PropertyChangedData> Changed { get; }

        public IObservable<PropertyChangingData> Changing { get; }

        public bool ChangeNotificationEnabled => Interlocked.Read(ref changeNotificationSuppressionCount) == 0L;

        public IDisposable SuppressNotifications()
        {
            Interlocked.Increment(ref changeNotificationSuppressionCount);
            return Disposable.Create(() => Interlocked.Decrement(ref changeNotificationSuppressionCount));
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
        }
    }
}