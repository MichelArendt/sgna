using ElectronNET.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.DesktopClient.Services
{
    public class DesktopWindowService
    {
        public BrowserWindow? DesktopWindow { get; set; }

        public void CloseDesktopWindow()
        {
            if (DesktopWindow != null)
            {
                DesktopWindow.Destroy();
            }
        }
    }
}
