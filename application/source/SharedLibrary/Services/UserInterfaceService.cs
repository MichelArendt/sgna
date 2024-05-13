using SharedLibrary.Core.UserInterface;
using SharedLibrary.Core.UserInterface.ActionBar;

namespace SharedLibrary.Services
{
    public class UserInterfaceService
    {
        public UserInterfaceService()
        {
        }

        public ActionBarUiBaseCore ActionBar = new ActionBarUiBaseCore();
        public ToolbarUiManagerCore Toolbar = new ToolbarUiManagerCore();

        public void CloseAllMenus()
        {
            //this.ActionBar.CloseAllMenus();
            this.Toolbar.CloseAllMenus();
        }
    }
}
