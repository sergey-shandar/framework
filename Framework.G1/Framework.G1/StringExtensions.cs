using System.Collections.Generic;
using System.Linq;

namespace Framework.G1
{
    public static class StringExtensions
    {
        public static bool StartsWith(this string s, char c, int offset = 0)
        {
            return s.Length > offset && s[offset] == c;
        }

        public static Split Split(this string s, int index)
        {
            return index == -1 
                ? new Split(s, string.Empty) 
                : new Split(s.Substring(0, index), s.Substring(index + 1));
        }

        /// <summary>
        /// TODO: check wich version is faster, this one or s.Cast[char]()
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static IEnumerable<char> AsEnumerable(this string s)
        {
            for (var i = 0; i < s.Length; ++i)
            {
                yield return s[i];
            }
        }
    }
}
