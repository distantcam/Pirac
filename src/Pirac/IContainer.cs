using System;
using System.Collections.Generic;

namespace Pirac
{
    public interface IContainer
    {
        void Configure(IEnumerable<Type> views, IEnumerable<Type> viewModels, IEnumerable<Type> attachments, IConventionManager conventionManager);

        T GetInstance<T>();

        object GetInstance(Type type);
    }
}