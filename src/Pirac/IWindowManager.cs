namespace Pirac
{
    public interface IWindowManager
    {
        bool? ShowDialog<TViewModel>();

        bool? ShowDialog(object viewModel);

        void ShowWindow<TViewModel>();

        void ShowWindow(object viewModel);
    }
}