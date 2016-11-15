using System;
using System.Windows;
using Bogus;
using Pirac;
using SampleApp.Framework;

namespace SampleApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize data generator
            Randomizer.Seed = new Random(24601);

            PiracRunner.Start<MainWindowViewModel>(new PiracContext { Container = new AutofacContainer() });
        }
    }
}