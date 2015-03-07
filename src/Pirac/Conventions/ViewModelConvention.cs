using System.ComponentModel;
using Conventional.Conventions;

namespace Pirac.Conventions
{
    public class ViewModelConvention : Convention
    {
        public ViewModelConvention()
        {
            Must.HaveNameEndWith("ViewModel");

            Should.Implement<INotifyPropertyChanged>();

            BaseName = t => t.Name.Substring(0, t.Name.Length - 9);

            Variants.HaveBaseNameAndEndWith("ViewModel");
        }
    }
}