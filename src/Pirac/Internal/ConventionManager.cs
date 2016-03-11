using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Conventional;
using Pirac.Extensions;

namespace Pirac.Internal
{
    internal class ConventionManager : IConventionManager
    {
        readonly Conventional.IConventionManager conventionManager;
        readonly Type attachmentConvention;
        readonly Type viewConvention;
        readonly Type viewModelConvention;

        public ConventionManager(Assembly scanAssembly, Type attachmentConvention, Type viewConvention, Type viewModelConvention)
        {
            this.attachmentConvention = attachmentConvention;
            this.viewConvention = viewConvention;
            this.viewModelConvention = viewModelConvention;

            ConventionBuilder.Logger = PiracRunner.GetLogger;

            var builder = new ConventionBuilder();

            builder.Scan(scanAssembly)
                .For(attachmentConvention)
                .For(viewConvention)
                .For(viewModelConvention);

            conventionManager = builder.Build();

            conventionManager.Verify();
        }

        public IEnumerable<Type> FindAllAttachments() => conventionManager.FindAll(attachmentConvention);

        public IEnumerable<Type> FindAllViewModels() => conventionManager.FindAll(viewModelConvention);

        public IEnumerable<Type> FindAllViews() => conventionManager.FindAll(viewConvention);

        public Type FindView(object viewModel) => conventionManager.FindAll(viewConvention, viewModel).ThrowIfEmpty(() => new ViewNotFoundException($"View for '{viewModel.GetType()}' not found.")).Single();

        public IEnumerable<Type> FindMatchingAttachments(object viewModel) => conventionManager.FindAll(attachmentConvention, viewModel);
    }

    [Serializable]
    public class ViewNotFoundException : Exception
    {
        public ViewNotFoundException()
        {
        }

        public ViewNotFoundException(string message) : base(message)
        {
        }

        public ViewNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ViewNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}