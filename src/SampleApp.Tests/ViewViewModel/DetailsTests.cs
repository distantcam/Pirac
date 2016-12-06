using System.Runtime.CompilerServices;
using System.Threading;
using NUnit.Framework;
using SampleApp.UI.Details;

namespace SampleApp.Tests.ViewViewModel
{
    [Apartment(ApartmentState.STA)]
    [TestFixture]
    public class DetailsTests : BaseViewTest
    {
        [Test]
        [Explicit]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void PersonDetailsBindingTest()
        {
            var view = new DetailsView();
            var viewModel = new DetailsViewModel();

            viewModel.FirstName = "Cameron";
            viewModel.LastName = "MacFarland";
            viewModel.PhoneNumber = "123 456 7890";
            viewModel.Address = "123 Fourth Ave, Fiver";
            viewModel.IsChanged = false;

            view.DataContext = viewModel;

            TestView(view);
        }

        [Test]
        [Explicit]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SaveEnabledWhenChanged()
        {
            var view = new DetailsView();
            var viewModel = new DetailsViewModel();

            viewModel.FirstName = "Cameron";
            viewModel.LastName = "MacFarland";
            viewModel.PhoneNumber = "123 456 7890";
            viewModel.Address = "123 Fourth Ave, Fiver";

            view.DataContext = viewModel;

            TestView(view);
        }
    }
}