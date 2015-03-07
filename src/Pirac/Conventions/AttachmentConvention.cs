using Conventional.Conventions;

namespace Pirac.Conventions
{
    public class AttachmentConvention : Convention
    {
        public AttachmentConvention()
        {
            Must.HaveNameEndWith("Attachment");

            Should.Implement<IAttachment>();

            BaseName = t => t.Name.Substring(0, t.Name.Length - 10);

            Variants.HaveBaseNameAndEndWith("Attachment");
        }
    }
}