using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Server.Interfaces
{
    interface IRequiredPermissions
    {
        bool RequiresDevelopment { get; set; }
        bool RequiresLogin { get; set; }
    }
}
