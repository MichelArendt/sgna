using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SharedLibrary.Models.Settings;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace SharedLibrary.Services
{
    public class UserSettingsService
    {
        public UserSettingsService()
        {
            //CultureInfo = CultureInfo.DefaultThreadCurrentCulture;
        }

        public CultureInfo CultureInfo { get; set; } = new CultureInfo("pt-BR");
    }
}
