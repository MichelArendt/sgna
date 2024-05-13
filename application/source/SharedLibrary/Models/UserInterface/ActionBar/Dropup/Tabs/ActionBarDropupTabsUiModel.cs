using System;

namespace SharedLibrary.Models.UserInterface.ActionBar.Dropup.Tabs
{
    public class ActionBarDropupTabsUiModel
    {
        public ActionBarDropupTabUiModel Tasks = new ActionBarDropupTabUiModel(true);
        public ActionBarDropupTabUiModel Notifications = new ActionBarDropupTabUiModel();

        public void SelectTab(ActionBarDropupTabUiModel tab)
        {
            throw new NotImplementedException();
        }
    }
}
