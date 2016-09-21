using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace Pirac.Tests
{
    [TestFixture]
    public class PiracRunnerTests
    {
        public static IEnumerable GetGuardedMethods
        {
            get
            {
                return typeof(PiracRunner)
                    .GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(IsGuardedMethods)
                    .Select(CreateTestCase);
            }
        }

        public static IEnumerable GetUnguardedMethods
        {
            get
            {
                return typeof(PiracRunner)
                    .GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(IsUnguardedMethods)
                    .Select(CreateTestCase);
            }
        }

        [Test]
        [RunInApplicationDomain]
        [TestCaseSource(typeof(PiracRunnerTests), nameof(GetGuardedMethods))]
        public void HasGuard(MethodInfo method)
        {
            var ex = Assert.Throws<TargetInvocationException>(() => CallMethod(method));

            var innerException = ex.InnerException as InvalidOperationException;
            Assert.NotNull(innerException);

            Assert.AreEqual("Pirac has not been started.", innerException.Message);
        }

        [Test]
        [RunInApplicationDomain]
        [TestCaseSource(typeof(PiracRunnerTests), nameof(GetUnguardedMethods))]
        public void DoesNotHaveGuard(MethodInfo method)
        {
            CallMethod(method);
        }

        private static void CallMethod(MethodInfo method)
        {
            if (method.ContainsGenericParameters)
            {
                method = method.MakeGenericMethod(typeof(PiracRunnerTests));
            }

            if (method.GetParameters().Length == 0)
            {
                method.Invoke(null, null);
                return;
            }

            if (method.GetParameters().Length == 1)
            {
                method.Invoke(null, new object[] { null });
                return;
            }

            throw new NotImplementedException(method.Name);
        }

        private static bool IsInternalMethod(MethodInfo method)
            => method.Attributes.HasFlag(MethodAttributes.Assembly);

        private static bool IsNamedMethod(MethodInfo method)
        {
            return method.Name == "Start" ||
                method.Name == "get_IsContextSet" ||
                method.Name == "EnsureContext";
        }

        private static bool IsGuardedMethods(MethodInfo method)
        {
            if (!IsInternalMethod(method))
                return false;

            return !IsNamedMethod(method);
        }

        private static bool IsUnguardedMethods(MethodInfo method)
        {
            if (!IsInternalMethod(method))
                return false;

            return IsNamedMethod(method);
        }

        private static TestCaseData CreateTestCase(MethodInfo method)
            => new TestCaseData(method).SetName(method.Name);
    }
}