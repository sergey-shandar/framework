namespace Framework
{
    partial class Optional
    {
        public struct Class<T> where T : class
        {
            public Class(T value)
            {
                _value = value;
            }

            public Optional<T> Cast()
            {
                return _value.IsNull() ? new Optional<T>.NoValue() : _value.ToOptional();
            }

            private readonly T _value;
        }
    }
}
