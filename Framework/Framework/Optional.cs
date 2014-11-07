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
}
