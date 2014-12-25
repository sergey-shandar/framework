using System.Threading;

namespace Framework.G1.Lazy
{
    public struct MTRef<T>: ILazy<T> where T: class, new()
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
                if (Interlocked.CompareExchange(ref _value, MTRef.Initializing, null).IsNull())
                {
                    var value = new T();
                    _value = value;
                    return value;
                }
                while (ReferenceEquals(_value, MTRef.Initializing))
                {
                }
                return (T)_value;
            }
        }

        private object _value;
    }

    internal static class MTRef
    {
        public static readonly object Initializing = new object();
    }
}
