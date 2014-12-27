using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.G1
{
    public static class OptionalExtensions
    {
        public static Optional<T>.Value ToOptional<T>(this T value)
        {
            return new Optional<T>.Value(value);
        }

        public static Optional.Class<T> ToOptionalClass<T>(this T value)
            where T : class
        {
            return new Optional.Class<T>(value);
        }

        public static Optional.Struct<T> ToOptionalStruct<T>(this T value)
            where T: struct
        {
            return new Optional.Struct<T>(value);
        }

        public static IEnumerable<T> SelectMany<T>(
            this Optional<IEnumerable<T>> optional)
        {
            return optional.Select(v => v, Enumerable.Empty<T>);
        }

        public static IEnumerable<T> SelectMany<T>(
            this Optional.Class<IEnumerable<T>> optional)
        {
            return optional.Cast().SelectMany();
        }

        public static Optional<T> ThenCreateOptional<T>(this bool condition, Func<T> create)
        {
            return condition ? 
                create().ToOptional().UpCast<Optional<T>>() :
                Optional<T>.Absent.Value;
        }

        public static Optional<T> ThenCreateOptional<T>(this bool condition, T value)
        {
            return condition.ThenCreateOptional(() => value);
        }
    }
}
