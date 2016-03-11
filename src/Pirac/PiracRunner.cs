using System;
using System.Reactive.Concurrency;
using Pirac.Internal;

namespace Pirac
{
    public static class PiracRunner
    {
        private static bool contextSet;

        public static void Start<T>(PiracContext context = null)
        {
            context = context ?? new PiracContext();

            Logger = context.Logger;
            Container = context.Container;
            WindowManager = context.WindowManager;
            UIScheduler = context.UIScheduler;
            BackgroundScheduler = context.BackgroundScheduler;

            ConventionManager = new ConventionManager(typeof(T), context.AttachmentConvention, context.ViewConvention, context.ViewModelConvention);

            contextSet = true;

            Container.Configure(ConventionManager.FindAllViews(), ConventionManager.FindAllViewModels(), ConventionManager.FindAllAttachments(), ConventionManager);

            var viewModel = Container.GetInstance<T>();
            WindowManager.ShowWindow(viewModel);
        }

        public static Func<string, ILogger> Logger { get; private set; }
        public static IContainer Container { get; private set; }
        public static IWindowManager WindowManager { get; private set; }
        public static IScheduler UIScheduler { get; private set; }
        public static IScheduler BackgroundScheduler { get; private set; }
        public static IConventionManager ConventionManager { get; private set; }

        public static ILogger GetLogger(string name) => Logger(name);

        public static ILogger GetLogger<TType>() => Logger(typeof(TType).Name);

        internal static object GetViewForViewModel(object viewModel)
        {
            var viewType = ConventionManager.FindView(viewModel);
            return Container.GetInstance(viewType);
        }
    }
}