using System.Windows;

namespace SampleApp.Framework
{
    interface IDialogService
    {
        MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button);
    }

    class DialogService : IDialogService
    {
        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            return MessageBox.Show(messageBoxText, caption, button);
        }
    }
}