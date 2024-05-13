using Microsoft.Extensions.Configuration;
using ServerAPI.Server.Models.Settings;

namespace ServerAPI.Server.Services
{
    public class OpenIdSettingsService
    {
        public OpenIdSettingsService(IConfiguration configuration)
        {
            //Configuration = configuration;
            OpenIdSettings = configuration.GetSection("OpenId").Get<OpenIdSettingsModel>();
        }

        //private IConfiguration Configuration { get; }
        public OpenIdSettingsModel OpenIdSettings { get; set; }
    }
}
