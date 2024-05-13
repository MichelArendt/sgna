using ServerAPI.Server.Models.Settings;
using SharedLibrary.Models.User.Login.OpenId;
using System;
using System.Web;

namespace ServerAPI.Server.Core.User.Login.OpenId
{
    public class OpenIdLoginuserServerCore : LoginUserServerCore
    {
        public OpenIdLoginuserServerCore(string state, OpenIdSettingsModel openIdSettingsModel)
        {
            this.State = state;
            this.OpenIdSettingsModel = openIdSettingsModel;
        }

        OpenIdSettingsModel OpenIdSettingsModel;
        private readonly string State;
        private readonly string? LoginHint; // TODO

        public string BuildUri(OpenIdProviderUriSettingsModel openIdUriSettings)
        {
            string uri = "";

            uri += openIdUriSettings.Endpoint;
            uri += "client_id=" + openIdUriSettings.ClientId;
            uri += "&response_type=code";
            uri += "&scope=" + String.Join("%20", openIdUriSettings.Scopes);
            uri += "&redirect_uri=" + openIdUriSettings.RedirectUri;
            uri += "&state=" + "login_token%3D" + HttpUtility.UrlEncode(this.State);
            if (!String.IsNullOrEmpty(this.LoginHint))
            {
                uri += "&login_hint=" + this.LoginHint;
            }

            return uri;
        }

        public string BuildOpenIdUriFromProvider(OpenIdProviderUriSettingsModel openIdUriSettings)
        {
            return this.BuildUri(openIdUriSettings);
        }

        public OpenIdUrisModel? BuildOpenIdUris()
        {
            OpenIdUrisModel openIdUrisModel = new OpenIdUrisModel
            {
                Facebok = (OpenIdSettingsModel.Facebook == null) ? null : this.BuildOpenIdUriFromProvider(this.OpenIdSettingsModel.Facebook),
                Google = (OpenIdSettingsModel.Google == null) ? null : this.BuildOpenIdUriFromProvider(this.OpenIdSettingsModel.Google),
                Twitter = (OpenIdSettingsModel.Twitter == null) ? null : this.BuildOpenIdUriFromProvider(this.OpenIdSettingsModel.Twitter),
                Microsoft = (OpenIdSettingsModel.Microsoft == null) ? null : this.BuildOpenIdUriFromProvider(this.OpenIdSettingsModel.Microsoft)
            };

            return openIdUrisModel;
        }
    }
}
