namespace VRT.Asseco.Platnik.Extensions;
internal static class EnumerableExtensions
{
    public static IEnumerable<T> NotEmpty<T>(this IEnumerable<T?> enumerable)
        where T : class
    {
        return enumerable.Where(t => t != null).Select(t => t!);
    }
}
