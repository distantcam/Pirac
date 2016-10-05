using System;
using System.Reactive.Concurrency;
using System.Reflection;
using System.Threading;
using Pirac.Internal;

namespace Pirac
{
    public static class PiracRunner
    {
        private static int contextSet;

        public static void Start<T>(PiracContext context = null)
        {
            context = context ?? new PiracContext();

            EnsureContext(context);

            ConventionManager = new ConventionManager(Assembly.GetCallingAssembly(), context.AttachmentConvention, context.ViewConvention, context.ViewModelConvention);

            Container.Configure(ConventionManager.FindAllViews(), ConventionManager.FindAllViewModels(), ConventionManager.FindAllAttachments(), ConventionManager);

            var viewModel = Container.GetInstance<T>();
            WindowManager.ShowWindow(viewModel);
        }

        internal static Func<string, ILogger> Logger { get; private set; }
        internal static IContainer Container { get; private set; }
        public static IWindowManager WindowManager { get; private set; }
        internal static IScheduler MainScheduler { get; private set; }
        internal static IScheduler BackgroundScheduler { get; private set; }
        internal static IConventionManager ConventionManager { get; private set; }
        internal static bool IsContextSet => contextSet == 1;

        public static ILogger GetLogger(string name)
        {
            EnsureContext(null);
            return Logger(name);
        }

        public static ILogger GetLogger<TType>()
        {
            EnsureContext(null);
            return Logger(typeof(TType).Name);
        }

        internal static object GetViewForViewModel(object viewModel)
        {
            var viewType = ConventionManager.FindView(viewModel);
            return Container.GetInstance(viewType);
        }

        internal static void EnsureContext(PiracContext context)
        {
            // JIT Startup
            if (Interlocked.Exchange(ref contextSet, 1) == 0)
            {
                context = context ?? new PiracContext();

                Logger = context.Logger;
                Container = context.Container;
                WindowManager = context.WindowManager;
                MainScheduler = context.MainScheduler;
                BackgroundScheduler = context.BackgroundScheduler;
            }
        }
    }
}