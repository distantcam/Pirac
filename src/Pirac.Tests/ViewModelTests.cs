using NUnit.Framework;

namespace Pirac.Tests
{
    public class ViewModelTests
    {
        [Test]
        public void CanInstantiateViewModel()
        {
            var vm = new TestViewModel();
        }
    }
}