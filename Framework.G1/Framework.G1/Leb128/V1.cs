using System;
using System.Collections.Generic;

namespace Framework.G1.Leb128
{
    public sealed class V1: ICompression<ulong>
    {
        public static readonly V1 Value = new V1();

        public IEnumerable<byte> Encode(ulong value)
        {
            while (Flag <= value)
            {
                yield return (byte)(Flag | value);
                value >>= Offset;
            }
            yield return (byte)value;
        }

        public ulong Decode(Func<byte> getNextByte)
        {
            var result = 0ul;
            var c = 1ul;
            while (true)
            {
                var b = getNextByte();
                result += b * c;
                if (b < Flag)
                {
                    return result;
                }
                c *= Flag;
                result -= c;
            }
        }

        private V1()
        {
        }

        internal const int Offset = 7;

        internal const byte Flag = 0x80;
    }
}
