using System.Runtime.CompilerServices;
using System.Threading;
using NUnit.Framework;
using SampleApp.UI.List;

namespace SampleApp.Tests.ViewViewModel
{
    [Apartment(ApartmentState.STA)]
    [TestFixture]
    public class ListTests : BaseViewTest
    {
        private static void AddPeople(ListViewModel viewModel)
        {
            viewModel.People.Add(new Data.Person
            {
                FirstName = "Cameron",
                LastName = "MacFarland",
                Address = "123 Fourth St, Fiver",
                PhoneNumber = "123 456 7890"
            });

            viewModel.People.Add(new Data.Person
            {
                FirstName = "Sophie",
                LastName = "Ambrose",
                Address = "123 Fourth St, Fiver",
                PhoneNumber = "123 456 7890"
            });
        }

        [Test]
        [Explicit]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ListBindingTest()
        {
            var view = new ListView();
            var viewModel = new ListViewModel();

            AddPeople(viewModel);

            view.DataContext = viewModel;

            TestView(view);
        }

        [Test]
        [Explicit]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SelectedFirstItem()
        {
            var view = new ListView();
            var viewModel = new ListViewModel();

            AddPeople(viewModel);
            viewModel.SelectedPerson = viewModel.People[0];

            view.DataContext = viewModel;

            TestView(view);
        }

        [Test]
        [Explicit]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SelectedMiddleItem()
        {
            var view = new ListView();
            var viewModel = new ListViewModel();

            AddPeople(viewModel);
            viewModel.SelectedPerson = viewModel.People[1];

            view.DataContext = viewModel;

            TestView(view);
        }
    }
}