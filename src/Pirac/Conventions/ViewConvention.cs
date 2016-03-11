using System;
using Conventional.Conventions;

namespace Pirac.Conventions
{
    internal class ViewConvention : Convention
    {
        public ViewConvention()
        {
            Must.Pass(t => t.IsClass && (t.Name.EndsWith("View") || t.Name == "MainWindow"), "Name ends with View or is named MainWindow");

            Should.BeAConcreteClass();

            BaseName = t => t.Name == "MainWindow" ? t.Name : t.Name.Substring(0, t.Name.Length - 4);

            Variants.Add(new DelegateBaseFilter((t, b) =>
            {
                if (t.Name == "MainWindow" && b == "MainWindow")
                    return true;

                return t.Name == b + "View";
            }));
        }

        class DelegateBaseFilter : IBaseFilter
        {
            private readonly Func<Type, string, bool> predicate;

            public DelegateBaseFilter(Func<Type, string, bool> predicate)
            {
                this.predicate = predicate;
            }

            public bool Matches(Type t, string baseName)
            {
                return predicate(t, baseName);
            }
        }
    }
}