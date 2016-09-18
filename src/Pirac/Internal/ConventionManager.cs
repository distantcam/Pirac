using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Pirac.Conventions;

namespace Pirac.Internal
{
    internal class ConventionManager : IConventionManager
    {
        readonly IConvention attachmentConvention;
        readonly IConvention viewConvention;
        readonly IConvention viewModelConvention;

        readonly IEnumerable<Type> attachmentTypes;
        readonly IEnumerable<Type> viewTypes;
        readonly IEnumerable<Type> viewModelTypes;

        public ConventionManager(Assembly scanAssembly, IConvention attachmentConvention, IConvention viewConvention, IConvention viewModelConvention)
        {
            this.attachmentConvention = attachmentConvention;
            this.viewConvention = viewConvention;
            this.viewModelConvention = viewModelConvention;

            attachmentTypes = scanAssembly.GetTypes().Where(attachmentConvention.Filter).ToList();
            viewTypes = scanAssembly.GetTypes().Where(viewConvention.Filter).ToList();
            viewModelTypes = scanAssembly.GetTypes().Where(viewModelConvention.Filter).ToList();

            foreach (var item in attachmentTypes)
            {
                attachmentConvention.Verify(item);
            }
            foreach (var item in viewTypes)
            {
                viewConvention.Verify(item);
            }
            foreach (var item in viewModelTypes)
            {
                viewModelConvention.Verify(item);
            }
        }

        public IEnumerable<Type> FindAllAttachments() => attachmentTypes;

        public IEnumerable<Type> FindAllViewModels() => viewModelTypes;

        public IEnumerable<Type> FindAllViews() => viewTypes;

        public Type FindView(object viewModel)
            => FindAll(viewConvention, viewTypes, viewModelConvention.BaseName(viewModel.GetType()))
            .ThrowIfEmpty(() => new ViewNotFoundException($"View for '{viewModel.GetType()}' not found."))
            .Single();

        public IEnumerable<Type> FindMatchingAttachments(object viewModel)
            => FindAll(attachmentConvention, attachmentTypes, viewModelConvention.BaseName(viewModel.GetType()));

        private IEnumerable<Type> FindAll(IConvention convention, IEnumerable<Type> types, string basename)
            => types.Where(t => convention.IsVariant(t, basename));
    }
}