using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.Interfaces.UserInterface
{
    public interface IUiSelected
    {
        public bool IsSelected { get; set; }
        public string Style { get; }

        public void Select();

        public void Deselect();
    }
}
