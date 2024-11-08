namespace VRT.Asseco.Platnik.Extensions;
internal static class DictionaryExtensions
{
    public static TValue? GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dic, TKey key, TValue? defaultValue = default!)
    {
        return dic.TryGetValue(key, out TValue value) ? value : defaultValue;
    }
}
