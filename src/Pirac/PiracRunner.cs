using System;
using System.Reactive.Concurrency;
using Pirac.Internal;

namespace Pirac
{
    public static class PiracRunner
    {
        public static void Start<T>(PiracContext context = null)
        {
            context = context ?? new PiracContext();

            Logger = context.Logger;
            Container = context.Container;
            WindowManager = context.WindowManager;
            UIScheduler = context.UIScheduler;
            BackgroundScheduler = context.BackgroundScheduler;

            ConventionManager = new ConventionManager(typeof(T), context.AttachmentConvention, context.ViewConvention, context.ViewModelConvention);

            Container.Configure(ConventionManager.FindAllViews(), ConventionManager.FindAllViewModels(), ConventionManager.FindAllAttachments(), ConventionManager);

            var viewModel = Container.GetInstance<T>();
            WindowManager.ShowWindow(viewModel);
        }

        internal static Func<string, ILogger> Logger { get; set; }
        internal static IContainer Container { get; set; }
        internal static IWindowManager WindowManager { get; set; }
        internal static IScheduler UIScheduler { get; set; }
        internal static IScheduler BackgroundScheduler { get; set; }
        internal static IConventionManager ConventionManager { get; set; }

        internal static ILogger GetLogger(string name) => Logger(name);

        internal static ILogger GetLogger<TType>() => Logger(typeof(TType).Name);

        internal static object GetViewForViewModel(object viewModel)
        {
            var viewType = ConventionManager.FindView(viewModel);
            return Container.GetInstance(viewType);
        }
    }
}