using System;
using System.Reactive.Concurrency;
using System.Windows.Threading;
using Pirac.Internal;

namespace Pirac
{
    public class PiracContext
    {
        public PiracContext()
        {
            if (Dispatcher.CurrentDispatcher == null)
                throw new Exception("Start should be called from within your Application.");

            UIScheduler = new DispatcherScheduler(Dispatcher.CurrentDispatcher);
        }

        public Func<string, ILogger> Logger { get; set; } = name => new Logger(name);
        public IContainer Container { get; set; } = new Container();
        public IWindowManager WindowManager { get; set; } = new WindowManager();
        public IScheduler UIScheduler { get; set; }
        public IScheduler BackgroundScheduler { get; set; } = TaskPoolScheduler.Default;
        public Type AttachmentConvention { get; set; } = typeof(Conventions.AttachmentConvention);
        public Type ViewConvention { get; set; } = typeof(Conventions.ViewConvention);
        public Type ViewModelConvention { get; set; } = typeof(Conventions.ViewModelConvention);
    }
}