using VRT.Asseco.Platnik;
using VRT.Asseco.Platnik.Infos;

(string Encoded, string Expected)[] tests =
    [
        ("ux{w{snvtyxzqsppy{zomqlnprnlllyvzn{vxvxspnmvuq","Aęćłśddfdksdu134j65858."),
        ("r{xzvpnwwxsmzwyuvrrvomrsnnslrlpv{rrwqxtmzorwwn",@"~ąÓŁŹźŻżóó12345=+/\\//\"),
        ("mortvozuwyl{zvmyvrprlmssqnrlmpmy{onwpv{rzoqxwn",@"ąÓŁŹźxżóó12345rH{├=+/\\")
    ];

Console.WriteLine("Testy dekodowania");
foreach (var test in tests)
{
    var (encoded, expected) = test;
    var decoded = PlatnikAppInfo.DecodePassword(encoded);
    var status = string.Equals(expected, decoded)
        ? "OK"
        : "NOTOK";
    Console.WriteLine("[{1}] Zakodowane: {0}, Odkodowane: {2}, Spodziewane: {3}", encoded, status, decoded, expected);
}

Console.WriteLine();
Console.WriteLine("Testy kodowania");
foreach (var test in tests)
{
    var (expected, decoded) = test;
    var encoded = PlatnikAppInfo.EncodePassword(decoded);
    var status = string.Equals(expected, encoded)
        ? "OK"
        : "NOTOK";
    Console.WriteLine("[{1}] Zakodowane: {0}, Odkodowane: {2}, Spodzewane:{3}", encoded, status, decoded, expected);
}

var installedInfo = PlatnikAppInfo
    .FromRegistry()
    .GetLatest();

if (installedInfo != null)
{
    Console.WriteLine();
    Console.WriteLine("Wykryto zainstalowanego płatnika. {0}", installedInfo.ToString());
}