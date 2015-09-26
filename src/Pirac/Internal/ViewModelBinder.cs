using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Pirac.Internal
{
    internal static class ViewModelBinder
    {
        private static ILogger Log = PiracRunner.GetLogger(typeof(ViewModelBinder));

        public static void Bind(FrameworkElement view, object viewModel)
        {
            Log.Info(string.Format("Binding '{0}' to '{1}'", view, viewModel));

            var namedElements = UIHelper.FindNamedChildren(view);

            var viewModelProperties = viewModel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var matches = namedElements.Join(viewModelProperties, e => e.Name, p => p.Name, (e, p) => new { e, p });

            foreach (var element in namedElements)
            {
                var matchingProperty = viewModelProperties.Where(p => p.Name == element.Name).SingleOrDefault();
                if (matchingProperty == null)
                {
                    Log.Debug(String.Format("No matching property for element '{0}'", element.Name));
                    continue;
                }

                if (!TryBind(element, matchingProperty, viewModel))
                    Log.Debug(string.Format("Could not bind element '{0}' to property '{1}'", element.Name, matchingProperty.Name));
            }

            view.DataContext = viewModel;

            var viewAware = viewModel as IViewAware;
            viewAware?.AttachView(view);
        }

        private static bool TryBind(FrameworkElement element, PropertyInfo property, object viewModel)
        {
            // Unlike Caliburn.Micro we only name bind to certain control types.
            // - ContentControl
            // - ItemsControl

            var contentControl = element as ContentControl;
            if (contentControl != null && contentControl.Content == null)
            {
                Log.Info(string.Format("Binding ContentControl {0} to property {1}", contentControl.Name, property.Name));

                var contentViewModel = property.GetValue(viewModel, null);
                if (contentViewModel != null)
                {
                    var contentView = (FrameworkElement)PiracRunner.GetViewForViewModel(contentViewModel);
                    Bind(contentView, contentViewModel);
                    contentControl.Content = contentView;

                    return true;
                }
            }

            var itemsControl = element as ItemsControl;
            if (itemsControl != null)
            {
                Log.Info(string.Format("Binding ItemsControl {0} to property {1}", itemsControl.Name, property.Name));

                var itemViewModel = property.GetValue(viewModel, null) as IEnumerable;
                if (itemViewModel != null)
                {
                    var itemViews = itemViewModel.OfType<object>().Select(vm =>
                    {
                        var view = (FrameworkElement)PiracRunner.GetViewForViewModel(vm);
                        Bind(view, vm);
                        return view;
                    })
                    .ToList();

                    itemsControl.ItemsSource = itemViews;
                    return true;
                }
            }

            return false;
        }
    }
}