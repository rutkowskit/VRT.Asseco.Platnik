//Wzorowane na kodzie ze strony https://platnik.fork.pl

using System.Text;
using System.Text.RegularExpressions;

namespace Platnik.Lib
{
    public static class PlatnikHelpers
    {
        private static readonly Encoding CharsEncoding = CreateEncoding();
        private const int Base = 16;

        private static readonly string[] Keys = new string[]
        {
            "tuvwxyz{lmnopqrs",
            "qpsrmlonyx{zutwv",
            "utwvyx{zmlonqpsr",
            "mlonqpsrutwvyx{z",
            "pqrslmnoxyz{tuvw",
            "nolmrspqvwtuz{xy",
            "onmlsrqpwvut{zyx",
            "srqponml{zyxwvut"
        };

        private static readonly int[] Order =
        {
            0,1,2,3,4,0,3,5,
            2,1,5,4,3,6,6,2,
            4,2,2,4,3,2,7,7
        };

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
                result.Append(key[aIndex]).Append(key[bIndex]);
            }
            return result.ToString();
        }

        public static string DecodePassword(string encodedPassword)
        {
            encodedPassword = Regex.Replace(encodedPassword, $"[^{Regex.Escape(Keys[0])}]", "");
            var result = new StringBuilder();
            var pairMatch = Regex.Match(encodedPassword, ".{2}");
            var i = 0;
            while (pairMatch.Success)
            {
                string key = GetKey(i++);
                int aIndex = key.IndexOf(pairMatch.Value[0]);
                int bIndex = key.IndexOf(pairMatch.Value[1]);
                var decodedChar = (byte)(aIndex + (Base * bIndex));
                result.Append(CharsEncoding.GetString(new byte[] { decodedChar }));
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
}
