using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework
{
    public struct OptionalClass<T>
        where T: class
    {
        public OptionalClass(T value)
        {
            _value = value;
        }

        public Optional<T> Cast()
        {
            return _value.IsNull() ? Optional<T>.CreateNoValue(): _value.Optional();
        }

        private readonly T _value;
    }
}
