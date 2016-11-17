using System;
using System.Reactive.Linq;
using System.Windows;
using Pirac;
using SampleApp.Events;
using SampleApp.Framework;

namespace SampleApp.UI.Details
{
    class DetailsSaveLoadAttachment : Attachment<DetailsViewModel>
    {
        IEventAggregator eventAggregator;
        IDialogService dialogService;

        public DetailsSaveLoadAttachment(IEventAggregator eventAggregator, IDialogService dialogService)
        {
            this.dialogService = dialogService;
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
                        var ask = dialogService.Show("You have unsaved changes. Do you wish to save them?", "Save changes?", MessageBoxButton.YesNoCancel);

                        if (ask == MessageBoxResult.Cancel)
                        {
                            eventAggregator.Publish(new CancelPersonChanged(viewModel.Person));
                            return;
                        }
                        if (ask == MessageBoxResult.Yes)
                        {
                            Save();
                        }
                    }

                    viewModel.Person = e.Person;

                    Load();

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

        private void Load()
        {
            viewModel.FirstName = viewModel.Person.FirstName;
            viewModel.LastName = viewModel.Person.LastName;
            viewModel.PhoneNumber = viewModel.Person.PhoneNumber;
            viewModel.Address = viewModel.Person.Address;
        }

        private void Save()
        {
            viewModel.Person.FirstName = viewModel.FirstName;
            viewModel.Person.LastName = viewModel.LastName;
            viewModel.Person.PhoneNumber = viewModel.PhoneNumber;
            viewModel.Person.Address = viewModel.Address;
        }
    }
}