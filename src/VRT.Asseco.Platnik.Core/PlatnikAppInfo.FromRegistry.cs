using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using VRT.Asseco.Platnik.Extensions;
using VRT.Asseco.Platnik.Helpers;

namespace VRT.Asseco.Platnik.Infos
{
    public sealed partial class PlatnikAppInfo
    {
        public static IEnumerable<PlatnikAppInfo> FromRegistry()
        {
            var root = FindPlatnikRootKey();
            return root
                ?.GetSubKeyNames()
                .Select(name => root.OpenSubKey(name))
                .Select(versionKey => FromRegistryKey(versionKey))
                .Where(info => info != null)
                .ToArray() ?? Array.Empty<PlatnikAppInfo>();
        }

        private static PlatnikAppInfo FromRegistryKey(RegistryKey versionRegistryKey)
        {
            var adminSubKey = versionRegistryKey?.OpenSubKey("Admin");
            if (adminSubKey == null)
            {
                return null;
            }
            var admValues = adminSubKey
                .GetValueNames()
                .Where(name => name.StartsWith("Adm"))
                .Select(name => new
                {
                    Key = ToAdmIndex(name),
                    Value = PlatnikHelpers.DecodePassword(adminSubKey.GetValue(name)?.ToString())
                })
                .Where(name => name.Key > 0 && name.Value.IsNotEmpty())
                .OrderBy(n => n.Key)
                .ToArray();

            var result = new PlatnikAppInfo()
            {
                AppVersion = versionRegistryKey.Name.Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault()
            };

            foreach (var admValue in admValues)
            {
                if (admValue.Key == 1) result.AdminCurrentPasswordDate = admValue.Value;
                if (admValue.Key == 2) result.AdminFirstName = admValue.Value;
                if (admValue.Key == 3) result.AdminLastName = admValue.Value;
                else result.AdminCurrentPassword = admValue.Value;
            }
            return result;
        }

        private static RegistryKey FindPlatnikRootKey()
        {
            return Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Asseco Poland SA\\Płatnik")
                ?? Registry.LocalMachine.OpenSubKey("SOFTWARE\\Asseco Poland SA\\Płatnik");
        }
        private static int ToAdmIndex(string admKeyName)
        {
            if (admKeyName.IsEmpty() || admKeyName.StartsWith("Adm").Not())
            {
                return -1;
            }
            return int.TryParse(admKeyName.Substring(3), out var result)
                ? result
                : -2;
        }
    }
}
