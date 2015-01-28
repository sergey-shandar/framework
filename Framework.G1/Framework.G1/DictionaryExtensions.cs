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

        public static TV GetOrNew<TK, TV>(
            this IDictionary<TK, TV> dictionary, TK key)
            where TV: new()
        {
            return dictionary
                .Get(key)
                .Default(() =>
                {
                    var value = new TV();
                    dictionary.Add(key, value);
                    return value;
                });
        }
    }
}
