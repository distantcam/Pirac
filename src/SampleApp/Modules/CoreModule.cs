using Autofac;
using SampleApp.Framework;

namespace SampleApp.Modules
{
    class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
        }
    }
}