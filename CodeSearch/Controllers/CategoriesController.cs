using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodeSearch.Models;
using CodeSearch.Models.Data;
using CodeSearch.ViewModels;

namespace CodeSearch.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private CodeSearchModelContainer db = new CodeSearchModelContainer();
        
        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View(new ImageViewModel());
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName,CategoryDescription,Created,Modified")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(ImageViewModel model)
        {
            var validImageTypes = new string[]
            {
        "image/gif",
        "image/jpeg",
        "image/pjpeg",
        "image/png"
            };

            if (model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
            {
                    ModelState.AddModelError("ImageUpload", "This field is required");
            }

            else if (!validImageTypes.Contains(model.ImageUpload.ContentType))
            {
                    ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }

            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName,
                    ImageURL = model.ImageURL,
                    CategoryDescription = model.CategoryDescription
                };
        
        if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "/uploads";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, model.ImageUpload.FileName);
                    model.ImageUpload.SaveAs(imagePath);
                    category.ImageURL = imageUrl;
                }

                db.Categories.Add(category);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Categories/Edit/5
        /*
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }*/

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName,CategoryDescription,Created,Modified")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }*/


        public ActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return new HttpNotFoundResult();
            }

            var categoryImage = new ImageViewModel
            {
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
                ImageURL = category.ImageURL
            };

            return View(categoryImage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ImageViewModel model)
        {
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if ((model.ImageUpload != null || model.ImageUpload.ContentLength > 0) && !validImageTypes.Contains(model.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }

            if (ModelState.IsValid)
            {
                var categoryImage = db.Categories.Find(id);
                if (categoryImage == null)
                {
                    return new HttpNotFoundResult();
                }

                categoryImage.CategoryName = model.CategoryName;
                categoryImage.CategoryDescription = model.CategoryDescription;

                if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "/uploads";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, model.ImageUpload.FileName);
                    model.ImageUpload.SaveAs(imagePath);
                    categoryImage.ImageURL = imageUrl;
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            var categoryImage = new ImageViewModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
                ImageURL = category.ImageURL
            };

            return View(categoryImage);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
