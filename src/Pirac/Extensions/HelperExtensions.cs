using System;
using System.Collections.Generic;
using System.Linq;

namespace Pirac.Extensions
{
    static class HelperExtensions
    {
        public static IEnumerable<T> ThrowIfEmpty<T>(this IEnumerable<T> source, Func<Exception> exception)
        {
            if (!source.Any())
                throw exception();
            return source;
        }
    }
}