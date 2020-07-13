using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Resturant.Models;

namespace Resturant.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<SubCategory> SubCategories { get; set; }

        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
       
    }
}
