using System;
using System.Windows;

namespace Pirac
{
    public class HasViewBase : BindableObject, IHaveView
    {
        private WeakReference<object> view;

        public void TryClose(bool? dialogResult = null)
        {
            object v;
            if (view != null && view.TryGetTarget(out v))
            {
                if (CanClose(v))
                    Close(v, dialogResult);
            }
        }

        internal object GetView()
        {
            object v = null;
            view?.TryGetTarget(out v);
            return v;
        }

        protected virtual void OnViewAttached(object view)
        {
        }

        protected virtual void OnViewLoaded(FrameworkElement view)
        {
        }

        protected virtual bool CanClose(object view)
        {
            return true;
        }

        private static void Close(object view, bool? dialogResult)
        {
            var viewType = view.GetType();
            var closeMethod = viewType.GetMethod("Close");

            if (closeMethod != null)
            {
                var isClosed = false;
                if (dialogResult != null)
                {
                    var resultProperty = viewType.GetProperty("DialogResult");
                    if (resultProperty != null)
                    {
                        resultProperty.SetValue(view, dialogResult, null);
                        isClosed = true;
                    }
                }

                if (!isClosed)
                {
                    closeMethod.Invoke(view, null);
                }
            }
            else
            {
                PiracRunner.GetLogger<HasViewBase>().Warn($"Type {viewType} does not have a 'Close' method.");
            }
        }

        void IHaveView.AttachView(object view)
        {
            this.view = new WeakReference<object>(view);

            var element = view as FrameworkElement;
            if (element != null)
            {
                if (element.IsLoaded)
                {
                    OnViewLoaded(element);
                }
                else
                {
                    RoutedEventHandler handler = null;
                    handler = (s, e) =>
                    {
                        element.Loaded -= handler;
                        OnViewLoaded(element);
                    };
                    element.Loaded += handler;
                }
            }

            OnViewAttached(view);
        }
    }
}