using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.G1
{
    public static class OptionalClassExtensions
    {
        public static IEnumerable<T> SelectMany<T>(
            this Optional.Class<IEnumerable<T>> optional)
        {
            return optional.Cast().SelectMany();
        }
    }
}
