using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pirac.Internal
{
    internal static class UIHelper
    {
        public static IEnumerable<FrameworkElement> FindNamedChildren(DependencyObject parent)
        {
            if (parent == null)
                yield break;

            var itemQueue = new Queue<DependencyObject>();
            itemQueue.Enqueue(parent);

            while (itemQueue.Count > 0)
            {
                var next = itemQueue.Dequeue();

                // Does the item have a name
                var element = next as FrameworkElement;
                if (element != null && !string.IsNullOrEmpty(element.Name))
                    yield return element;

                // Add any children
                int childCount = VisualTreeHelper.GetChildrenCount(next);
                if (childCount > 0)
                    for (int i = 0; i < childCount; i++)
                    {
                        var child = VisualTreeHelper.GetChild(next, i);
                        itemQueue.Enqueue(child);
                    }
                else
                {
                    // Add the content
                    var contentControl = next as ContentControl;
                    if (contentControl != null)
                    {
                        var content = contentControl.Content as DependencyObject;
                        if (content != null)
                            itemQueue.Enqueue(content);
                    }
                    else
                    {
                        // Add the items
                        var itemsControl = next as ItemsControl;
                        if (itemsControl != null)
                            foreach (var item in itemsControl.Items.OfType<DependencyObject>())
                                itemQueue.Enqueue(item);
                    }
                }
            }
        }
    }
}