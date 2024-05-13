using SharedLibrary.Models.ActionCenter;
using SharedLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.Core.ActionCenter
{
    public class ActionCenterTasksCore
    {
        public Dictionary<ulong?, ActionCenterTaskModel> CurrentTasks = new Dictionary<ulong?, ActionCenterTaskModel>();

        public Action? TasksChangedAction { get; set; }

        ulong DbTaskIdTracker = 0;
        ulong CurrentTaskId = 0;

        public ActionCenterTaskModel AddTask(string descriptionResourceName)
        {
            this.DbTaskIdTracker++;
            ActionCenterTaskModel task = new ActionCenterTaskModel(descriptionResourceName);
            task.TaskChangedAction += RefreshTasksAction;
            this.CurrentTasks.Add(this.DbTaskIdTracker, task);
            return task;
        }

        public ActionCenterTaskModel Task(ulong? taskId = null)
        {
            this.CurrentTaskId = taskId ?? this.DbTaskIdTracker;

            return CurrentTasks[CurrentTaskId];
        }

        public void RefreshTasksAction()
        {
            this.TasksChangedAction?.Invoke();
        }
    }
}
