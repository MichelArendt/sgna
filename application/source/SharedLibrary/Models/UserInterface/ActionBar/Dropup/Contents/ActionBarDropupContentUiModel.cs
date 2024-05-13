using SharedLibrary.Interfaces.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.Models.UserInterface.ActionBar.Dropup.Contents
{
    public class ActionBarDropupContentUiModel : IUiVisibility
    {
        public ActionBarDropupContentUiModel(bool visible = false)
        {
            if (visible)
            {
                this.Show();
            }
        }

        public bool IsVisible { get; set; } = false;
        public string Style { get; private set; } = "display_none";

        private readonly string styleVisible = "display_flex_column";
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
    }
}
