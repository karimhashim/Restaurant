using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Category Name is Requried")]
        [Display(Name ="Category Name")]
        public string Name { get; set; }


        public virtual IEnumerable<SubCategory> subCategories { get; set; }
    }
}
