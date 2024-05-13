using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.Models.User.Login.OpenId.Token
{
    public class IdTokenOpenIdModel
    {
        public string iss { get; set; } = "";
        public string? at_hash { get; set; }
        public bool? email_verified { get; set; }
        public string sub { get; set; } = "";
        public string email { get; set; } = "";
        public string? profile { get; set; }
        public string? picture { get; set; }
        public string? name { get; set; }
        public string aud { get; set; } = "";
        public int iat { get; set; } = 0;
        public int exp { get; set; } = 0;
        public string? nonce { get; set; }
        public string? hd { get; set; }
    }
}
