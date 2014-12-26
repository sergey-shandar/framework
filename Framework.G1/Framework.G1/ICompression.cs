using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.G1
{
    public interface ICompression<T>
    {
        IEnumerable<byte> Encode(T value);
        T Decode(Func<byte> getNextByte);
    }
}
