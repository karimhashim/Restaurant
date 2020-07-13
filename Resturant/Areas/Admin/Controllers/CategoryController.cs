using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Resturant.Data;
using Resturant.Models;

namespace Resturant.Areas.Admin.Controllers
{   [Area("Admin")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext db;
        public CategoryController(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category newCategory)
        {

            if (ModelState.IsValid)
            {
            db.Categories.Add(newCategory);
            db.SaveChanges();
         return RedirectToAction("index");

            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id == null) return NotFound();
            var category = db.Categories.Find(Id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Update(category);
                db.SaveChanges();
               return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null) return NotFound();
            var category = db.Categories.Find(Id);
            if (category == null) return NotFound();

            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
         public IActionResult Details(int Id)
         {
            var category = db.Categories.Find(Id);
            return View(category);
         }
    }
}
