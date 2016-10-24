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

        static PiracRunner()
        {
            // Set up values for testing

            // TODO Use a test scheduler
            MainScheduler = CurrentThreadScheduler.Instance;
            BackgroundScheduler = CurrentThreadScheduler.Instance;

            Logger = s => new Logger(s);
        }

        public static void Start<T>(PiracContext context = null)
        {
            context = context ?? new PiracContext();

            SetContext(context);

            ConventionManager = new ConventionManager(Assembly.GetCallingAssembly(), context.AttachmentConvention, context.ViewConvention, context.ViewModelConvention);

            Container.Configure(ConventionManager.FindAllViews(), ConventionManager.FindAllViewModels(), ConventionManager.FindAllAttachments(), ConventionManager);

            var viewModel = Container.GetInstance<T>();
            WindowManager.ShowWindow(viewModel);
        }

        public static void SetContext(PiracContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context), $"{nameof(context)} is null.");

            if (Interlocked.Exchange(ref contextSet, 1) == 0)
            {
                Logger = context.Logger;
                Container = context.Container;
                WindowManager = context.WindowManager;
                MainScheduler = context.MainScheduler;
                BackgroundScheduler = context.BackgroundScheduler;
            }
        }

        internal static Func<string, ILogger> Logger { get; private set; }
        internal static IContainer Container { get; private set; }
        public static IWindowManager WindowManager { get; private set; }
        internal static IScheduler MainScheduler { get; private set; }
        internal static IScheduler BackgroundScheduler { get; private set; }
        internal static IConventionManager ConventionManager { get; private set; }
        internal static bool IsContextSet => contextSet == 1;

        public static ILogger GetLogger(string name) => Logger(name);

        public static ILogger GetLogger<TType>() => Logger(typeof(TType).Name);

        internal static object GetViewForViewModel(object viewModel)
        {
            var viewType = ConventionManager.FindView(viewModel);
            return Container.GetInstance(viewType);
        }
    }
}