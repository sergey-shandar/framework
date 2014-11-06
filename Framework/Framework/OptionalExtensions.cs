namespace Framework
{
    public static class OptionalExtensions
    {
        public static Optional<T> Optional<T>(this T value)
        {
            return new Optional<T>.Value(value);
        }
    }
}
