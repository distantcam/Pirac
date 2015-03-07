﻿using System;
using System.Collections.Generic;
using Pirac.LightInject;

namespace Pirac.Internal
{
    internal class Container : IContainer
    {
        private HashSet<Type> viewModelTypes;
        private IServiceContainer container = new ServiceContainer();

        public void Setup(IEnumerable<Type> typesToRegister, IEnumerable<Type> viewModelTypesToRegister)
        {
            typesToRegister.Apply(container.Register);
            viewModelTypesToRegister.Apply(container.Register);

            this.viewModelTypes = new HashSet<Type>(viewModelTypesToRegister);
        }

        public object GetInstance(Type type)
        {
            var instance = container.GetInstance(type);

            if (viewModelTypes.Contains(type))
            {
                var attachments = PiracRunner.ConventionManager.FindAll(PiracRunner.Default.AttachmentConvention, type);
                if (attachments != null)
                    foreach (var a in attachments)
                        ((IAttachment)container.GetInstance(a)).AttachTo(instance);
            }

            return instance;
        }
    }
}