using SharedLibrary.Models.ActionBar;
using SharedLibrary.Models.ActionCenter;
using System;
using System.Collections.Generic;

namespace SharedLibrary.Services
{
    public class ActionBarService
    {
        public ActionBarService()
        {
        }

        public List<ActionCenterTaskModel> Tasks = new List<ActionCenterTaskModel>();
        public List<ActionBarNotificationModel> Notifications = new List<ActionBarNotificationModel>();

        public void AddTask(string description)
        {
            this.Tasks.Add(new ActionCenterTaskModel(description));
        }

        public void AddNotification(string description)
        {
            this.Notifications.Add(new ActionBarNotificationModel(description));
        }

        public void GetActiveActions()
        {
            throw new NotImplementedException();
        }
    }
}
