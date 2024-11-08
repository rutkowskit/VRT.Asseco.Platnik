# Narz�dzia Programu P�atnika firmy Asseco

Biblioteka zawiera zestaw narz�dzi dla `Program P�atnika` firmy Asseco.

## Dost�pne funkcjonalno�ci

### Informacja o Programie P�atnika

Do przechowywania informacji o programie p�atnika, s�u�y klasa `PlatnikAppInfo`.

Klasa zawiera informacje dotycz�ce wersji `Programu P�atnik`.
Dane zawarte w klasie:

```csharp
public sealed partial class PlatnikAppInfo
{
    /// <summary>
    /// Warto�� pusta
    /// </summary>
    public static PlatnikAppInfo Empty { get; } = new();
    private PlatnikAppInfo() { }
    /// <summary>
    /// Imi� Administratora
    /// </summary>
    public string AdminFirstName { get; private init; } = "";
    /// <summary>
    /// Nazwisko Administratora
    /// </summary>
    public string AdminLastName { get; private init; } = "";
    /// <summary>
    /// Aktualne has�o Administratora jawnym tekstem
    /// </summary>
    public string AdminCurrentPassword { get; private init; } = "";
    /// <summary>
    /// Data aktualizacji (zmienia si� po zmianie has�a)
    /// </summary>
    public string AdminCurrentPasswordDate { get; private init; } = "";
    /// <summary>
    /// Wersja aplikacji (nazwa podklucza w rejestrze)
    /// </summary>
    public string AppVersion { get; private init; } = "";

    /// <summary>
    /// Pobiera opis og�lny informacji o Programie P�atnik
    /// </summary>
    /// <returns>Opis</returns>
    public override string ToString()
    {
        return $"Admin: {AdminFirstName} {AdminLastName}. Current Password: {AdminCurrentPassword}, Updated on: {AdminCurrentPasswordDate}, AppVersion: {AppVersion}";
    }
}
```

Aby pobra� dane z rejestru systemu windows, nale�y wywo�a� funkcj� statyczn�:

```csharp
var installedVersions = PlatnikAppInfo.FromRegistry().ToArray();
```

Aby utworzy� informacj� o Programie P�atnika na podstawie danych z innego �r�d�a, mo�na przekaza� list� warto�ci jako s�ownik wywo�uj�c funkcj�:

```csharp
const string AppFullVersion = @"Computer\HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Asseco Poland SA\P�atnik\10.02.002";
var admValues = new Dictionary<string, string>()
{
    ["Adm1"] = "twyrpwlnqsqvonnmwvmr",
    ["Adm2"] = "xylmvxqpmlvy",
    ["Adm3"] = "xylmvxqpwl{ynpurly",
    ["Adm4"] = "ux{w{snvtyxzqsppy{zomqlnprnlllyvzn{vxvxspnmvuqrp",
    ["Adm5"] = "r{xzvpnwwxsmzwyuvrrvomrsnnslrlpv{rrwqxtmzorwwn"
};

var result = PlatnikAppInfo.FromDictionary(AppFullVersion, admValues);
```

### Kodowanie/dekodowanie has�a administratora

Aby zakodowa� has�o z postaci jawnej, do postaci rozpoznawanej przez Program p�atnika mo�na u�y� funkcji statycznej:

```csharp
var plainTextPassword = "secret_password";
var encoded = PlatnikAppInfo.EncodePassword(plainTextPassword);
```

Aby odkodowa� has�o (lub inn� warto�� z warto�ci pobranych z rejestru rozpoczynaj�cych si� na `Adm`) mo�na u�y� funkcji statycznej:

```csharp
var encodedPassword = "twyrpwlnqsqvonnmwvmr";
var decodedPassword = PlatnikAppInfo.DecodePassword(encodedPassword);
```

## Historia zmian

### Wersja 1.0.1
1. Utworzenie pierwszej wersji aplikacji
