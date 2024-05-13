using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.Models.User.Login.OpenId
{
    public class ProviderOpenIdModel
    {
        public ProviderOpenIdModel(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
        public bool IsDisabled { get; set; } = true;
        public Action? RefreshComponentAction;

        private string? uri;
        public string? Uri
        {
            get { return uri; }
            set
            {
                uri = value;
                if (!String.IsNullOrEmpty(uri)) { this.Enable(); this.RefreshComponentAction?.Invoke(); }
            }
        }

        public void Enable() { this.IsDisabled = false; }
        public void Disable() { this.IsDisabled = true; }
    }
}
