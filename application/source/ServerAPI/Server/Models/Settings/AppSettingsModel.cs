#nullable disable

namespace ServerAPI.Server.Models.Settings
{
    public class AppSettingsModel
    {
        public string ApplicationName { get; set; }
        public string DefaultLanguage { get; set; }
        public Database Database { get; set; }
    }

    public class Database
    {
        public string Type { get; set; }
        public ConnectionString ConnectionString { get; set; }
    }

    public class ConnectionString
    {
        public string SQLServer { get; set; }
        public string MySQL { get; set; }
    }
}
