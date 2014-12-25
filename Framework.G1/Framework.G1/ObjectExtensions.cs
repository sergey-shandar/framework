﻿namespace Framework.G1
{
    public static class ObjectExtensions
    {
        public static bool IsNull<T>(this T value)
            where T: class
        {
            return ReferenceEquals(value, null);
        }
    }
}
