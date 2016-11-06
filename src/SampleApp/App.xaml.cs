using System.Windows;
using Pirac;

namespace SampleApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            PiracRunner.Start<MainWindowViewModel>();
        }
    }
}