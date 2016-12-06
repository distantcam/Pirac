using System;
using System.Runtime.CompilerServices;
using ApprovalTests;
using Bogus;
using NUnit.Framework;
using Pirac.Conventions;
using SampleApp.UI.List;

namespace SampleApp.Tests.ViewModelAttachment
{
    [TestFixture]
    public class ListGeneratorTests
    {
        [Test]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void GeneratorPopulatesPeopleList1()
        {
            var viewModel = new ListViewModel();
            var attachment = new ListGeneratorAttachment();

            // Initialize data generator
            Randomizer.Seed = new Random(24601);

            AttachmentHelper.Attach(attachment, viewModel);

            Approvals.VerifyAll(viewModel.People, "People", p => $"{p.FirstName} {p.LastName} {p.Address} {p.PhoneNumber}");
        }

        [Test]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void GeneratorPopulatesPeopleList2()
        {
            var viewModel = new ListViewModel();
            var attachment = new ListGeneratorAttachment();

            // Initialize data generator
            Randomizer.Seed = new Random(12345);

            AttachmentHelper.Attach(attachment, viewModel);

            Approvals.VerifyAll(viewModel.People, "People", p => $"{p.FirstName} {p.LastName} {p.Address} {p.PhoneNumber}");
        }
    }
}