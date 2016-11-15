using SampleApp.Data;

namespace SampleApp.Events
{
    class SelectedPersonChanged
    {
        public SelectedPersonChanged(Person person)
        {
            Person = person;
        }

        public Person Person { get; }
    }
}