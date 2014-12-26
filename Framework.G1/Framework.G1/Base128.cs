using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.G1
{
    /// <summary>
    /// ULEB128
    /// http://en.wikipedia.org/wiki/LEB128
    /// </summary>
    public static class Base128
    {
        public static ulong FromBase128(this Func<byte> getNextByte)
        {
            var result = 0ul;
            var i = 0;
            while (true)
            {
                var b = (ulong)getNextByte();
                result |= ((b & Mask) << i);
                if (b < Flag)
                {
                    return result;
                }
                i += Offset;
            }
        }

        public static IEnumerable<byte> ToBase128(this ulong value)
        {
            while (true)
            {
                var b = (byte)(value & Mask);
                value >>= Offset;
                var next = value != 0;
                b |= next ? Flag : (byte)0;
                yield return b;
                if (!next)
                {
                    break;
                }
            }
        }

        private const byte Mask = 0x7F;

        private const int Offset = 7;

        private const byte Flag = 0x80;
    }
}
