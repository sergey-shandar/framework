using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Policy
{
    interface INumeric<T>
    {
        T _0 { get; }
        T _1 { get; }
        T Add(T a, T b);
        T Subtract(T a, T b);
        T Mul(T a, T b);
        T Div(T a, T b);
    }
}
