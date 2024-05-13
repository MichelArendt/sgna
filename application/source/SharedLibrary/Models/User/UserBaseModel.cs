#nullable disable

using SharedLibrary.Models.User.Login.OpenId;
using System;
using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.Models.User
{
    public class UserBaseModel
    {
        [Key]
        public ulong Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Locale { get; set; }
        public string StartPage { get; set; }
        public string GoogleId { get; set; }

        public void UserFromJWTToken(JWTPayloadOpenIdModel jWTPayloadOpenIdModel)
        {
            this.GoogleId = jWTPayloadOpenIdModel.sub;
            this.Email = jWTPayloadOpenIdModel.email;
            this.EmailVerified = Convert.ToBoolean(jWTPayloadOpenIdModel.email_verified);
            this.FullName = jWTPayloadOpenIdModel.name;
            this.FirstName = jWTPayloadOpenIdModel.given_name;
            this.LastName = jWTPayloadOpenIdModel.family_name;

            if (!String.IsNullOrEmpty(jWTPayloadOpenIdModel.locale))
            {
                this.Locale = jWTPayloadOpenIdModel.locale;
            }
        }
    }
}
