# Narzędzia Programu Płatnika firmy Asseco

Biblioteka zawiera zestaw narzędzi dla `Program Płatnika` firmy Asseco.

## Dostępne funkcjonalności

### Informacja o Programie Płatnika

Do przechowywania informacji o programie płatnika, służy klasa `PlatnikAppInfo`.

Klasa zawiera informacje dotyczące wersji `Programu Płatnik`.
Dane zawarte w klasie:

```csharp
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
```

Aby pobrać dane z rejestru systemu windows, należy wywołać funkcję statyczną:

```csharp
var installedVersions = PlatnikAppInfo.FromRegistry().ToArray();
```

Aby utworzyć informację o Programie Płatnika na podstawie danych z innego źródła, można przekazać listę wartości jako słownik wywołując funkcję:

```csharp
const string AppFullVersion = @"Computer\HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Asseco Poland SA\Płatnik\10.02.002";
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

### Kodowanie/dekodowanie hasła administratora

Aby zakodować hasło z postaci jawnej, do postaci rozpoznawanej przez Program płatnika można użyć funkcji statycznej:

```csharp
var plainTextPassword = "secret_password";
var encoded = PlatnikAppInfo.EncodePassword(plainTextPassword);
```

Aby odkodować hasło (lub inną wartość z wartości pobranych z rejestru rozpoczynających się na `Adm`) można użyć funkcji statycznej:

```csharp
var encodedPassword = "twyrpwlnqsqvonnmwvmr";
var decodedPassword = PlatnikAppInfo.DecodePassword(encodedPassword);
```

## Historia zmian

### Wersja 1.0.1
1. Utworzenie pierwszej wersji aplikacji
