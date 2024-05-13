using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ServerAPI.Server.Models.Product;

namespace ServerAPI.Server.Models.Product
{
    public class ProductRelationshipsModel
    {
        public ulong ParentId { get; set; }
        public ulong ProductId { get; set; }
        public ulong ChildId { get; set; }

        // Relationship
        #nullable disable
        public virtual ProductModel Parent { get; set; }
        public virtual ProductModel Product { get; set; }
        public virtual ProductModel Child { get; set; }

    }
}
