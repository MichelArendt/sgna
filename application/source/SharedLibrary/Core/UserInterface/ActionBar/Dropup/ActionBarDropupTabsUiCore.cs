using SharedLibrary.Models.UserInterface.ActionBar.Dropup.Tabs;
using System;

namespace SharedLibrary.Core.UserInterface.ActionBar.Dropup
{
    public class ActionBarDropupTabsUiCore
    {
        public ActionBarDropupTabsUiCore()
        {
            this.Tasks.Select();
        }

        public ActionBarDropupTabUiModel Tasks = new ActionBarDropupTabUiModel(true);
        public ActionBarDropupTabUiModel Notifications = new ActionBarDropupTabUiModel();
    }
}
