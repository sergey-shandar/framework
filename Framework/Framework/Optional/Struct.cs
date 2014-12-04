using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework
{
    partial class Optional
    {
        public struct Struct<T> where T : struct
        {
            public Struct(T value)
            {
                _hasValue = true;
                _value = value;
            }

            public Optional<T> Cast()
            {
                return _hasValue ? new Optional<T>.NoValue() : _value.ToOptional();
            }

            private readonly bool _hasValue;
            private readonly T _value;
        }
    }
}
