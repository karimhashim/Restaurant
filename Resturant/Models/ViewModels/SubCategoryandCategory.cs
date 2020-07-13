using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant.Models.ViewModels
{
    public class SubCategoryandCategory
    {
        public List<Category> categories { get; set; }
        public List<string> subCategoryList { get; set; }
        public SubCategory subCategory { get; set; }
        public string  status { get; set; }
    }
}
