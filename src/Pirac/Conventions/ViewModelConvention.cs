using System;
using System.ComponentModel;
using System.Linq;

namespace Pirac.Conventions
{
    internal class ViewModelConvention : IConvention
    {
        public bool Filter(Type type)
        {
            return type.Name.EndsWith("ViewModel");
        }

        public void Verify(Type type)
        {
            if (type.IsAbstract)
            {
                throw new ConventionBrokenException($"ViewModel type '{type}' must be a concrete class.");
            }
            if (!type.GetInterfaces().Any(t => t == typeof(INotifyPropertyChanged)))
            {
                throw new ConventionBrokenException($"ViewModel type '{type}' must implement '{typeof(INotifyPropertyChanged)}'.");
            }
            if (!type.GetInterfaces().Any(t => t == typeof(INotifyPropertyChanging)))
            {
                throw new ConventionBrokenException($"ViewModel type '{type}' must implement '{typeof(INotifyPropertyChanging)}'.");
            }
        }

        public string BaseName(Type type)
        {
            if (type.Name.Length < 9)
            {
                return string.Empty;
            }

            return type.Name.Substring(0, type.Name.Length - 9);
        }

        public bool IsVariant(Type type, Type variant, string basename)
        {
            return Filter(type) && type.Name.StartsWith(basename);
        }
    }
}