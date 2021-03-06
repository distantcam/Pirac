﻿using System;
using System.Reactive.Concurrency;
using System.Windows.Threading;
using Pirac.Conventions;
using Pirac.Internal;

namespace Pirac
{
    public class PiracContext
    {
        public PiracContext()
        {
            if (Dispatcher.CurrentDispatcher == null)
                throw new Exception("Start should be called from within your Application.");

            MainScheduler = new DispatcherScheduler(Dispatcher.CurrentDispatcher);
        }

        public Func<string, ILogger> Logger { get; set; } = name => new Logger(name);
        public IContainer Container { get; set; } = new Container();
        public IWindowManager WindowManager { get; set; } = new WindowManager();
        public IScheduler MainScheduler { get; set; }
        public IScheduler BackgroundScheduler { get; set; } = TaskPoolScheduler.Default;
        public IConvention AttachmentConvention { get; set; } = new AttachmentConvention();
        public IConvention ViewConvention { get; set; } = new ViewConvention();
        public IConvention ViewModelConvention { get; set; } = new ViewModelConvention();
    }
}