using System;
using System.Reactive.Linq;
using System.Windows;
using Pirac;
using SampleApp.Events;
using SampleApp.Framework;

namespace SampleApp.UI.Details
{
    class DetailsSelectionAttachment : Attachment<DetailsViewModel>
    {
        IEventAggregator eventAggregator;

        public DetailsSelectionAttachment(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        protected override void OnAttach()
        {
            eventAggregator.GetEvent<SelectedPersonChanged>()
                .Subscribe(e =>
                {
                    if (viewModel.Person == e.Person)
                        return;

                    if (viewModel.IsChanged)
                    {
                        var ask = MessageBox.Show("You have unsaved changes. Do you wish to save them?", "Save changes?", MessageBoxButton.YesNoCancel);

                        if (ask == MessageBoxResult.Cancel)
                        {
                            eventAggregator.Publish(new CancelPersonChanged(viewModel.Person));
                            return;
                        }
                        else if (ask == MessageBoxResult.Yes)
                        {
                            Save();
                        }
                    }

                    viewModel.Person = e.Person;

                    // Copy values for editing
                    viewModel.FirstName = e.Person.FirstName;
                    viewModel.LastName = e.Person.LastName;
                    viewModel.PhoneNumber = e.Person.PhoneNumber;
                    viewModel.Address = e.Person.Address;

                    viewModel.IsChanged = false;
                });

            viewModel.SaveCommand = viewModel
                .WhenPropertyChanged<bool>(nameof(viewModel.IsChanged))
                .Select(pc => pc.After)
                .ToCommand(_ =>
                {
                    Save();

                    viewModel.IsChanged = false;
                });
        }

        private void Save()
        {
            // Save values back to original
            viewModel.Person.FirstName = viewModel.FirstName;
            viewModel.Person.LastName = viewModel.LastName;
            viewModel.Person.PhoneNumber = viewModel.PhoneNumber;
            viewModel.Person.Address = viewModel.Address;
        }
    }
}