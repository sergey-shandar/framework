using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.G1
{
    public static partial class OptionalExtensions
    {
        public static IEnumerable<T> SelectMany<T>(
            this Optional<IEnumerable<T>> optional)
        {
            return optional.Select(v => v, Enumerable.Empty<T>);
        }

        public static TResult Select<T, TResult>(
            this Optional<T> optional,
            Func<T, TResult> hasValue,
            Func<TResult> hasNoValue)
        {
            return optional.Apply(
                new SwitchSelect<T, TResult>(hasValue, hasNoValue));
        }

        public static TResult Select<T, TResult>(
            this Optional<T> optional,
            Func<T, TResult> hasValue,
            TResult defaultResult)
        {
            return optional.Select(hasValue, () => defaultResult);
        }

        public static void Select<T>(
            this Optional<T> optional, Action<T> hasValue, Action hasNoValue)
        {
            optional.Select(
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

        public static void ForEach<T>(
            this Optional<T> optional, Action<T> hasValue)
        {
            optional.Select(hasValue, () => { });
        }

        public static T Default<T>(this Optional<T> optional, Func<T> create)
        {
            return optional.Select(value => value, create);
        }

        public static T Default<T>(this Optional<T> optional, T value)
        {
            return optional.Default(() => value);
        }

        public static bool ValueEqual<T>(this Optional<T> optional, T value)
        {
            return optional.Select(v => v.Equals(value), false);
        }

        public static IEnumerable<T> ToEnumerable<T>(this Optional<T> optional)
        {
            return optional.Select(
                value => value.Enumerate(), Enumerable.Empty<T>);
        }

        public static IEnumerable<T> SelectOnlyValues<T>(
            this IEnumerable<Optional<T>> source)
        {
            return source.SelectMany(o => o.ToEnumerable());
        }
    }
}
