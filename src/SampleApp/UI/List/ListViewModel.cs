using System.Collections.ObjectModel;
using Pirac;
using SampleApp.Data;

namespace SampleApp.UI.List
{
    class ListViewModel : ViewModelBase
    {
        public ObservableCollection<Person> People { get; } = new ObservableCollection<Person>();

        public Person SelectedPerson { get; set; }
    }
}