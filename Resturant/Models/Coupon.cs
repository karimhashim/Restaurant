using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant.Models
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Coupon")]
        public string Name { get; set; }

        [Required]
        public string CouponType { get; set; }

        public enum Ecoupontype { percent = 0, dollar = 1 }
        [Required]
        public double Discount { get; set; }

        public string Image { get; set; }

        public bool IsActive { get; set; }


    }
}
