using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Resturant.Data;
using Resturant.Models;

namespace Resturant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CouponController : Controller
    {
        ApplicationDbContext db;
        IWebHostEnvironment hosting;
        public CouponController(ApplicationDbContext _db,IWebHostEnvironment _hosting)
        {
            db = _db;
            hosting = _hosting;
        }
        public IActionResult Index()
        {
            
            return View(db.Coupons.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Coupon coupon,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if(file != null)
                {

                coupon.Image = file.FileName;
                var filePath = hosting.WebRootPath + "/Images";
                using (var stream = new FileStream(Path.Combine(filePath, file.FileName), FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }
                }
                db.Coupons.Add(coupon);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(coupon);
            }
        }

        public IActionResult Edit(int id)
        {
            var coupon = db.Coupons.Find(id);
            return View(coupon);
        }

        [HttpPost]
        public IActionResult Edit(Coupon coupon,IFormFile file)
        {
            if (ModelState.IsValid)
            {


                if (file == null)
                {
                }
                else
                {
                    coupon.Image = file.FileName;
                    var filePath = hosting.WebRootPath + "/Images";
                    using (var stream = new FileStream(Path.Combine(filePath, file.FileName), FileMode.Create))
                    {
                        file.CopyToAsync(stream);
                    }
                }
                db.Update(coupon);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            else
            {
              
                return View(coupon);
            }
        }

        public IActionResult Delete(int id)
        {
            db.Coupons.Remove(db.Coupons.Find(id));
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
