#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.Models.Users
{
    public class UsersTokensModel
    {

        [Key]
        public ulong Id { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Token { get; set; }
    }
}
