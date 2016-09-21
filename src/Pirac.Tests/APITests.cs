using ApiApprover;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace Pirac.Tests
{
    [UseReporter(typeof(DiffReporter))]
    [UseApprovalSubdirectory("results")]
    [TestFixture]
    public class APITests
    {
        [Test]
        public void PublicAPI()
        {
            PublicApiApprover.ApprovePublicApi("Pirac.dll");
        }
    }
}