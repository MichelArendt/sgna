using Microsoft.JSInterop;
using Newtonsoft.Json;
using ServerAPI.Server.Database;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Server.Core.API
{
    public class UsersServerAPI : ServerHubAPI
    {
        public UsersServerAPI(DatabaseContext database)
        {
            Database = database;
        }

        private readonly DatabaseContext Database;
    }
}
