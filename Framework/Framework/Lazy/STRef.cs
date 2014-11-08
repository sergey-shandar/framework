namespace Framework.Lazy
{
    public struct STRef<T>: ILazy<T> where T: class, new()
    {
        public bool IsValueCrated 
        {
            get
            {
                return !_value.IsNull();
            }
        }

        public T Value
        {
            get
            {
                if (!IsValueCrated)
                {
                    _value = new T();
                }
                return _value;
            }
        }

        private T _value;    
    }
}
