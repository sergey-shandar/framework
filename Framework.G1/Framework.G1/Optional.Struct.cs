﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.G1
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
                return _hasValue.ThenCreateOptional(_value);
            }

            public static implicit operator Struct<T>(T value)
            {
                return new Struct<T>(value);
            }

            private readonly bool _hasValue;
            private readonly T _value;
        }
    }
}
