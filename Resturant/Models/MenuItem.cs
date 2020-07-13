using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant.Models
{
    public class MenuItem
    {

        public int Id { get; set; }
        [Required]
        [Display(Name ="MenuItem")]
        public string Name { get; set; }
        public string Discription { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="price should be greater than 1$")]
        public double Price { get; set; }

        public string Image { get; set; }

        public enum spiceness  {NotSpice =0,spicy=1,veryspicy=2}
     
    }
}
