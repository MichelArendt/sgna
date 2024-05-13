namespace SharedLibrary.Models.User.Login.OpenId.Token
{
    public class AuthenticationTokenOpenIdModel
    {
        public AuthenticationTokenOpenIdModel()
        {

        }
        public AuthenticationTokenOpenIdModel(string loginToken, string code)
        {
            this.LoginToken = loginToken;
            this.Code = code;
        }

        public string LoginToken { get; set; }
        public string Code { get; set; }
    }
}
