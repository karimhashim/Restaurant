using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resturant.Data;
using Resturant.Models;

namespace Resturant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuItemController : Controller
    {
        private ApplicationDbContext db;
        private IWebHostEnvironment hosting;
        public MenuItemController(ApplicationDbContext _db, IWebHostEnvironment _hosting)
        {
            db = _db;
            hosting = _hosting;
        }
        public IActionResult Index()
        {
            var menuitems = db.MenuItems.Include(x => x.Category).Include(x => x.SubCategory).ToList();
            return View(menuitems);
        }

        public IActionResult Create()
        {
            ViewBag.subcategories = new SelectList(db.SubCategories.ToList(), "Id", "Name");
            ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(MenuItem menuItem, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {

                    menuItem.Image = file?.FileName;
                    var filePath = hosting.WebRootPath + "/Images";
                    using (var stream = new FileStream(Path.Combine(filePath, file.FileName), FileMode.Create))
                    {
                        file.CopyToAsync(stream);
                    }
                }
                db.MenuItems.Add(menuItem);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var menuitem = db.MenuItems.Find(id);
            ViewBag.subcategories = new SelectList(db.SubCategories.ToList(), "Id", "Name");
            ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name", menuitem.CategoryId);
            return View(menuitem);
        }

        [HttpPost]
        public IActionResult Edit(MenuItem menuItem, IFormFile file)
        {
            if (ModelState.IsValid)
            {


                if (file == null)
                {
                }
                else
                {
                    menuItem.Image = file.FileName;
                    var filePath = hosting.WebRootPath + "/Images";
                    using (var stream = new FileStream(Path.Combine(filePath, file.FileName), FileMode.Create))
                    {
                        file.CopyToAsync(stream);
                    }
                }
                db.Update(menuItem);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            else
            {
                ViewBag.subcategories = new SelectList(db.SubCategories.ToList(), "Id", "Name");
                ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name", menuItem.CategoryId);
                return View(menuItem);
            }

        }

        public IActionResult Delete(int id)
        {
            db.MenuItems.Remove(db.MenuItems.Find(id));
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
