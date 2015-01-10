using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.G1
{
    static partial class OptionalExtensions
    {
        private sealed class SwitchSelect<T, TResult> :
            Optional<T>.Switch<TResult>
        {
            public SwitchSelect(
                Func<T, TResult> hasValue, Func<TResult> hasNoValue)
            {
                _hasValue = hasValue;
                _hasNoValue = hasNoValue;
            }

            public override TResult Case(Optional<T>.Absent absent)
            {
                return _hasNoValue();
            }

            public override TResult Case(Optional<T>.Value value)
            {
                return _hasValue(value.V);
            }

            private readonly Func<T, TResult> _hasValue;

            private readonly Func<TResult> _hasNoValue;
        }
    }
}
