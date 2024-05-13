using SharedLibrary.Interfaces.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.Models.UserInterface.ActionBar.Dropup
{
    public class ActionBarButtonDropupUiModel : IUiSelected
    {
        public ActionBarButtonDropupUiModel(bool isSelected = false)
        {
            if (isSelected)
            {
                this.Select();
            }
        }
        public bool IsSelected { get; set; } = false;
        public string Style { get; private set; } = "";

        private readonly string styleSelected = "selected";
        private readonly string styleUnselected = "";

        public void Select()
        {
            this.Style = this.styleSelected;
            this.IsSelected = true;
        }

        public void Deselect()
        {
            this.Style = this.styleUnselected;
            this.IsSelected = false;
        }

        public void Toggle()
        {
            if (this.IsSelected)
            {
                this.Deselect();
            }
            else
            {
                this.Select();
            }
        }
    }
}
