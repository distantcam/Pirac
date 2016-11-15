using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using Pirac;

namespace SampleApp.Framework
{
    interface IEventAggregator : IDisposable
    {
        IObservable<TEvent> GetEvent<TEvent>();

        void Publish<TEvent>(TEvent sampleEvent);
    }

    class EventAggregator : IEventAggregator
    {
        private Subject<object> subject = new Subject<object>();

        public IObservable<TEvent> GetEvent<TEvent>()
        {
            return subject.OfType<TEvent>().AsObservable().ObserveOnPiracBackground();
        }

        public void Publish<TEvent>(TEvent sampleEvent)
        {
            subject.OnNext(sampleEvent);
        }

        public void Dispose()
        {
            Interlocked.Exchange(ref subject, null)?.Dispose();
        }
    }
}