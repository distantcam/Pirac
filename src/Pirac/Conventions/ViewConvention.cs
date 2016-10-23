using System;

namespace Pirac.Conventions
{
    internal class ViewConvention : IConvention
    {
        public bool Filter(Type type)
        {
            return (type.Name.EndsWith("View") || type.Name.EndsWith("MainWindow"))
                && type.IsClass;
        }

        public void Verify(Type type)
        {
            if (type.IsAbstract)
            {
                throw new ConventionBrokenException($"View type '{type}' must be a concrete class.");
            }
        }

        public string BaseName(Type type)
        {
            if (type.Name.Length < 4)
            {
                return string.Empty;
            }

            return type.Name == "MainWindow" ? type.Name : type.Name.Substring(0, type.Name.Length - 4);
        }

        public bool IsVariant(Type type, string basename)
        {
            if (type.Name == "MainWindow" && basename == "MainWindow")
            {
                return true;
            }

            return type.Name == basename + "View";
        }
    }
}