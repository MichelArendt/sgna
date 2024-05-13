using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Server.Interfaces
{
    public interface ICustomProperty
    {
        ulong Id { get; set; }
        string Symbol { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
