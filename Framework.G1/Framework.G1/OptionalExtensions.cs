namespace Framework.G1
{
    public static class OptionalExtensions
    {
        public static Optional<T> ToOptional<T>(this T value)
        {
            return new Optional<T>.Value(value);
        }
    }
}
