using FluentAssertions;
using VRT.Asseco.Platnik.Infos;

namespace VRT.Asseco.Platnik.Core.Tests.Unit;

public class PlatnikAppInfoTests
{
    private const string AppFullVersion = @"Computer\HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Asseco Poland SA\Płatnik\10.02.002";
    private const string AppShortVersion = "10.02.002";

    [Theory]
    [InlineData("", "")]
    [InlineData("ux{w{snvtyxzqsppy{zomqlnprnlllyvzn{vxvxspnmvuq", "Aęćłśddfdksdu134j65858.")]
    [InlineData("r{xzvpnwwxsmzwyuvrrvomrsnnslrlpv{rrwqxtmzorwwn", @"~ąÓŁŹźŻżóó12345=+/\\//\")]
    [InlineData("mortvozuwyl{zvmyvrprlmssqnrlmpmy{onwpv{rzoqxwn", @"ąÓŁŹźxżóó12345rH{├=+/\\")]
    public void DecodePasswordTests(string encodedPassword, string expectedDecodedPassword)
    {
        var decodedPassword = PlatnikAppInfo.PasswordEncoding.GetBytes(PlatnikAppInfo.DecodePassword(encodedPassword));
        var expectedDecodedPasswordBytes = PlatnikAppInfo.PasswordEncoding.GetBytes(expectedDecodedPassword);

        decodedPassword.Should().BeEquivalentTo(expectedDecodedPasswordBytes);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("ux{w{snvtyxzqsppy{zomqlnprnlllyvzn{vxvxspnmvuq", "Aęćłśddfdksdu134j65858.")]
    [InlineData("r{xzvpnwwxsmzwyuvrrvomrsnnslrlpv{rrwqxtmzorwwn", @"~ąÓŁŹźŻżóó12345=+/\\//\")]
    [InlineData("mortvozuwyl{zvmyvrprlmssqnrlmpmy{onwpv{rzoqxwn", @"ąÓŁŹźxżóó12345rH{├=+/\\")]
    public void EncodePasswordTests(string expectedEncodedPassword, string plainTextPassword)
    {
        var encodedPassword = PlatnikAppInfo.EncodePassword(plainTextPassword);
        encodedPassword.Should().Be(expectedEncodedPassword);
    }


    [Fact]
    public void FromDictionary_WhenPassedCorrectValues_ShouldReturnCorrectPlatnikAppInfo()
    {
        // Arrange
        var expected = new
        {
            AppVersion = AppShortVersion,
            FirstName = "TESTER",
            LastName = "TESTOWSKI",
            CurrentPassword = @"~ąÓŁŹźŻżóó12345=+/\\//\",
            CurrentPasswordDate = "08-11-2024"
        };
        var admValues = new Dictionary<string, string>()
        {
            ["Adm1"] = "twyrpwlnqsqvonnmwvmr",
            ["Adm2"] = "xylmvxqpmlvy",
            ["Adm3"] = "xylmvxqpwl{ynpurly",
            ["Adm4"] = "ux{w{snvtyxzqsppy{zomqlnprnlllyvzn{vxvxspnmvuqrp",
            ["Adm5"] = "r{xzvpnwwxsmzwyuvrrvomrsnnslrlpv{rrwqxtmzorwwn"
        };

        // Act
        var result = PlatnikAppInfo.FromDictionary(AppFullVersion, admValues);

        // Assert
        result.AppVersion.Should().Be(expected.AppVersion);
        result.AdminFirstName.Should().Be(expected.FirstName);
        result.AdminLastName.Should().Be(expected.LastName);
        result.AdminCurrentPassword.Should().Be(expected.CurrentPassword);
        result.AdminCurrentPasswordDate.Should().Be(expected.CurrentPasswordDate);
    }

    [Fact]
    public void FromDictionary_WhenPassedInvalidValues_ShouldReturnEmptyAppInfo()
    {
        var admValues = new Dictionary<string, string>()
        {
            ["Adm1"] = "33",
            ["Adm2"] = "44",
            ["Adm3"] = "55",
            ["Adm4"] = "66",
            ["Adm5"] = ".."
        };

        // Act
        var result = PlatnikAppInfo.FromDictionary(AppFullVersion, admValues);

        // Assert
        result.AppVersion.Should().Be(AppShortVersion);
        result.AdminFirstName.Should().Be("");
        result.AdminLastName.Should().Be("");
        result.AdminCurrentPassword.Should().Be("");
        result.AdminCurrentPasswordDate.Should().Be("");
    }
}
