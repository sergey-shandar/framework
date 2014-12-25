using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.G1
{
    partial class Optional<T>
    {
        private sealed class SwitchSelect<TResult>: ISwitch<TResult>
        {
            public SwitchSelect(Func<T, TResult> hasValue, Func<TResult> hasNoValue)
            {
                _hasValue = hasValue;
                _hasNoValue = hasNoValue;
            }

            public TResult Case(NoValue noValue)
            {
                return _hasNoValue();
            }

            public TResult Case(Value value)
            {
                return _hasValue(value.V);
            }

            private readonly Func<T, TResult> _hasValue;

            private readonly Func<TResult> _hasNoValue;
        }
    }
}
