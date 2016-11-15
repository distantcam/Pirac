using System.Collections.ObjectModel;
using Pirac;
using SampleApp.Data;

namespace SampleApp.UI.List
{
    class ListViewModel : ViewModelBase
    {
        public ListViewModel()
        {
            People = new ObservableCollection<Person>();
        }

        public ObservableCollection<Person> People { get; }

        public Person SelectedPerson { get; set; }
    }
}