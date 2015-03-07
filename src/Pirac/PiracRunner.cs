using System;
using System.Linq;
using Conventional;

namespace Pirac
{
    public static partial class PiracRunner
    {
        private static IContainer container;
        private static ILogger Log;

        internal static IConventionManager ConventionManager { get; set; }

        public static void Start<T>()
        {
            Log = GetLogger(typeof(PiracRunner));

            ConventionBuilder.Logger = Default.Logger;

            var builder = new ConventionBuilder();

            builder.Scan<T>()
                .For(Default.AttachmentConvention)
                .For(Default.ViewConvention)
                .For(Default.ViewModelConvention);

            ConventionManager = builder.Build();

            ConventionManager.Verify();

            var typesToRegister = ConventionManager.FindAll(Default.ViewConvention)
                .Concat(ConventionManager.FindAll(Default.AttachmentConvention))
                ;

            container = Default.IoC;

            container.Setup(typesToRegister, ConventionManager.FindAll(Default.ViewModelConvention));

            var viewModel = GetInstance(typeof(T));

            WindowManager.ShowWindow(viewModel);
        }

        public static ILogger GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }

        public static ILogger GetLogger(Type type)
        {
            return GetLogger(type.FullName);
        }

        public static ILogger GetLogger(string name)
        {
            return Default.Logger(name);
        }

        internal static T GetInstance<T>()
        {
            return (T)container.GetInstance(typeof(T));
        }

        internal static object GetInstance(Type type)
        {
            return container.GetInstance(type);
        }

        internal static object GetViewForViewModel(object viewModel)
        {
            var viewType = ConventionManager.FindAll(Default.ViewConvention, viewModel).Single();

            return GetInstance(viewType);
        }
    }
}