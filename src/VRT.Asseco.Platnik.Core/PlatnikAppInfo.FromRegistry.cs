using Microsoft.Win32;
using VRT.Asseco.Platnik.Extensions;
using VRT.Asseco.Platnik.Helpers;

namespace VRT.Asseco.Platnik.Infos;

public sealed partial class PlatnikAppInfo
{
    /// <summary>
    /// Wczytuje listę wszystkich wersji Programu Płatnika z rejestru systemu Windows
    /// </summary>
    /// <returns>Lista informacji o każdej zainstalowanej wersji Programu Płatnika</returns>
    public static IEnumerable<PlatnikAppInfo> FromRegistry()
    {
        return GetVersionRegistryKeys()
            .Select(FromRegistryKey)
            .NotEmpty()
            .ToArray() ?? [];
    }
    private static IEnumerable<RegistryKey> GetVersionRegistryKeys()
    {
        var root = FindPlatnikRootKey();
        var subkeyNames = root?.GetSubKeyNames() ?? [];
        foreach (var subkey in subkeyNames)
        {
            var versionSubkey = root?.OpenSubKey(subkey);
            if (versionSubkey != null)
            {
                yield return versionSubkey;
            }
        }
    }

    private static PlatnikAppInfo? FromRegistryKey(RegistryKey? versionRegistryKey)
    {
        var adminSubKey = versionRegistryKey?.OpenSubKey("Admin");
        if (versionRegistryKey is null || adminSubKey is null)
        {
            return null;
        }
        var admValues = adminSubKey
            .GetValueNames()
            .Where(IsAdmKey)
            .NotEmpty()
            .Select(name => new KeyValuePair<int, string>(ToAdmIndex(name), PlatnikHelpers.DecodePassword(adminSubKey.GetValue(name)?.ToString() ?? "")))
            .Where(name => name.Key > 0 && name.Value.IsNotEmpty())
            .ToDictionary(k => k.Key, v => v.Value);

        return FromDictionary(versionRegistryKey.Name, admValues);
    }

    private static RegistryKey? FindPlatnikRootKey()
    {
        return Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Asseco Poland SA\\Płatnik")
            ?? Registry.LocalMachine.OpenSubKey("SOFTWARE\\Asseco Poland SA\\Płatnik");
    }
    private static int ToAdmIndex(string admKeyName)
    {
        return IsAdmKey(admKeyName) && int.TryParse(admKeyName[3..], out var result)
            ? result
            : -1;
    }
    private static bool IsAdmKey(string? keyName)
    {
        return keyName is not null && keyName.StartsWith("adm", StringComparison.InvariantCultureIgnoreCase);
    }
}
