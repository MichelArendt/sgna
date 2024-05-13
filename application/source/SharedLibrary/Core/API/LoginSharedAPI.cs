using Blazor.Extensions;
using Newtonsoft.Json;
using SharedLibrary.Models.User.Login.OpenId.Token;
using System;
using System.Threading.Tasks;

namespace SharedLibrary.Core.API
{
    public class LoginSharedAPI : ClientHubAPI
    {
        public AccessTokenOpenIdModel OpenIdResponse = new AccessTokenOpenIdModel();
        public Action? ReceivedAccessTokenAction { get; set; } = null;

        private readonly string Endpoint = "https://localhost:44300/API/Login";

        public async Task BuildAndConnectAsync(HubConnectionBuilder hubConnectionBuilder)
        {
            await base.BuildAndConnectAsync(hubConnectionBuilder, this.Endpoint);

        }

        public async Task GetOpenIdSettingsAsync(string provider)
        {
            await SendMessageAsync("GetOpenIdSettingsAsync", provider);
        }

        public async Task<string?> GetOpenIdUriAsync(string provider, string email)
        {
            if (base.HubConnection != null)
            {
                return await base.HubConnection.InvokeAsync<string?>("GetOpenIdUriAsync", provider, email);
            } else
            {
                // TODO
                throw new NotImplementedException();
            }
        }

        public async Task<string?> IsLoginTokenValid(string? token)
        {
            if (token == null)
            {
                return null;
            }
            return await base.HubConnection.InvokeAsync<string?>("IsLoginTokenValid", token);
        }

        public async Task ExchangeCodeForAccessTokenAsync(string code)
        {
            if (base.HubConnection?.On<string, string>("ReceiveAccessToken", (message, token) => this.OnReceiveAccessToken(message, token)) != null)
            {
                await base.HubConnection.InvokeAsync("ExchangeCodeForAccessTokenAsync", code);
            }
            else
            {
                // TODO
                throw new NotImplementedException();
            }
        }
        public async Task OnReceiveAccessToken(string message, string token)
        {
            await Task.Run(() => {
                base.Message = message;
                OpenIdResponse = JsonConvert.DeserializeObject<AccessTokenOpenIdModel>(token);
            });
            ReceivedMessageAction?.Invoke();
            ReceivedAccessTokenAction?.Invoke();
        }

        public async Task NewLoginTokenAsync()
        {
            await base.SendMessageAsync("NewLoginTokenAsync");
            //if (base.HubConnection?.On<string>("NewLoginTokenAsync", (tokenId) => this.OnNewLoginTokenAsync(tokenId)) != null)
            //{
            //} else
            //{
            //    // TODO
            //    throw new NotImplementedException();
            //}
        }

        //public async Task OnNewLoginTokenAsync(string tokenId)
        //{
        //    await Task.Run(() => LoginToken = tokenId);
        //    ReceivedNewLoginToken?.Invoke();
        //}
    }
}
