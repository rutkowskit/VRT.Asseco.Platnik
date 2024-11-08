namespace VRT.Asseco.Platnik.Infos
{
    public sealed partial class PlatnikAppInfo
    {
        public static PlatnikAppInfo Empty { get; } = new PlatnikAppInfo();
        private PlatnikAppInfo() { }
        public string AdminFirstName { get; private set; }
        public string AdminLastName { get; private set; }
        public string AdminCurrentPassword { get; private set; }
        public string AdminCurrentPasswordDate { get; private set; }
        public string AppVersion { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "Admin: {0} {1}. Current Password: {2}, Updated on: {3}, AppVersion: {4}",
                AdminFirstName, AdminLastName, AdminCurrentPassword, AdminCurrentPasswordDate, AppVersion);
        }
    }
}
