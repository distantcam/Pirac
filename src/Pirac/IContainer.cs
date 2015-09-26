using System;
using System.Collections.Generic;

namespace Pirac
{

    public interface IContainer
    {
        void Setup(IEnumerable<Type> typesToRegister, IEnumerable<Type> viewModelTypesToRegister);

        object GetInstance(Type type);
    }
}