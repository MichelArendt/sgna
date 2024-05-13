using SharedLibrary.Interfaces.UserInterface;
using SharedLibrary.Models.UserInterface.ActionBar.Dropup.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.Core.UserInterface.ActionBar.Dropup
{
    public class ActionBarDropupUiCore : IUiVisibility
    {
        public ActionBarDropupUiCore(bool visible = false)
        {
            if (visible)
            {
                this.Show();
            }
        }

        public ActionBarDropupTabsUiCore Tabs = new ActionBarDropupTabsUiCore();
        public ActionBarDropupContentsUiCore Contents = new ActionBarDropupContentsUiCore();
        public bool IsVisible { get; set; } = false;
        public string Style { get; private set; } = "display_none";
        public event Action? RefreshComponentAction;

        private readonly string styleVisible = "display_block";
        private readonly string styleHidden = "display_none";

        public void SelectTab(ActionBarDropupTabUiModel tab)
        {
            if (!tab.IsSelected)
            {
                if (this.Tabs.Tasks == tab)
                {
                    this.Tabs.Tasks.Select();
                    this.Contents.Tasks.Show();
                    this.Tabs.Notifications.Deselect();
                    this.Contents.Notifications.Hide();
                }
                else
                {
                    this.Tabs.Tasks.Deselect();
                    this.Contents.Tasks.Hide();
                    this.Tabs.Notifications.Select();
                    this.Contents.Notifications.Show();
                }
                this.RefreshComponent();
            }
        }

        public void Hide()
        {
            this.Style = this.styleHidden;
            this.IsVisible = false;
            this.RefreshComponent();
        }

        public void Show()
        {
            this.Style = this.styleVisible;
            this.IsVisible = true;
            this.RefreshComponent();
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
