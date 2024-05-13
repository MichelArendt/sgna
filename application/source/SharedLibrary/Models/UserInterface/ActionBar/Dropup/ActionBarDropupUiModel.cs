#nullable disable

using SharedLibrary.Interfaces.UserInterface;
using SharedLibrary.Models.UserInterface.ActionBar.Dropup.Tabs;
using System;

namespace SharedLibrary.Models.UserInterface.ActionBar.Dropup
{
    public class ActionBarDropupUiModel : IUiVisibility
    {
        public ActionBarDropupUiModel(bool visible = false)
        {
            if (visible)
            {
                this.Show();
            }
        }

        public ActionBarDropupTabsUiModel Tabs = new ActionBarDropupTabsUiModel();
        public bool IsVisible { get; set; } = false;
        public string Style { get; private set; } = "display_none";
        public event Action? RefreshComponentAction;

        private readonly string styleVisible = "display_block";
        private readonly string styleHidden = "display_none";

        public void Hide()
        {
            this.Style = this.styleHidden;
            this.IsVisible = false;
        }

        public void Show()
        {
            this.Style = this.styleVisible;
            this.IsVisible = true;
        }

        public void Toggle()
        {
            if (this.IsVisible)
            {
                this.Hide();
            }
            else
            {
                this.Show();
            }
        }

        public void RefreshComponent()
        {
            this.RefreshComponentAction?.Invoke();
        }
    }
}
