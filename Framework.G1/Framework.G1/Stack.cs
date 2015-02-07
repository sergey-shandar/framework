namespace Framework.G1
{
    abstract class Stack<T>
    {
        public abstract class Switch<TR>
        {
            public abstract TR Case(Empty empty);
            public abstract TR Case(One one);
        }

        public abstract TR Apply<TR>(Switch<TR> switch_);

        public sealed class Empty: Stack<T>
        {
            public override TR Apply<TR>(Switch<TR> switch_)
            {
                return switch_.Case(this);
            }
        }

        public sealed class One: Stack<T>
        {
            public override TR Apply<TR>(Switch<TR> switch_)
            {
                return switch_.Case(this);
            }
        }

        private Stack() { }
    }
}
