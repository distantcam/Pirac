using System.Windows.Input;
using Pirac;
using PropertyChanged;
using SampleApp.Data;

namespace SampleApp.UI.Details
{
    class DetailsViewModel : ViewModelBase
    {
        public bool IsChanged { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public Person Person { get; set; }

        [DoNotSetChanged]
        public ICommand SaveCommand { get; set; }
    }
}