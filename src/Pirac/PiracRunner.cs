using System;
using System.Reactive.Concurrency;
using System.Reflection;
using Pirac.Internal;

namespace Pirac
{
    public static class PiracRunner
    {
        internal static bool ContextSet;

        internal static void Start(Assembly scanAssembly, PiracContext context = null)
        {
            context = context ?? new PiracContext();

            Logger = context.Logger;
            Container = context.Container;
            WindowManager = context.WindowManager;
            UIScheduler = context.UIScheduler;
            BackgroundScheduler = context.BackgroundScheduler;

            ContextSet = true;

            ConventionManager = new ConventionManager(scanAssembly, context.AttachmentConvention, context.ViewConvention, context.ViewModelConvention);

            Container.Configure(ConventionManager.FindAllViews(), ConventionManager.FindAllViewModels(), ConventionManager.FindAllAttachments(), ConventionManager);
        }

        public static void Start<T>(PiracContext context = null)
        {
            Start(Assembly.GetCallingAssembly(), context);

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