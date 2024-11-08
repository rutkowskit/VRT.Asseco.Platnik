namespace VRT.Asseco.Platnik.Infos;

/// <summary>
/// Informacja o Programie Płatnik
/// </summary>
public sealed partial class PlatnikAppInfo
{
    /// <summary>
    /// Wartość pusta
    /// </summary>
    public static PlatnikAppInfo Empty { get; } = new();
    private PlatnikAppInfo() { }
    /// <summary>
    /// Imię Administratora
    /// </summary>
    public string AdminFirstName { get; private init; } = "";
    /// <summary>
    /// Nazwisko Administratora
    /// </summary>
    public string AdminLastName { get; private init; } = "";
    /// <summary>
    /// Aktualne hasło Administratora jawnym tekstem
    /// </summary>
    public string AdminCurrentPassword { get; private init; } = "";
    /// <summary>
    /// Data aktualizacji (zmienia się po zmianie hasła)
    /// </summary>
    public string AdminCurrentPasswordDate { get; private init; } = "";
    /// <summary>
    /// Wersja aplikacji (nazwa podklucza w rejestrze)
    /// </summary>
    public string AppVersion { get; private init; } = "";

    /// <summary>
    /// Pobiera opis ogólny informacji o Programie Płatnik
    /// </summary>
    /// <returns>Opis</returns>
    public override string ToString()
    {
        return $"Admin: {AdminFirstName} {AdminLastName}. Current Password: {AdminCurrentPassword}, Updated on: {AdminCurrentPasswordDate}, AppVersion: {AppVersion}";
    }
}
