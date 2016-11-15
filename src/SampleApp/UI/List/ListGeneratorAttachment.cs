using Pirac;
using SampleApp.Data;

namespace SampleApp.UI.List
{
    class ListGeneratorAttachment : Attachment<ListViewModel>
    {
        protected override void OnAttach()
        {
            for (int i = 0; i < 10; i++)
            {
                viewModel.People.Add(Person.Generator.Generate());
            }
        }
    }
}