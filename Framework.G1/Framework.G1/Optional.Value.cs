using System;

namespace Framework.G1
{
    partial class Optional<T>
    {
        public sealed class Value : Optional<T>
        {
            public override TResult Apply<TResult>(Switch<TResult> switch_)
            {
                return switch_.Case(this);
            }

            public Value(T value)
            {
                V = value;
            }

            public override string ToString()
            {
                return "{" + V.ToString() + "}";
            }

            public override int GetHashCode()
            {
                return V.GetHashCode();
            }

            public readonly T V;

            public override bool Equals(Optional other)
            {
                return other.Apply(new EqualsSwitch(V));
            }

            private sealed class EqualsSwitch : Optional.Switch<bool>
            {
                public EqualsSwitch(T value)
                {
                    _value = value;
                }

                public override bool Case<TOther>(Optional<TOther> optional)
                {
                    return optional.Select(value => _value.Equals(value), false);
                }

                private readonly T _value;
            }
        }
    }
}
