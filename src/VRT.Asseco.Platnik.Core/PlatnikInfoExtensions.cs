using VRT.Asseco.Platnik.Infos;

namespace VRT.Asseco.Platnik;

/// <summary>
/// Rozszerzenia klasy PlatnikAppInfo
/// </summary>
public static class PlatnikInfoExtensions
{
    /// <summary>
    /// Wybiera informację o najnowszej wersji Programu Płatnik
    /// </summary>
    /// <param name="infos">Lista informacji o wersjach Programu Płatnik</param>
    /// <returns>Informacja o najnowszej wersji Programu Płatnik</returns>
    public static PlatnikAppInfo GetLatest(this IEnumerable<PlatnikAppInfo> infos)
    {
        return infos.OrderBy(i => i.AppVersion).LastOrDefault();
    }
}
