#nullable disable

using System.Collections.Generic;

namespace ServerAPI.Server.Models.Settings
{
    public class OpenIdSettingsModel
    {
        public OpenIdProviderUriSettingsModel Facebook { get; set; }
        public OpenIdProviderUriSettingsModel Google { get; set; }
        public OpenIdProviderUriSettingsModel Microsoft { get; set; }
        public OpenIdProviderUriSettingsModel Twitter { get; set; }
    }

    public class OpenIdProviderUriSettingsModel
    {
        public string Endpoint { get; set; }
        public string ClientId { get; set; }
        public string RedirectUri { get; set; }
        public List<string> Scopes { get; set; }
    }
}
