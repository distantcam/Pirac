namespace Pirac.Tests
{
    public class TestPiracContext : PiracContext
    {
        public TestPiracContext()
        {
            this.WindowManager = new TestWindowManager();
        }
    }

    public class TestWindowManager : IWindowManager
    {
        public bool? ShowDialog(object viewModel)
        {
            return null;
        }

        public bool? ShowDialog<TViewModel>()
        {
            return null;
        }

        public void ShowWindow(object viewModel)
        {
        }

        public void ShowWindow<TViewModel>()
        {
        }
    }

    public class TestViewModel : ViewModelBase { }

    public class TestView { }
}