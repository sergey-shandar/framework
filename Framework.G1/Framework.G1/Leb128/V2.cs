using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.G1.Leb128
{
    public sealed class V2: ICompression<ulong>
    {
        public IEnumerable<byte> Encode(ulong value)
        {
            while (V1.Flag <= value)
            {
                yield return (byte)(V1.Flag | value);
                value = (value >> V1.Offset) - 1;
            }
            yield return (byte)value;
        }

        public ulong Decode(Func<byte> getNextByte)
        {
            ulong result = 0;
            ulong c = 1;
            while (true)
            {
                var b = getNextByte();
                result += b * c;
                if (b < V1.Flag)
                {
                    break;
                }
                c *= V1.Flag;
            }
            return result;
        }
    }
}
