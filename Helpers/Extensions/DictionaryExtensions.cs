namespace Helpers.Extensions;

public static class DictionaryExtensions
{
    public static void RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> source,
        Predicate<KeyValuePair<TKey, TValue>> predicate)
    {
        foreach (var pair in source)
            if (predicate(pair))
                source.Remove(pair.Key);
    }
}
