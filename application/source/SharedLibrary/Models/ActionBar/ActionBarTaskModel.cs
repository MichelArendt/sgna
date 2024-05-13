#nullable disable
using SharedLibrary.Models.ActionCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.Models.ActionBar
{
    public class ActionBarTaskModel
    {
        public ActionBarTaskModel(string description)
        {
            this.Description = description;
            this.ChangeClass();
        }

        //public enum States { New, Idle, Running, Finished, Cancelled, Failed, Completed };

        public string ClassSVG { get; set; } = "";
        public string Description { get; set; }
        public ActionCenterTaskStates State { get { return this.state; } set { this.state = value;  this.ChangeClass(); } }
        private ActionCenterTaskStates state { get; set; } = ActionCenterTaskStates.New;

        private void ChangeClass()
        {
            this.ClassSVG = "status_";
            this.ClassSVG += state.ToString();
        }
    }
}
