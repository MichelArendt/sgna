using SharedLibrary.Core.UserInterface.ActionBar.Dropup;
using SharedLibrary.Models.UserInterface.ActionBar.Dropup;

namespace SharedLibrary.Core.UserInterface.ActionBar
{
    public class ActionBarUiBaseCore
    {
        public ActionBarDropupUiCore Dropup = new ActionBarDropupUiCore();
        public ActionBarButtonDropupUiModel ButtonDropup = new ActionBarButtonDropupUiModel();

        public void ToggleDropup()
        {
            this.Dropup.Toggle();
            this.ButtonDropup.Toggle();
        }
    }
}
