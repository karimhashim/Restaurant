using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant.Models.ViewModels
{
    public class IndexViewModel
    {

        public IEnumerable<Coupon> coupons { get; set; }
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<MenuItem> menuItems { get; set; }
    }
}
