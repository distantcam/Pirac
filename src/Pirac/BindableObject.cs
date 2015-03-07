using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace Pirac
{
    public class BindableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private long changeNotificationSuppressionCount;

        private ISubject<ReactivePropertyChangedEventArgs> changed;
        private ISubject<ReactivePropertyChangingEventArgs> changing;

        public BindableObject()
        {
            changed = new Subject<ReactivePropertyChangedEventArgs>();
            changed.ObserveOnDispatcher().Subscribe(args =>
            {
                var handler = PropertyChanged;
                if (handler != null)
                    handler(args.Sender, new PropertyChangedEventArgs(args.PropertyName));
            });
            Changed = changed.AsObservable();

            changing = new Subject<ReactivePropertyChangingEventArgs>();
            changing.ObserveOnDispatcher().Subscribe(args =>
            {
                var handler = PropertyChanging;
                if (handler != null)
                    handler(args.Sender, new PropertyChangingEventArgs(args.PropertyName));
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        public IObservable<ReactivePropertyChangedEventArgs> Changed { get; }

        public IObservable<ReactivePropertyChangingEventArgs> Changing { get; }

        public bool ChangeNotificationEnabled => Interlocked.Read(ref changeNotificationSuppressionCount) == 0L;

        public IDisposable SuppressNotifications()
        {
            Interlocked.Increment(ref changeNotificationSuppressionCount);
            return Disposable.Create(() => Interlocked.Decrement(ref changeNotificationSuppressionCount));
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (ChangeNotificationEnabled)
                changed.OnNext(new ReactivePropertyChangedEventArgs(propertyName, this));
        }

        protected void OnPropertyChanging(string propertyName)
        {
            if (ChangeNotificationEnabled)
                changing.OnNext(new ReactivePropertyChangingEventArgs(propertyName, this));
        }
    }

    public class ReactivePropertyChangedEventArgs
    {
        private readonly string propertyName;
        private readonly object sender;

        public ReactivePropertyChangedEventArgs(string propertyName, object sender)
        {
            this.sender = sender;
            this.propertyName = propertyName;
        }

        public string PropertyName => propertyName;

        public object Sender => sender;
    }

    public class ReactivePropertyChangingEventArgs
    {
        private readonly string propertyName;
        private readonly object sender;

        public ReactivePropertyChangingEventArgs(string propertyName, object sender)
        {
            this.sender = sender;
            this.propertyName = propertyName;
        }

        public string PropertyName => propertyName;

        public object Sender => sender;
    }
}