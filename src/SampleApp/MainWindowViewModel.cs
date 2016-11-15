using Pirac;
using SampleApp.UI.Details;
using SampleApp.UI.List;

namespace SampleApp
{
    class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(ListViewModel list, DetailsViewModel details)
        {
            List = list;
            Details = details;

            AddChildren(list, details);
        }

        public ListViewModel List { get; }
        public DetailsViewModel Details { get; }
    }
}