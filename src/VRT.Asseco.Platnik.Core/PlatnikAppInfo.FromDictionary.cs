using System.Diagnostics.CodeAnalysis;
using VRT.Asseco.Platnik.Extensions;
using VRT.Asseco.Platnik.Helpers;

namespace VRT.Asseco.Platnik.Infos;

public sealed partial class PlatnikAppInfo
{
    /// <summary>
    /// Tworzy informację o Programie Płatnik na podstawie wartości ze słownika.
    /// </summary>
    /// <param name="appVersion">Wersja Programu Płatnik</param>
    /// <param name="adminValues">Wartości odwzoruwujące wpisy w kluczu Admin rejestru systemu Windows</param>
    /// <returns>Informacja o Programie Płatnik</returns>
    public static PlatnikAppInfo FromDictionary([NotNull] string appVersion, [NotNull] IReadOnlyDictionary<string, string> adminValues)
    {
        var convertedAdminValues = adminValues
            .Where(kv => IsAdmKey(kv.Key))
            .Select(kv => new KeyValuePair<int, string>(ToAdmIndex(kv.Key), PlatnikHelpers.DecodePassword(kv.Value)))
            .Where(name => name.Key > 0 && name.Value.IsNotEmpty())
            .ToDictionary(k => k.Key, v => v.Value);
        return FromDictionary(appVersion, convertedAdminValues) ?? Empty;
    }

    private static PlatnikAppInfo FromDictionary(string appVersion, [NotNull] IReadOnlyDictionary<int, string> adminValues)
    {
        var result = new PlatnikAppInfo()
        {
            AppVersion = appVersion.Split(['\\', '/'], StringSplitOptions.RemoveEmptyEntries).LastOrDefault(),
            AdminCurrentPasswordDate = adminValues.GetValueOrDefault(1, "")!,
            AdminFirstName = adminValues.GetValueOrDefault(2, "")!,
            AdminLastName = adminValues.GetValueOrDefault(3, "")!,
            AdminCurrentPassword = adminValues.Where(k => k.Key > 3).OrderBy(k => k.Key).Select(k => k.Value).LastOrDefault() ?? ""
        };
        return result;
    }
}
