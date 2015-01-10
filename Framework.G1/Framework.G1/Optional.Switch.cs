namespace Framework.G1
{
    partial class Optional
    {
        public abstract class Switch<TResult>
        {
            public abstract TResult Case<T>(Optional<T> optional);
        }
    }

    partial class Optional<T>
    {
        public new abstract class Switch<TResult>
        {
            public abstract TResult Case(Absent absent);
            public abstract TResult Case(Value value);
        }
    }
}
