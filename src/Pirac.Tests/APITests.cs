using System.Runtime.CompilerServices;
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
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void PublicAPI()
        {
            PublicApiApprover.ApprovePublicApi("Pirac.dll");
        }
    }
}