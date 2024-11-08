using System.Text;
using VRT.Asseco.Platnik.Helpers;

namespace VRT.Asseco.Platnik.Infos;

public sealed partial class PlatnikAppInfo
{
    /// <summary>
    /// Zwraca postać jawną zakodowanego hasła
    /// </summary>
    /// <param name="encodedPassword">Zakodowane hasło</param>
    /// <returns>Hasło w postaci jawnej</returns>
    public static string DecodePassword(string encodedPassword)
    {
        return PlatnikHelpers.DecodePassword(encodedPassword);
    }

    /// <summary>
    /// Zwraca postać zakodowaną hasła
    /// </summary>
    /// <param name="plainTextPassword">Hasło w postaci jawnej</param>
    /// <returns>Hasło w postaci zakodowanej</returns>
    public static string EncodePassword(string plainTextPassword)
    {
        return PlatnikHelpers.EncodePassword(plainTextPassword ?? "");
    }

    /// <summary>
    /// Kodowanie używane do konwersji znaków hasła
    /// </summary>
    public static Encoding PasswordEncoding => PlatnikHelpers.CharsEncoding;
}
