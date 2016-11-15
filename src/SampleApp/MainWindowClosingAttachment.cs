using System.Windows;
using Pirac;

namespace SampleApp
{
    class MainWindowClosingAttachment : Attachment<MainWindowViewModel>
    {
        protected override void OnAttach()
        {
            viewModel.CanCloseCheck = ()
                => MessageBox.Show("Really quit?", "Quit?", MessageBoxButton.OKCancel) == MessageBoxResult.OK;
        }
    }
}