using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework
{
    public abstract class Optional<T>
    {
        public abstract TR Select<TR>(Func<T, TR> hasValue, Func<TR> hasNoValue);

        public void Select(Action<T> hasValue, Action hasNoValue)
        {
            Select(
                value =>
                {
                    hasValue(value);
                    return new Void();
                },
                () =>
                {
                    hasNoValue();
                    return new Void();
                });
        }

        public void Select(Action<T> hasValue)
        {
            Select(hasValue, () => {});
        }

        public IEnumerable<T> ToEnumerable()
        {
            return Select(value => value.Enumerate(), Enumerable.Empty<T>);
        }

        public static Optional<T> CreateNoValue()
        {
            return new NoValue();
        }

        public sealed class NoValue: Optional<T>
        {
            public override TR Select<TR>(Func<T, TR> hasValue, Func<TR> hasNoValue)
            {
                return hasNoValue();
            }
        }

        public sealed class Value : Optional<T>
        {
            public Value(T value)
            {
                _value = value;
            }

            public override TR Select<TR>(Func<T, TR> hasValue, Func<TR> hasNoValue)
            {
                return hasValue(_value);
            }

            private readonly T _value;
        }
    }

    public static class Optional
    {
        public struct Class<T>
            where T : class
        {
            public Class(T value)
            {
                _value = value;
            }

            public Optional<T> Cast()
            {
                return _value.IsNull() ? Optional<T>.CreateNoValue() : _value.ToOptional();
            }

            private readonly T _value;
        }

        public struct Struct<T> where T : struct
        {
            public Struct(T value)
            {
                _hasValue = true;
                _value = value;
            }

            public Optional<T> Cast()
            {
                return _hasValue ? Optional<T>.CreateNoValue() : _value.ToOptional();
            }

            private readonly bool _hasValue;
            private readonly T _value;
        }

        public static Optional<T> ToOptional<T>(this T value)
        {
            return new Optional<T>.Value(value);
        }

    }
}
