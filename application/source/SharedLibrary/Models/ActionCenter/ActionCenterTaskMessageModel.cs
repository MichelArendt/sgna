using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.Models.ActionCenter
{
    public class ActionCenterTaskMessageModel
    {
        public ActionCenterTaskMessageModel(string resourceName, string[]? additionalResourcesNames)
        {
            this.ResourceName = resourceName;
            if (additionalResourcesNames != null)
            {
                (this.AdditionalResourcesNames = new List<string>()).AddRange(additionalResourcesNames);

            }
            this.Date = DateTime.Now;
        }

        public string ResourceName { get; set; }
        public List<string>? AdditionalResourcesNames { get; set; }
        public DateTime Date { get; set; }
    }
}
