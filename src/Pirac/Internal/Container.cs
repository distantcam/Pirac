using System;
using System.Collections.Generic;
using System.Linq;
using Pirac.LightInject;

namespace Pirac.Internal
{
    internal class Container : IContainer
    {
        private IServiceContainer container = new ServiceContainer();

        public void Configure(IEnumerable<Type> views, IEnumerable<Type> viewModels, IEnumerable<Type> attachments, IConventionManager conventionManager)
        {
            foreach (var type in views.Concat(viewModels).Concat(attachments))
            {
                container.Register(type);
            }

            container.Initialize(registration => viewModels.Contains(registration.ServiceType), (factory, instance) =>
            {
                var matchingAttachments = conventionManager.FindMatchingAttachments(instance).Select(factory.GetAllInstances).Cast<IAttachment>();
                foreach (var attachment in matchingAttachments)
                {
                    attachment.AttachTo(instance);
                }
            });
        }

        public object GetInstance(Type type) => container.GetInstance(type);

        public T GetInstance<T>() => container.GetInstance<T>();
    }
}