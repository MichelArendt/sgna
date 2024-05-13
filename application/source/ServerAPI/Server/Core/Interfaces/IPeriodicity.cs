using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Server.Interfaces
{
    [Flags]
    public enum DaysOfWeek
    {
        Sunday = 1,
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        Saturday = 64
    }

    public interface IPeriodicity
    {
        //TimeZoneInfo TimeZone { get; set; }
        DateTime DateBegin { get; set; }
        DateTime DateEnd { get; set; }
        DateTime TimeBegin { get; set; }
        DateTime TimeEnd { get; set; }
        DaysOfWeek WeekDays { get; set; }
        TimeSpan Repeatable { get; set; }
    }
}
