using Newtonsoft.Json;
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
    public class LoginServerAPI : ServerHubAPI
    {

        public LoginServerAPI(OpenIdSettingsService openIdSettingsService, ClientSecretsService clientSecretsService, DatabaseService database)
        {
            OpenIdSettingsService = openIdSettingsService;
            ClientSecretsService = clientSecretsService;
            Database = database;
        }

        OpenIdSettingsService OpenIdSettingsService;
        ClientSecretsService ClientSecretsService;
        DatabaseService Database;

        //public async Task<OpenIdUriModel?> GetOpenIdSettingsAsync(string provider)
        //{
        //    OpenIdUriModel? openIdModel;

        //    switch (provider)
        //    {
        //        // Google
        //        default:
        //            openIdModel = OpenIdSettingsService.OpenIdSettings.Google;
        //            break;
        //    }

        //    return openIdModel;
        //}

        //public async Task<string?> GetOpenIdUriAsync(string provider, string email)
        //{
        //    OpenIdUriModel? openIdModel;

        //    switch (provider)
        //    {
        //        case "Google":
        //            openIdModel = OpenIdSettingsService.OpenIdSettings.Google;
        //            break;

        //        // Provedor inválido
        //        default:
        //            return null;
        //    }

        //    OpenIdLoginServerCore openIdLogin = new OpenIdLoginServerCore(openIdModel);
        //    openIdLogin.LoginHint = email;
        //    openIdLogin.State = await Database.AddUserTokenLoginAsync();

        //    return openIdLogin.BuildUri();
        //}

        public async Task<UserBaseModel?> LoginUserWithOpenIdAsync((string LoginToken, string Code) uriParameters)
        {
            string? userLoginToken = await this.IsLoginTokenValid(uriParameters.LoginToken);

            if (!String.IsNullOrEmpty(userLoginToken))
            {

            }

            return null;
        }

        public async Task<string?> IsLoginTokenValid(string loginToken)
        {
            // Verifica se o token de segurança existe no banco de dados
            UserTokenLoginModel userLoginToken = await Database.GetLoginTokenAsync(loginToken);

            if (userLoginToken != null)
            {
                // Token válido - deleta o token
                await Database.DeleteLoginToken(userLoginToken);
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

            await SendMessageToCallerAsync("Authorizing token", "ReceiveMessage");
            var response = await StaticGlobals.HttpClient.PostAsync("https://oauth2.googleapis.com/token", content);


            string responseString = await response.Content.ReadAsStringAsync();
            await SendMessageToCallerAsync("Token authorized: " + responseString, "ReceiveMessage");

            AccessTokenOpenIdModel openIdResponseModel = new AccessTokenOpenIdModel();

            openIdResponseModel = JsonConvert.DeserializeObject<AccessTokenOpenIdModel>(responseString);
            await SendMessageToCallerAsync(openIdResponseModel.id_token, "ReceiveMessage");

            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            //Check if readable token (string is in a JWT format)
            bool readableToken = jwtHandler.CanReadToken(openIdResponseModel.id_token);

            if (readableToken != true)
            {
                await SendMessageToCallerAsync("The token doesn't seem to be in a proper JWT format: " + openIdResponseModel.id_token, "ReceiveMessage");
            }
            else
            {
                JwtSecurityToken token = jwtHandler.ReadJwtToken(openIdResponseModel.id_token);
                await SendMessageToCallerAsync("Token read", "ReceiveMessage");

                Console.WriteLine("==============================");
                Console.WriteLine("==============================");
                var claims = token.Claims.Select(c => new { c.Type, c.Value });
                var claimsList = claims.ToList();

                var json = "{\r\n";
                foreach (var claim in claimsList)
                {
                    if (claim.Type == "iat" || claim.Type == "exp")
                    {
                        json += $"\t\"{claim.Type}\": {claim.Value}";
                    } else
                    {
                        json += $"\t\"{claim.Type}\": \"{claim.Value}\"";
                    }

                    if (claim != claimsList.Last())
                    {
                        json += ",\r\n";
                    }
                }
                json += "\r\n}";

                Console.WriteLine(json);

                JWTPayloadOpenIdModel jwtPayloadModel = JsonConvert.DeserializeObject<JWTPayloadOpenIdModel>(json);
                Console.WriteLine(jwtPayloadModel.email);
                UserBaseModel user = new UserBaseModel();
                Console.WriteLine("==============================");
                Console.WriteLine("==============================");
                user.UserFromJWTToken(jwtPayloadModel);

                foreach (PropertyInfo prop in user.GetType().GetProperties())
                {
                    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    if (type == typeof(DateTime))
                    {
                        Console.WriteLine(prop.GetValue(user, null).ToString());
                    }
                }
            }
        }
    }
}
