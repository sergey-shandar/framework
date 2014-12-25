using System;

namespace Framework.G1
{
    partial class Optional<T>
    {
        public sealed class NoValue : Optional<T>
        {
            public override string ToString()
            {
                return "{}";
            }

            public override int GetHashCode()
            {
                return 0;
            }

            public override bool Equals(Optional other)
            {
                return !other.HasValue;
            }

            public override TResult Apply<TResult>(ISwitch<TResult> switch_)
            {
                return switch_.Case(this);
            }
        }
    }
}
