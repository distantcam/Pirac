using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Pirac;
using Pirac.Conventions;

namespace SampleApp.Framework
{
    class AutofacContainer : Pirac.IContainer
    {
        Autofac.IContainer container;
        IConventionManager conventionManager;

        public void Configure(IEnumerable<Type> views, IEnumerable<Type> viewModels, IEnumerable<Type> attachments, IConventionManager conventionManager)
        {
            this.conventionManager = conventionManager;

            var builder = new ContainerBuilder();

            foreach (var type in views.Concat(attachments))
            {
                builder.RegisterType(type);
            }

            foreach (var type in viewModels)
            {
                builder.RegisterType(type).OnActivating(Activating);
            }

            builder.RegisterInstance(PiracRunner.WindowManager).As<IWindowManager>();

            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            container = builder.Build();
        }

        public object GetInstance(Type type) => container.Resolve(type);

        public T GetInstance<T>() => container.Resolve<T>();

        private void Activating(IActivatingEventArgs<object> e)
        {
            var matchingAttachments = conventionManager.FindMatchingAttachments(e.Instance).Select(e.Context.Resolve);
            foreach (var attachment in matchingAttachments)
            {
                AttachmentHelper.Attach(attachment, e.Instance);
            }
        }
    }
}