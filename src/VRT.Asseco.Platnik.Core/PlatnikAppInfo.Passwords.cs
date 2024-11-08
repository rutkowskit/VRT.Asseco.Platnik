using VRT.Asseco.Platnik.Helpers;

namespace VRT.Asseco.Platnik.Infos
{
    public sealed partial class PlatnikAppInfo
    {
        public static string DecodePassword(string encodedPassword)
        {
            return PlatnikHelpers.DecodePassword(encodedPassword);
        }

        public static string EncodePassword(string plainTextPassword)
        {
            return PlatnikHelpers.EncodePassword(plainTextPassword);
        }
    }
}
