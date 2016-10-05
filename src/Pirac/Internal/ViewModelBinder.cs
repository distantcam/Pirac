using System.Collections;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Pirac.Internal
{
    internal static class ViewModelBinder
    {
        private static ILogger Log = PiracRunner.GetLogger(nameof(ViewModelBinder));

        public static void Bind(FrameworkElement view, object viewModel)
        {
            Log.Info($"Binding '{view}' to '{viewModel}'");

            var namedElements = UIHelper.FindNamedChildren(view);

            var viewModelProperties = viewModel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var matches = namedElements.Join(viewModelProperties, e => e.Name, p => p.Name, (e, p) => new { e, p });

            foreach (var element in namedElements)
            {
                var matchingProperty = viewModelProperties.Where(p => p.Name == element.Name).SingleOrDefault();
                if (matchingProperty == null)
                {
                    Log.Debug($"No matching property for element '{element.Name}'");
                    continue;
                }

                if (!TryBind(element, matchingProperty, viewModel))
                    Log.Debug($"Could not bind element '{element.Name}' to property '{matchingProperty.Name}'");
            }

            view.DataContext = viewModel;

            var viewAware = viewModel as IHaveView;
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
                Log.Info($"Binding ContentControl {contentControl.Name} to property {property.Name}");

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
                Log.Info($"Binding ItemsControl {itemsControl.Name} to property {property.Name}");

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