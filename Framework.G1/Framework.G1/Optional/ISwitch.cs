namespace Framework.G1
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
            TResult Case(Absent absent);
            TResult Case(Value value);
        }
    }
}
