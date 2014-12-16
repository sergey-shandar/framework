namespace Framework
{
    partial class Optional
    {
        public interface ISwitch<out TResult>
        {
            TResult Case<T>(Optional<T> optional);
        }
    }

    partial class Optional<T>
    {
        public new interface ISwitch<out TResult>
        {
            TResult Case(NoValue noValue);
            TResult Case(Value value);
        }
    }
}
