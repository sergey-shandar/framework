using System;
using System.Collections.Generic;

namespace Framework.G1.Leb128
{
    public sealed class V1: ICompression<ulong>
    {
        public IEnumerable<byte> Encode(ulong value)
        {
            while (value >= Flag)
            {
                yield return (byte)(Flag | value);
                value >>= Offset;
            }
            yield return (byte)value;
        }

        public ulong Decode(Func<byte> getNextByte)
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

        private const byte Mask = 0x7F;

        internal const int Offset = 7;

        internal const byte Flag = 0x80;
    }
}
