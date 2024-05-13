using SharedLibrary.Models.User;
using SharedLibrary.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.Services
{
    public class SessionService
    {
        public UserBaseModel? User { get; set; }
        public string? State { get; set; } = null;

        public bool UserIsLogged()
        {
            if (User == null)
            {
                return false;
            }

            return true;
        }
    }
}
