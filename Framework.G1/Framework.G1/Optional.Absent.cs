using System;

namespace Framework.G1
{
    partial class Optional<T>
    {
        /// <summary>
        /// Any two Absent instances are equal, even for differnt T.
        /// </summary>
        public sealed class Absent : Optional<T>
        {
            public new static readonly Absent Value = new Absent();

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

            public override TResult Apply<TResult>(Switch<TResult> switch_)
            {
                return switch_.Case(this);
            }

            private Absent()
            {                
            }
        }
    }
}
