#nullable disable

using ServerAPI.Server.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServerAPI.Server.Models.Category
{
    public class CategoryModel : ICategory
    {
        [Key]
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Relationship
        public virtual List<SubcategoryOfModel> From { get; set; }
        public virtual List<SubcategoryOfModel> To { get; set; }
    }
}
