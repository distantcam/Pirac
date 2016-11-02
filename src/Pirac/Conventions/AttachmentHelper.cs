namespace Pirac.Conventions
{
    public static class AttachmentHelper
    {
        public static void Attach(object attachment, object t)
        {
            typeof(IAttachment<>)
                .MakeGenericType(t.GetType())
                .GetMethod(nameof(IAttachment<object>.AttachTo))
                .Invoke(attachment, new object[] { t });
        }
    }
}