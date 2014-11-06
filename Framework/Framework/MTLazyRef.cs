using System.Threading;

namespace Framework
{
    public struct MTLazyRef<T>: ILazy<T> where T: class, new()
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
                if (Interlocked.CompareExchange(ref _value, MTLazyRef.Initializing, null).IsNull())
                {
                    var value = new T();
                    _value = value;
                    return value;
                }
                while (ReferenceEquals(_value, MTLazyRef.Initializing))
                {
                }
                return (T)_value;
            }
        }

        private object _value;
    }

    internal static class MTLazyRef
    {
        public static readonly object Initializing = new object();
    }
}
