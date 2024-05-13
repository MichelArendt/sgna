using SharedLibrary.Interfaces.UserInterface;
using SharedLibrary.Models.UserInterface.ActionBar.Dropup.Contents;

namespace SharedLibrary.Core.UserInterface.ActionBar.Dropup
{
    public class ActionBarDropupContentsUiCore
    {
        public ActionBarDropupContentUiModel Tasks = new ActionBarDropupContentUiModel(true);
        public ActionBarDropupContentUiModel Notifications = new ActionBarDropupContentUiModel();
    }
}
