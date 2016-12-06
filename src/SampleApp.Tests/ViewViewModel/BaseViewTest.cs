using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Wpf;
using ApprovalUtilities.Wpf;

namespace SampleApp.Tests.ViewViewModel
{
    public class BaseViewTest
    {
        private static void PrepareForRender(UIElement control)
        {
            // From http://stackoverflow.com/a/2596035
            control.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            control.Arrange(new Rect(control.DesiredSize));

            control.Dispatcher.Invoke(() => { }, DispatcherPriority.Loaded);
        }

        protected static void TestView(UserControl view)
        {
            var border = new Border { Background = Brushes.White, Child = view };

            var host = new ContentControl
            {
                Width = 400,
                Height = 400,
                Content = border
            };

            PrepareForRender(host);

            NamerFactory.Clear();

            Approvals.Verify(new ImageWriter(f => WpfUtils.ScreenCapture(host, f)));
        }
    }
}