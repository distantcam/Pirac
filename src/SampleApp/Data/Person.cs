using Bogus;
using Pirac;

namespace SampleApp.Data
{
    class Person : BindableObject
    {
        public static Faker<Person> Generator = new Faker<Person>()
            .StrictMode(true)
            .RuleFor(p => p.FirstName, f => f.Person.FirstName)
            .RuleFor(p => p.LastName, f => f.Person.LastName)
            .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(p => p.Address, f => f.Address.StreetAddress());

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}