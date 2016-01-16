using Conventional.Conventions;

namespace Pirac.Conventions
{
    internal class AttachmentConvention : Convention
    {
        public AttachmentConvention()
        {
            Must.HaveNameEndWith("Attachment");

            Should.BeAConcreteClass().Implement<IAttachment>();

            BaseName = t => t.Name.Substring(0, t.Name.Length - 10);

            Variants.HaveBaseNameAndEndWith("Attachment");
        }
    }
}