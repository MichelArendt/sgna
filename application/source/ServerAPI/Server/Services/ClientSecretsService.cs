using Microsoft.Extensions.Configuration;
using ServerAPI.Server.Models.Settings;

namespace ServerAPI.Server.Services
{
    public class ClientSecretsService
    {
        public ClientSecretsService(IConfiguration configuration)
        {
            //Configuration = configuration;
            ClientSecrets = configuration.GetSection("OpenId").Get<ClientSecretsSettingsModel>();
        }

        //private IConfiguration Configuration { get; }
        public ClientSecretsSettingsModel ClientSecrets { get; set; }
    }
}
