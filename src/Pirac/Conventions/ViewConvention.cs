using Conventional.Conventions;

namespace Pirac.Conventions
{
    internal class ViewConvention : Convention
    {
        public ViewConvention()
        {
            Must.HaveNameEndWith("View").BeAClass();

            Should.BeAConcreteClass();

            BaseName = t => t.Name.Substring(0, t.Name.Length - 4);

            Variants.HaveBaseNameAndEndWith("View");
        }
    }
}