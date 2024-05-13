#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Server.Models.Category
{
    public class CategoryWithSubcategoryModel
    {
        public CategoryModel Category { get; set; }
        public List<CategoryWithSubcategoryModel> Subcategories { get; set; }

        public CategoryWithSubcategoryModel()
        {
                
        }

        public CategoryWithSubcategoryModel(CategoryModel category, List<CategoryWithSubcategoryModel> subcategories)
        {
            Category = category;
            Subcategories = subcategories;
        }
    }
}
