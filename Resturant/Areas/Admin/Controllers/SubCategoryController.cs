using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resturant.Data;
using Resturant.Models;
using Resturant.Models.ViewModels;

namespace Resturant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {

        [TempData]
        public string statusmessage { set; get; }
        ApplicationDbContext db;
        public SubCategoryController(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var SubCategories = db.SubCategories.Include(x => x.Category).ToList();
            return View(SubCategories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
            SubCategoryandCategory model = new SubCategoryandCategory()
            {
                categories = db.Categories.ToList(),
                subCategoryList = db.SubCategories.Select(x => x.Name).ToList(),
                subCategory =new SubCategory()

            };


            return View(model);
        }

        [HttpPost]
        public IActionResult Create(SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                var doesSubExist = db.SubCategories.Include(x=>x.Category).Where(x => x.Name == subCategory.Name && subCategory.CategoryId == x.CategoryId).ToList();
                if (doesSubExist.Count > 0)
                {
                    statusmessage = $"Error : subCategory exist in {doesSubExist.First().Category.Name}";
                }
                else
                {

                    db.SubCategories.Add(subCategory);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
            SubCategoryandCategory model = new SubCategoryandCategory()
            {
                categories = db.Categories.ToList(),
                subCategoryList = db.SubCategories.Select(x => x.Name).ToList(),
                status = statusmessage

            };
            return View(model);
        }


        public IActionResult Edit(int Id)
        {
            ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name", db.SubCategories.Find(Id).CategoryId);
            return View(db.SubCategories.Find(Id));
        }

        [HttpPost]
        public IActionResult Edit(SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                db.Update(subCategory);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int Id)
        {
            db.SubCategories.Remove(db.SubCategories.Find(Id));
            db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult getSubCategories(int Id){
            var subcategories = db.SubCategories.Where(x => x.CategoryId == Id).ToList();
            return PartialView(subcategories);
        }

        public JsonResult getSubCategoriesv2(int Id)
        {
            var subcategories = db.SubCategories.Where(x => x.CategoryId == Id).ToList();
            return Json(subcategories);

        }
    }
}
