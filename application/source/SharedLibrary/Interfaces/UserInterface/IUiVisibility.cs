using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.Interfaces.UserInterface
{
    public interface IUiVisibility
    {
        public bool IsVisible { get; set; }
        public string Style { get; }

        public void Hide();

        public void Show();
    }
}
