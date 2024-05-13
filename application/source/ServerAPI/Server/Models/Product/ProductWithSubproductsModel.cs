#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Server.Models.Product
{
    public class ProductWithSubproductsModel
    {
        public ProductModel Product { get; set; }
        public List<ProductWithSubproductsModel>? Subproducts { get; set; }

        //public ProductWithSubproductsModel()
        //{
                
        //}

        public ProductWithSubproductsModel(ProductModel product, List<ProductWithSubproductsModel>? subproducts)
        {
            Product = product;
            Subproducts = subproducts;
        }
    }
}
