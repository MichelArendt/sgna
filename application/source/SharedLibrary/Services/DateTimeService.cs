using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.Services
{
    public class DateTimeService
    {
        public DateTimeService(UserSettingsService userSettingsService)
        {
            UserSettingsService = userSettingsService;
            CultureInfoStr = userSettingsService.CultureInfo.ToString();
        }

        public UserSettingsService UserSettingsService;

        public readonly string CultureInfoStr;

        public string DateTimeWithTimezone(DateTime dateTime)
        {
            switch (CultureInfoStr)
            {
                case "en-US":
                    dateTime = dateTime.AddHours(0);
                    break;

                default:
                    // pt-BR
                    dateTime = dateTime.AddHours(-3);
                    break;
            }

            return dateTime.ToString("dd/MM/yy HH:mm");
        }
    }
}
