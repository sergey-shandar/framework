using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.G1
{
    public abstract partial class Optional: IEquatable<Optional>
    {
        public abstract bool Equals(Optional other);

        public abstract TResult Apply<TResult>(Switch<TResult> switch_);

        public abstract bool HasValue { get; }

        public abstract override string ToString();

        public abstract override bool Equals(object obj);

        public abstract override int GetHashCode();

        internal Optional()
        {
        }
    }

    public abstract partial class Optional<T>: Optional
    {
        public abstract TResult Apply<TResult>(Switch<TResult> switch_);

        public sealed override TResult Apply<TResult>(Optional.Switch<TResult> switch_)
        {
            return switch_.Case(this);
        }

        public abstract override int GetHashCode();

        public sealed override bool Equals(object obj)
        {
            var optional = obj as Optional;
            return !optional.IsNull() && Equals(optional);
        }

        public sealed override bool HasValue
        {
            get { return this.Select(value => true, false); }
        }

        private Optional()
        {            
        }
    }
}
