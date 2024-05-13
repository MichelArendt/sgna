#nullable disable

namespace ServerAPI.Server.Models.Category
{
    public class SubcategoryOfModel
    {
        public ulong CategoryId { get; set; }
        public ulong SubcategoryId { get; set; }


        // Relationships

        public virtual CategoryModel Category { get; set; }
        public virtual CategoryModel Subcategory { get; set; }

    }
}
