using System;
using System.Reactive.Concurrency;
using System.Reflection;
using Pirac.Internal;

namespace Pirac
{
    public static class PiracRunner
    {
        internal static bool ContextSet;

        internal static void SetContext(PiracContext context)
        {
            context = context ?? new PiracContext();

            Logger = context.Logger;
            Container = context.Container;
            WindowManager = context.WindowManager;
            MainScheduler = context.MainScheduler;
            BackgroundScheduler = context.BackgroundScheduler;

            ContextSet = true;
        }

        public static void Start<T>(PiracContext context = null)
        {
            SetContext(context);

            ConventionManager = new ConventionManager(Assembly.GetCallingAssembly(), context.AttachmentConvention, context.ViewConvention, context.ViewModelConvention);

            Container.Configure(ConventionManager.FindAllViews(), ConventionManager.FindAllViewModels(), ConventionManager.FindAllAttachments(), ConventionManager);

            var viewModel = Container.GetInstance<T>();
            WindowManager.ShowWindow(viewModel);
        }

        public static Func<string, ILogger> Logger { get; private set; }
        public static IContainer Container { get; private set; }
        public static IWindowManager WindowManager { get; private set; }
        internal static IScheduler MainScheduler { get; private set; }
        internal static IScheduler BackgroundScheduler { get; private set; }
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