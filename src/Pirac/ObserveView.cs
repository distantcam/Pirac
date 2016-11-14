using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Windows;

namespace Pirac
{
    public class ObserveView : BindableObject, IObserveView
    {
        private Subject<object> viewAttached;
        private Subject<FrameworkElement> viewLoaded;

        private IObservable<object> whenViewAttached;
        private IObservable<FrameworkElement> whenViewLoaded;

        private WeakReference<object> view;

        private volatile int disposeSignaled;

        public ObserveView()
        {
            viewAttached = new Subject<object>();
            whenViewAttached = viewAttached.AsObservable();

            viewLoaded = new Subject<FrameworkElement>();
            whenViewLoaded = viewLoaded.AsObservable();
        }

        public IObservable<object> WhenViewAttached() => whenViewAttached;

        public IObservable<FrameworkElement> WhenViewLoaded() => whenViewLoaded;

        void IObserveView.AttachView(object view)
        {
            this.view = new WeakReference<object>(view);

            viewAttached.OnNext(view);

            var element = view as FrameworkElement;
            if (element != null)
            {
                if (element.IsLoaded)
                {
                    viewLoaded.OnNext(element);
                }
                else
                {
                    RoutedEventHandler handler = null;
                    handler = (s, e) =>
                    {
                        element.Loaded -= handler;
                        viewLoaded.OnNext(element);
                    };
                    element.Loaded += handler;
                }
            }
        }

        public override void Dispose()
        {
            if (Interlocked.Exchange(ref disposeSignaled, 1) != 0)
            {
                return;
            }
            if (viewAttached != null)
            {
                viewAttached.OnCompleted();
                viewAttached.Dispose();
                viewAttached = null;
            }
            if (viewLoaded != null)
            {
                viewLoaded.OnCompleted();
                viewLoaded.Dispose();
                viewLoaded = null;
            }

            base.Dispose();
        }
    }
}