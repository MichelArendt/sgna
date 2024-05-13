#nullable disable

using ServerAPI.Server.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ServerAPI.Server.Models
{
    public class PropertyModel : ICustomProperty
    {
        [Key]
        public ulong Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public SortedList<string, IProductProperty> Properties { get; set; }
    }
}
