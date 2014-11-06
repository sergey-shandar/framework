namespace Framework
{
    public struct OptionalStruct<T> where T: struct
    {
        public OptionalStruct(T value)
        {
            _hasValue = true;
            _value = value;
        }

        public Optional<T> Cast()
        {
            return _hasValue ? Optional<T>.CreateNoValue() : _value.Optional();
        }

        private readonly bool _hasValue;
        private readonly T _value;
    }
}
