using System.Windows;
using System.Windows.Controls;

namespace Pirac
{
    public class ViewModelControl : ContentControl
    {
        static ViewModelControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ViewModelControl), new FrameworkPropertyMetadata(typeof(ViewModelControl)));
        }
    }
}