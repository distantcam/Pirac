using System;
using Pirac;
using SampleApp.Data;
using SampleApp.Events;
using SampleApp.Framework;

namespace SampleApp.UI.List
{
    class ListSelectionAttachment : Attachment<ListViewModel>
    {
        IEventAggregator eventAggregator;

        public ListSelectionAttachment(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        protected override void OnAttach()
        {
            viewModel.WhenPropertyChanged<Person>(nameof(viewModel.SelectedPerson))
                .Subscribe(pc => eventAggregator.Publish(new SelectedPersonChanged(pc.After)));

            eventAggregator.GetEvent<CancelPersonChanged>()
                .Subscribe(e => viewModel.SelectedPerson = e.Person);
        }
    }
}