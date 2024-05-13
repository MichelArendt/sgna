#nullable disable

using SharedLibrary.Models.User;
using SharedLibrary.Models.Users;

namespace ServerAPI.Server.Models.Users
{
    public class UserModel : UserBaseModel
    {
        public UserModel() { }
        public string Password { get; set; }
    }
}
