using SharedLibrary.Core.ActionCenter;
using SharedLibrary.Models.ActionCenter;
using System;
using System.Collections.Generic;

namespace SharedLibrary.Services
{
    public class ActionCenterService
    {
        //public ActionCenterService(DateTimeService dateTimeService)
        //{
        //    //DateTimeService = dateTimeService;
        //    Tasks = new ActionCenterTasksCore(dateTimeService);
        //}

        //DateTimeService DateTimeService;
        public ActionCenterTasksCore Tasks = new ActionCenterTasksCore();
    }
}
