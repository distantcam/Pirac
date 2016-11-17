using System.Windows;
using Pirac;
using SampleApp.Framework;

namespace SampleApp
{
    class MainWindowClosingAttachment : Attachment<MainWindowViewModel>
    {
        IDialogService dialogService;

        public MainWindowClosingAttachment(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }

        protected override void OnAttach()
        {
            viewModel.CanCloseCheck = ()
                => dialogService.Show("Are you sure you want to quit?", "Quit?", MessageBoxButton.OKCancel) == MessageBoxResult.OK;
        }
    }
}