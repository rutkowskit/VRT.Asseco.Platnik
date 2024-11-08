//Wzorowane na kodzie ze strony https://platnik.fork.pl

using System.Text;
using System.Text.RegularExpressions;

namespace VRT.Asseco.Platnik.Helpers;

internal static class PlatnikHelpers
{
    internal static readonly Encoding CharsEncoding = CreateEncoding();
    private const int Base = 16;

    private static readonly string[] Keys =
    [
        "tuvwxyz{lmnopqrs",
        "qpsrmlonyx{zutwv",
        "utwvyx{zmlonqpsr",
        "mlonqpsrutwvyx{z",
        "pqrslmnoxyz{tuvw",
        "nolmrspqvwtuz{xy",
        "onmlsrqpwvut{zyx",
        "srqponml{zyxwvut"
    ];

    private static readonly int[] Order =
    [
        0,1,2,3,4,0,3,5,
        2,1,5,4,3,6,6,2,
        4,2,2,4,3,2,7,7
    ];

    public static string EncodePassword(string password)
    {
        var result = new StringBuilder();
        var passwordBytes = CharsEncoding.GetBytes(password);
        for (var i = 0; i < password.Length; i++)
        {
            var passwordCharOrd = passwordBytes[i];
            var aIndex = passwordCharOrd % Base;
            var bIndex = passwordCharOrd / Base % Base;
            var key = GetKey(i);
            _ = result.Append(key[aIndex]).Append(key[bIndex]);
        }
        return result.ToString();
    }

    public static string DecodePassword(string encodedPassword)
    {
        // Return empty string if encoded password is invalid
        if (Regex.IsMatch(encodedPassword, $"[^{Regex.Escape(Keys[0])}]"))
        {
            return "";
        }
        var result = new StringBuilder();
        var pairMatch = Regex.Match(encodedPassword, ".{2}");
        var i = 0;
        while (pairMatch.Success)
        {
            string key = GetKey(i++);
            int aIndex = key.IndexOf(pairMatch.Value[0]);
            int bIndex = key.IndexOf(pairMatch.Value[1]);
            var decodedChar = (byte)(aIndex + (Base * bIndex));
            _ = result.Append(CharsEncoding.GetString([decodedChar]));
            pairMatch = pairMatch.NextMatch();
        }
        return result.ToString();
    }

    private static string GetKey(int characterIndex)
    {
        return Keys[Order[characterIndex % Order.Length]];
    }

    private static Encoding CreateEncoding()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        return Encoding.GetEncoding("windows-1250");
    }
}
