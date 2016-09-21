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

        public void ShowWindow(object viewModel)
        {
        }
    }

    public class TestViewModel : BindableObject { }

    public class TestView { }
}