namespace Pirac.Tests.ConventionTests
{
    using Conventions;
    using NUnit.Framework;

    [TestFixture]
    public class AttachmentConventionTests
    {
        AttachmentConvention convention = new AttachmentConvention();

        [Test]
        public void Filter_InterfaceAttachment()
        {
            var result = convention.Filter(typeof(InterfaceAttachment));
            Assert.IsTrue(result);
        }

        [Test]
        public void Filter_SimpleAttachment()
        {
            var result = convention.Filter(typeof(SimpleAttachment));
            Assert.IsTrue(result);
        }

        [Test]
        public void Filter_InheritedAttachment()
        {
            var result = convention.Filter(typeof(SimpleAttachment));
            Assert.IsTrue(result);
        }

        [Test]
        public void Filter_NotCorrectAttachmentName()
        {
            var result = convention.Filter(typeof(NotCorrectAttachmentName));
            Assert.IsFalse(result);
        }

        [Test]
        public void Verify_InterfaceAttachment()
        {
            convention.Verify(typeof(InterfaceAttachment));
        }

        [Test]
        public void Verify_SimpleAttachment()
        {
            convention.Verify(typeof(SimpleAttachment));
        }

        [Test]
        public void Verify_InheritedAttachment()
        {
            convention.Verify(typeof(InheritedAttachment));
        }

        [Test]
        public void IsVariant()
        {
            Assert.IsTrue(convention.IsVariant(typeof(TestAttachment), typeof(TestViewModel), "Test"));
        }
    }

    public class InterfaceAttachment : IAttachment<object>
    {
        public void AttachTo(object obj)
        {
        }
    }

    public class SimpleAttachment : Attachment<object>
    {
        protected override void OnAttach()
        {
        }
    }

    public class InheritedAttachment : SimpleAttachment
    {
    }

    public class NotCorrectAttachmentName : Attachment<object>
    {
        protected override void OnAttach()
        {
        }
    }
}