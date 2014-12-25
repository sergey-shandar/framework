using System.Collections.Generic;

namespace Framework.G1
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Enumerate<T>(this T value)
        {
            yield return value;
        }
    }
}
