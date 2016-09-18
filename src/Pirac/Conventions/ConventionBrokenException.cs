using System;
using System.Linq;

namespace Pirac.Conventions
{
    [Serializable]
    public class ConventionBrokenException : Exception
    {
        public ConventionBrokenException()
        {
        }

        public ConventionBrokenException(string message) : base(message)
        {
        }

        public ConventionBrokenException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ConventionBrokenException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}