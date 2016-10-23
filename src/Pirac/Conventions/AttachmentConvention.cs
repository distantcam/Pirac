using System;
using System.Linq;

namespace Pirac.Conventions
{
    internal class AttachmentConvention : IConvention
    {
        public bool Filter(Type type)
        {
            return type.Name.EndsWith("Attachment");
        }

        public void Verify(Type type)
        {
            if (type.IsAbstract)
            {
                throw new ConventionBrokenException($"Attachment type '{type}' must be a concrete class.");
            }
            if (!type.GetInterfaces().Any(t => t == typeof(IAttachment)))
            {
                throw new ConventionBrokenException($"Attachment type '{type}' must implement '{typeof(IAttachment)}'.");
            }
        }

        public string BaseName(Type type)
        {
            if (type.Name.Length < 10)
            {
                return string.Empty;
            }

            return type.Name.Substring(0, type.Name.Length - 10);
        }

        public bool IsVariant(Type type, string basename)
        {
            return Filter(type) && type.Name.StartsWith(basename);
        }
    }
}