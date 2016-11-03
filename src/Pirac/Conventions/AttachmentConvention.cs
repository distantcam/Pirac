using System;
using System.Linq;

namespace Pirac.Conventions
{
    internal class AttachmentConvention : IConvention
    {
        private Func<Type, bool> attachmentFilter = t => t.GetGenericTypeDefinition() == typeof(IAttachment<>);

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
            if (!type.GetInterfaces().Any(attachmentFilter))
            {
                throw new ConventionBrokenException($"Attachment type '{type}' must implement '{typeof(IAttachment<>)}'.");
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

        public bool IsVariant(Type type, Type variant, string basename)
        {
            var attachmentTypes = type.GetInterfaces()
                .Where(attachmentFilter)
                .Select(t => t.GetGenericArguments()[0]);

            return Filter(type) && attachmentTypes.Any(t => t.IsAssignableFrom(variant));
        }
    }
}