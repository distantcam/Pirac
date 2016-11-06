using System;
using System.Collections.Generic;
using System.Linq;

namespace Pirac
{
    static class HelperExtensions
    {
        public static IEnumerable<T> ThrowIfEmpty<T>(this IEnumerable<T> source, Func<Exception> exception)
        {
            if (!source.Any())
                throw exception();
            return source;
        }

        public static T Value<T>(this WeakReference<T> item) where T : class
        {
            T v = null;
            item?.TryGetTarget(out v);
            return v;
        }
    }
}