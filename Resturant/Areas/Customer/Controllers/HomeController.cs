using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resturant.Data;
using Resturant.Models;
using Resturant.Models.ViewModels;

namespace Resturant.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

        ApplicationDbContext db;
        public HomeController(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel()
            {
                coupons = db.Coupons.Where(x=>x.IsActive ==true).ToList(),
                categories = db.Categories.ToList(),
                menuItems = db.MenuItems.Include(x => x.Category).Include(x => x.SubCategory).ToList()
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
