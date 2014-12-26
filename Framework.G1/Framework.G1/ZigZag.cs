using System;
using System.Collections.Generic;

namespace Framework.G1
{
    public sealed class ZigZag: ICompression<long>
    {
        public ZigZag(ICompression<ulong> unsigned)
        {
            _unsigned = unsigned;
        }

        public static ulong ToUnsigned(long value)
        {
            return ((ulong)value << 1) ^ (ulong)(value >> 63);
        }

        public static long FromUnsigned(ulong value)
        {
            return (long)(value >> 1) ^ ((long)(value << 63) >> 63);
        }

        public IEnumerable<byte> Encode(long value)
        {
            return _unsigned.Encode(ToUnsigned(value));
        }

        public long Decode(Func<byte> getNextByte)
        {
            return FromUnsigned(_unsigned.Decode(getNextByte));
        }

        private readonly ICompression<ulong> _unsigned;
    }
}
