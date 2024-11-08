using System.Collections.Generic;
using System.Linq;
using VRT.Asseco.Platnik.Infos;

namespace VRT.Asseco.Platnik
{
    public static class PlatnikInfoExtensions
    {
        public static PlatnikAppInfo GetLatest(this IEnumerable<PlatnikAppInfo> infos)
        {
            return infos.OrderBy(i => i.AppVersion).LastOrDefault();
        }
    }
}
