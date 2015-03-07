using System;
using Pirac.Conventions;
using Pirac.Internal;

namespace Pirac
{
    public static partial class PiracRunner
    {
        public static class Default
        {
            private static Func<string, ILogger> logger;

            static Default()
            {
                AttachmentConvention = typeof(AttachmentConvention);
                ViewConvention = typeof(ViewConvention);
                ViewModelConvention = typeof(ViewModelConvention);

                IoC = new Container();
                Logger = t => new Logger();
            }

            public static Type AttachmentConvention { get; set; }

            public static Type ViewModelConvention { get; set; }

            public static Type ViewConvention { get; set; }

            public static IContainer IoC { get; set; }

            public static Func<string, ILogger> Logger { get; set; }
        }
    }
}