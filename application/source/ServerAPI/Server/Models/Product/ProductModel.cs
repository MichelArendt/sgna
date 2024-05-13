using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServerAPI.Server.Models.Product
{
    public class ProductModel
    {
        #nullable enable
        [Key]
        public ulong Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public float? Price { get; set; }


        // Relationships
        #nullable disable
        public virtual List<ProductRelationshipsModel> Parent { get; set; }
        public virtual List<ProductRelationshipsModel> Product { get; set; }
        public virtual List<ProductRelationshipsModel> Child { get; set; }

        //public SortedList<string, ProductPropertyModel> Properties { get; set; }
        //public SortedList<string, ProductModel> Products { get; set; }
        //public IPeriodicity Periodicity { get; set; }
        //[ForeignKey("PhotoModel")]
        //public long PhotoId { get; set; }
    }
}
