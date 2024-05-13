using SharedLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.DesktopClient.Services
{
    public class ConfigurationService : ConfigurationBaseService
    {
        // Opções de desenvolvimento
        public bool DebugShowDesktopWindowIcons { get; set; } = true;
        public string DebugExecutingAs()
        {
            return (Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "Browser");
        }
    }
}
