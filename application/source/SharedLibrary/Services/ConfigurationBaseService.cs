using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.Services
{
    public class ConfigurationBaseService
    {
        public bool IsDevelopment { get; set; } = true;

        public void GetEnvironment()
        {
            IsDevelopment = (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") ? true : false;
        }
    }
}
