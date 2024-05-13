using System;
using System.Collections.Generic;

namespace SharedLibrary.Models.ActionCenter
{
    public enum ActionCenterTaskStates { New, Idle, Running, Finished, Cancelled, Failed, Completed };

    public class ActionCenterTaskModel
    {
        public ActionCenterTaskModel(string descriptionResourceName, bool notifyUser = false)
        {
            this.DescriptionResourceName = descriptionResourceName;
            this.AddMessage("Task started");
            this.NotifyUser = notifyUser;
            this.StartDate = DateTime.Now.ToUniversalTime();
        }

        public string DescriptionResourceName { get; set; }
        /// <summary>
        /// List of resource name values
        /// </summary>
        public List<ActionCenterTaskMessageModel> Messages { get; set; } = new List<ActionCenterTaskMessageModel>();
        public ActionCenterTaskStates State { get; set; } = ActionCenterTaskStates.New;
        public bool NotifyUser { get; set; } = false;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Action? TaskChangedAction { get; set; }

        public ActionCenterTaskModel AddMessage(string messageResourceName, string[]? additionalResourcesNames = null)
        {
            this.Messages.Add(new ActionCenterTaskMessageModel(messageResourceName, additionalResourcesNames));
            this.RefreshTasksAction();
            return this;
        }

        public ActionCenterTaskModel ChangeState(ActionCenterTaskStates newState)
        {
            this.State = newState;
            if (newState == ActionCenterTaskStates.Completed)
            {
                this.AddMessage("Task completed");
            }
            this.RefreshTasksAction();
            return this;
        }

        public void RefreshTasksAction()
        {
            this.TaskChangedAction?.Invoke();
        }
    }
}
