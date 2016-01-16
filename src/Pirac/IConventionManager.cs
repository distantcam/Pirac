using System;
using System.Collections.Generic;

namespace Pirac
{
    public interface IConventionManager
    {
        IEnumerable<Type> FindAllAttachments();

        IEnumerable<Type> FindAllViews();

        IEnumerable<Type> FindAllViewModels();

        Type FindView(object viewModel);

        IEnumerable<Type> FindMatchingAttachments(object viewModel);
    }
}