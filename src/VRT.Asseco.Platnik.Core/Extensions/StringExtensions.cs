namespace VRT.Asseco.Platnik.Extensions
{
    internal static class StringExtensions
    {
        public static bool IsNotEmpty(this string value)
        {
            return value.IsEmpty().Not();
        }
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
