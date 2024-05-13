using Microsoft.AspNetCore.WebUtilities;
using SharedLibrary.Models.User;
using SharedLibrary.Models.User.Login.OpenId;
using SharedLibrary.Models.User.Login.OpenId.Token;
using System;
using System.Linq;

namespace SharedLibrary.Core.User.Login.OpenId
{
    public class OpenIdLoginUserCore  : LoginUserCore
    {
        public OpenIdLoginUserCore()
        {
            this.Facebook.RefreshComponentAction += this.Google.RefreshComponentAction += this.Twitter.RefreshComponentAction += this.Microsoft.RefreshComponentAction += this.RefreshComponent;
        }

        public ProviderOpenIdModel Facebook = new ProviderOpenIdModel("Facebook");
        public ProviderOpenIdModel Google = new ProviderOpenIdModel("Google");
        public ProviderOpenIdModel Twitter = new ProviderOpenIdModel("Twitter");
        public ProviderOpenIdModel Microsoft = new ProviderOpenIdModel("Microsoft");

        public Action? RefreshComponentAction;
        //public string? LoginToken { get; set; }
        //public string? Code { get; set; }

        //public struct UriParameters
        //{
        //    public UriParameters(string loginToken, string code)
        //    {
        //        this.LoginToken = loginToken;
        //        this.Code = code;
        //    }

        //    public string? LoginToken { get; set; }
        //    public string? Code { get; set; }
        //}

        public string test1 = "-";
        public string test2 = "-";

        void RefreshComponent()
        {
            this.RefreshComponentAction?.Invoke();
        }

        public void LoadUris(OpenIdUrisModel? openIdUrisModel)
        {
            if (openIdUrisModel != null)
            {
                this.Facebook.Uri = openIdUrisModel?.Facebok;
                this.Google.Uri = openIdUrisModel?.Google;
                this.Twitter.Uri = openIdUrisModel?.Twitter;
                this.Microsoft.Uri = openIdUrisModel?.Microsoft;
            }
        }

        public AuthenticationTokenOpenIdModel? GetStateAndCodeFromUri(Uri uri)
        {
            if (uri != null)
            {
                var parsedQuery = QueryHelpers.ParseQuery(uri.Query);

                if (parsedQuery.TryGetValue("state", out var loginToken) && parsedQuery.TryGetValue("code", out var code))
                {
                    return new AuthenticationTokenOpenIdModel(loginToken.First().Split(new[] { '=' }, 2)[1], code.First());
                }
            }

            return null;
        }
    }
}
