using SampleApp.Data;

namespace SampleApp.Events
{
    class CancelPersonChanged
    {
        public CancelPersonChanged(Person person)
        {
            Person = person;
        }

        public Person Person { get; }
    }
}