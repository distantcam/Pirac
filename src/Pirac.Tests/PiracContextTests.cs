using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Pirac.Tests
{
    [TestFixture]
    public class PiracContextTests

    {
        public static IEnumerable GetPropertiesToTest
        {
            get
            {
                return typeof(PiracContext)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Select(p => new TestCaseData((Action)(() => { p.SetValue(new PiracContext(), null); })).SetName(p.Name));
            }
        }

        [Test]
        [TestCaseSource(typeof(PiracContextTests), nameof(GetPropertiesToTest))]
        public void HasGuard(Action action)
        {
            var ex = Assert.Throws<TargetInvocationException>(() => action());

            var innerException = ex.InnerException as ArgumentNullException;
            Assert.NotNull(innerException);

            Assert.AreEqual($"cannot set a property of PiracContext to null.{Environment.NewLine}Parameter name: value", innerException.Message);
        }
    }
}