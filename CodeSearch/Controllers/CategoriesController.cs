﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodeSearch.Models;
using CodeSearch.Data;
using CodeSearch.ViewModels;
using CodeSearch.Helpers;

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
            return View(new CategoriesViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(CategoriesViewModel model)
        {
            if (ModelState.IsValid)
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

                    string finalImageName = "resize-" + model.ImageUpload.FileName.ToString();

                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, finalImageName);

                    //Resize Image
                    ImageResize.ResizeImage(model.ImageUpload);

                    model.ImageUpload.SaveAs(imagePath);
                    category.ImageURL = imageUrl;
                }

                db.Categories.Add(category);
                db.SaveChanges();

                TempData["SuccessMessage"] = "<div class='alert alert-success w-fade-out'><strong> Success!</strong> New Category Created</div>";
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return new HttpNotFoundResult();
            }

            var categoryImage = new CategoriesViewModel
            {
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
                ImageURL = category.ImageURL
            };

            return View(categoryImage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoriesViewModel model)
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

                    string finalImageName = "resize-" + model.ImageUpload.FileName.ToString();

                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, finalImageName);

                    //Resize Image
                    ImageResize.ResizeImage(model.ImageUpload);

                    model.ImageUpload.SaveAs(imagePath);
                    categoryImage.ImageURL = imageUrl;
                }

                db.SaveChanges();

                TempData["UpdateMessage"] = "<div class='alert alert-info w-fade-out'>Category Successfully Updated!</div>";

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

            var categoryImage = new CategoriesViewModel
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

            TempData["DeleteMessage"] = "<div class='alert alert-info w-fade-out'>Category Successfully Removed!</div>";
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
