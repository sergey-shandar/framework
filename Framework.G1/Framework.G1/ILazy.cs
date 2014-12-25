namespace Framework.G1
{
    public interface ILazy<T> where T: class
    {
        bool IsValueCrated { get; }
        T Value { get; }
    }
}
