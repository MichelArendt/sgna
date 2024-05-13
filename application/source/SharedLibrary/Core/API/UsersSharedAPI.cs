using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace SharedLibrary.Core.API
{
    public class UsersSharedAPI : ClientHubAPI
    {

        public UsersSharedAPI()
        {
        }


        public async Task GetAllUsersAsync()
        {
            //return await base.SendMessageAsync("GetAllUsers");
        }
    }
}
