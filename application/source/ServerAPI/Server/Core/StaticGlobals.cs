using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServerAPI.Server.Core
{
    public static class StaticGlobals
    {
        public static readonly HttpClient HttpClient = new HttpClient();
    }
}
