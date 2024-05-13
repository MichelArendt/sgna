#nullable disable

namespace ServerAPI.Server.Models.Settings
{
    public class ClientSecretsSettingsModel
    {
        public ClientSecretModel Facebook  { get; set; }
        public ClientSecretModel Google { get; set; }
        public ClientSecretModel Microsoft { get; set; }
        public ClientSecretModel Twitter { get; set; }
    }
}
