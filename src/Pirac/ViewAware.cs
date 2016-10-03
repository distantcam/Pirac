using System;
using System.Windows;

namespace Pirac
{
    public class ViewAware : BindableObject, IViewAware
    {
        private WeakReference<FrameworkElement> view;

        public void TryClose(bool? dialogResult = null)
        {
            FrameworkElement v;
            if (view != null && view.TryGetTarget(out v) && CanClose())
            {
                Close(this, v, dialogResult);
            }
        }

        protected virtual void OnViewAttached(FrameworkElement view)
        {
        }

        protected virtual void OnViewLoaded(FrameworkElement view)
        {
        }

        protected virtual bool CanClose()
        {
            return true;
        }

        private static void Close(object viewModel, FrameworkElement view, bool? dialogResult)
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
                PiracRunner.GetLogger<ViewAware>().Warn("TryClose requires a view with a Close method.");
            }
        }

        void IViewAware.AttachView(FrameworkElement view)
        {
            this.view = new WeakReference<FrameworkElement>(view);

            if (view.IsLoaded)
            {
                OnViewLoaded(view);
            }
            else
            {
                RoutedEventHandler handler = null;
                handler = (s, e) =>
                {
                    view.Loaded -= handler;
                    OnViewLoaded(view);
                };
                view.Loaded += handler;
            }

            OnViewAttached(view);
        }
    }
}