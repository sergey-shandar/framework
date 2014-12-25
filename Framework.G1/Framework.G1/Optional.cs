using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.G1
{
    public abstract partial class Optional
    {
        public abstract TResult Apply<TResult>(ISwitch<TResult> switch_);

        public abstract bool HasValue { get; }

        internal Optional()
        {
        }
    }

    public abstract partial class Optional<T>: Optional, IEquatable<Optional>
    {
        public abstract bool Equals(Optional other);

        public abstract override int GetHashCode();

        public abstract TResult Apply<TResult>(ISwitch<TResult> switch_);

        public override TResult Apply<TResult>(Optional.ISwitch<TResult> switch_)
        {
            return switch_.Case(this);
        }

        public override bool Equals(object obj)
        {
            var optional = obj as Optional;
            return !optional.IsNull() && Equals(optional);
        }

        public override bool HasValue
        {
            get { return Select(value => true, () => false); }
        }

        public TResult Select<TResult>(
            Func<T, TResult> hasValue, Func<TResult> hasNoValue)
        {
            return Apply(new SwitchSelect<TResult>(hasValue, hasNoValue));
        }

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

        private Optional()
        {            
        }
    }
}
