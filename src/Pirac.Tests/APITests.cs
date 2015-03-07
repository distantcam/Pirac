using ApiApprover;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Xunit;

namespace Pirac.Tests
{
    [UseReporter(typeof(DiffReporter))]
    [UseApprovalSubdirectory("results")]
    public class APITests
    {
        [Fact]
        public void PublicAPI()
        {
            PublicApiApprover.ApprovePublicApi("Pirac.dll");
        }
    }
}