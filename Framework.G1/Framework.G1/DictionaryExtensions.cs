using System.Collections.Generic;

namespace Framework.G1
{
    public static class DictionaryExtensions
    {
        public static Optional<TV> Get<TK, TV>(
            this IDictionary<TK, TV> dictionary, TK key)
        {
            TV value;
            return dictionary
                .TryGetValue(key, out value)
                .ThenCreateOptional(value);
        }
    }
}
