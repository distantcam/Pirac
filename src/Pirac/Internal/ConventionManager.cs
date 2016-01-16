using System;
using System.Collections.Generic;
using System.Linq;
using Conventional;

namespace Pirac.Internal
{
    internal class ConventionManager : IConventionManager
    {
        readonly Conventional.IConventionManager conventionManager;
        readonly Type attachmentConvention;
        readonly Type viewConvention;
        readonly Type viewModelConvention;

        public ConventionManager(Type scanType, Type attachmentConvention, Type viewConvention, Type viewModelConvention)
        {
            this.attachmentConvention = attachmentConvention;
            this.viewConvention = viewConvention;
            this.viewModelConvention = viewModelConvention;

            ConventionBuilder.Logger = PiracRunner.GetLogger;

            var builder = new ConventionBuilder();

            builder.Scan(scanType)
                .For(attachmentConvention)
                .For(viewConvention)
                .For(viewModelConvention);

            conventionManager = builder.Build();

            conventionManager.Verify();
        }

        public IEnumerable<Type> FindAllAttachments() => conventionManager.FindAll(attachmentConvention);

        public IEnumerable<Type> FindAllViewModels() => conventionManager.FindAll(viewModelConvention);

        public IEnumerable<Type> FindAllViews() => conventionManager.FindAll(viewConvention);

        public Type FindView(object viewModel) => conventionManager.FindAll(viewConvention, viewModel).Single();

        public IEnumerable<Type> FindMatchingAttachments(object viewModel) => conventionManager.FindAll(attachmentConvention, viewModel);
    }
}