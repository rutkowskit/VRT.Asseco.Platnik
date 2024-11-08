# Narzêdzia Programu P³atnika firmy Asseco

Biblioteka zawiera zestaw narzêdzi dla `Program P³atnika` firmy Asseco.

## Dostêpne funkcjonalnoœci

### Informacja o Programie P³atnika

Do przechowywania informacji o programie p³atnika, s³u¿y klasa `PlatnikAppInfo`.

Klasa zawiera informacje dotycz¹ce wersji `Programu P³atnik`.
Dane zawarte w klasie:

```csharp
public sealed partial class PlatnikAppInfo
{
    /// <summary>
    /// Wartoœæ pusta
    /// </summary>
    public static PlatnikAppInfo Empty { get; } = new();
    private PlatnikAppInfo() { }
    /// <summary>
    /// Imiê Administratora
    /// </summary>
    public string AdminFirstName { get; private init; } = "";
    /// <summary>
    /// Nazwisko Administratora
    /// </summary>
    public string AdminLastName { get; private init; } = "";
    /// <summary>
    /// Aktualne has³o Administratora jawnym tekstem
    /// </summary>
    public string AdminCurrentPassword { get; private init; } = "";
    /// <summary>
    /// Data aktualizacji (zmienia siê po zmianie has³a)
    /// </summary>
    public string AdminCurrentPasswordDate { get; private init; } = "";
    /// <summary>
    /// Wersja aplikacji (nazwa podklucza w rejestrze)
    /// </summary>
    public string AppVersion { get; private init; } = "";

    /// <summary>
    /// Pobiera opis ogólny informacji o Programie P³atnik
    /// </summary>
    /// <returns>Opis</returns>
    public override string ToString()
    {
        return $"Admin: {AdminFirstName} {AdminLastName}. Current Password: {AdminCurrentPassword}, Updated on: {AdminCurrentPasswordDate}, AppVersion: {AppVersion}";
    }
}
```

Aby pobraæ dane z rejestru systemu windows, nale¿y wywo³aæ funkcjê statyczn¹:

```csharp
var installedVersions = PlatnikAppInfo.FromRegistry().ToArray();
```

Aby utworzyæ informacjê o Programie P³atnika na podstawie danych z innego Ÿród³a, mo¿na przekazaæ listê wartoœci jako s³ownik wywo³uj¹c funkcjê:

```csharp
const string AppFullVersion = @"Computer\HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Asseco Poland SA\P³atnik\10.02.002";
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

### Kodowanie/dekodowanie has³a administratora

Aby zakodowaæ has³o z postaci jawnej, do postaci rozpoznawanej przez Program p³atnika mo¿na u¿yæ funkcji statycznej:

```csharp
var plainTextPassword = "secret_password";
var encoded = PlatnikAppInfo.EncodePassword(plainTextPassword);
```

Aby odkodowaæ has³o (lub inn¹ wartoœæ z wartoœci pobranych z rejestru rozpoczynaj¹cych siê na `Adm`) mo¿na u¿yæ funkcji statycznej:

```csharp
var encodedPassword = "twyrpwlnqsqvonnmwvmr";
var decodedPassword = PlatnikAppInfo.DecodePassword(encodedPassword);
```

## Historia zmian

### Wersja 1.0.1
1. Utworzenie pierwszej wersji aplikacji
