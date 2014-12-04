namespace Framework
{
    public interface ILazy<T> where T: class
    {
        bool IsValueCrated { get; }
        T Value { get; }
    }
}
