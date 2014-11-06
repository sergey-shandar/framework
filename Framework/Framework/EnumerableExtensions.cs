using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Enumerate<T>(this T value)
        {
            yield return value;
        }
    }
}
