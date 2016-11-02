using System;

namespace Pirac.Conventions
{
    public interface IConvention
    {
        bool Filter(Type type);

        void Verify(Type type);

        string BaseName(Type type);

        bool IsVariant(Type type, Type variant, string basename);
    }
}