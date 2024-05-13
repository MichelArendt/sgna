using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using ServerAPI.Server.Core.User.Login.OpenId;
using ServerAPI.Server.Models.Users;
using ServerAPI.Server.Services;
using SharedLibrary.Models.User;
using SharedLibrary.Models.User.Login.OpenId;
using SharedLibrary.Models.User.Login.OpenId.Token;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace ServerAPI.Server.Core.API
{
    public class ServerAPICore : Hub
    {
        // Hubs are transient:

        // Don't store state in a property on the hub class. Every hub method call is executed on a new hub instance.
        // Use await when calling asynchronous methods that depend on the hub staying alive.For example, a method such as Clients.All.SendAsync(...) can fail if it's called without await and the hub method completes before SendAsync finishes.

        public ServerAPICore(OpenIdSettingsService openIdSettingsService, ClientSecretsService clientSecretsService, DatabaseService databaseService)
        {
            OpenIdSettingsService = openIdSettingsService;
            ClientSecretsService = clientSecretsService;
            DatabaseService = databaseService;
        }

        OpenIdSettingsService OpenIdSettingsService;
        ClientSecretsService ClientSecretsService;
        DatabaseService DatabaseService;

        /// <summary>
        /// Evento executado quando usuário conecta-se a API
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("OnConnectedToAPI");
            if (await DatabaseService.CheckDatabaseConnectionAsync())
            {
                await Clients.Caller.SendAsync("OnConnectedToDatabase");
            } else
            {
                throw new NotImplementedException(); // TODO
            }
            await base.OnConnectedAsync();
        }

        public async Task<OpenIdUrisModel?> GetOpenIdUrisAsync()
        {
            string? state = await DatabaseService.AddUserTokenLoginAsync();

            if (!string.IsNullOrEmpty(state))
            {
                OpenIdLoginuserServerCore openIdLogin = new OpenIdLoginuserServerCore(state, OpenIdSettingsService.OpenIdSettings);

                return openIdLogin.BuildOpenIdUris();
            }

            return null;
        }

        public async Task<UserBaseModel?> LoginUserWithOpenIdAsync(AuthenticationTokenOpenIdModel openIdAuthenticationTokenModel)
        {
            string? userLoginToken = await this.IsLoginTokenValid(openIdAuthenticationTokenModel.LoginToken);

            if (!String.IsNullOrEmpty(userLoginToken))
            {
                await this.ExchangeCodeForAccessTokenAsync(openIdAuthenticationTokenModel.Code);
            }

            return null;
        }

        public async Task<string?> IsLoginTokenValid(string loginToken)
        {
            // Verifica se o token de segurança existe no banco de dados
            UserTokenLoginModel? userLoginToken = await DatabaseService.GetLoginTokenAsync(loginToken);

            Console.WriteLine(loginToken);
            Console.WriteLine(userLoginToken?.Token);
            if (userLoginToken != null)
            {
                // Token válido - deleta o token
                await DatabaseService.DeleteLoginToken(userLoginToken);
                return userLoginToken.Token;
            }

            return null;
        }

        public async Task ExchangeCodeForAccessTokenAsync(string code)
        {
            var values = new Dictionary<string, string> {
                    { "code", code },
                    { "client_id", OpenIdSettingsService.OpenIdSettings.Google.ClientId },
                    { "client_secret", ClientSecretsService.ClientSecrets.Google.ClientSecret },
                    { "redirect_uri", OpenIdSettingsService.OpenIdSettings.Google.RedirectUri },
                    { "grant_type", "authorization_code" }
                };

            var content = new FormUrlEncodedContent(values);

            var response = await StaticGlobals.HttpClient.PostAsync("https://oauth2.googleapis.com/token", content);

            string responseString = await response.Content.ReadAsStringAsync();

            AccessTokenOpenIdModel openIdResponseModel = new AccessTokenOpenIdModel();

            openIdResponseModel = JsonConvert.DeserializeObject<AccessTokenOpenIdModel>(responseString);

            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            //Check if readable token (string is in a JWT format)
            bool readableToken = jwtHandler.CanReadToken(openIdResponseModel.id_token);

            if (readableToken != true)
            {
                throw new NotImplementedException(); // TODO
            }
            else
            {
                JwtSecurityToken token = jwtHandler.ReadJwtToken(openIdResponseModel.id_token);

                var claims = token.Claims.Select(c => new { c.Type, c.Value });
                var claimsList = claims.ToList();

                var json = "{\r\n";
                foreach (var claim in claimsList)
                {
                    if (claim.Type == "iat" || claim.Type == "exp")
                    {
                        json += $"\t\"{claim.Type}\": {claim.Value}";
                    }
                    else
                    {
                        json += $"\t\"{claim.Type}\": \"{claim.Value}\"";
                    }

                    if (claim != claimsList.Last())
                    {
                        json += ",\r\n";
                    }
                }
                json += "\r\n}";

                JWTPayloadOpenIdModel jwtPayloadModel = JsonConvert.DeserializeObject<JWTPayloadOpenIdModel>(json);
                Console.WriteLine(jwtPayloadModel.email);
                UserModel user = new UserModel();

                user.UserFromJWTToken(jwtPayloadModel);

                await DatabaseService.AddOrRetrieveUserAsync(user);
            }
        }
    }
}
