using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodeSearch.Data;

namespace CodeSearch.Controllers
{
    [Authorize]
    public class ProjectCategoriesController : Controller
    {
        private CodeSearchModelContainer db = new CodeSearchModelContainer();

        // GET: ProjectCategories
        public ActionResult Index()
        {
            var projectCategories = db.ProjectCategories.Include(p => p.Project).Include(p => p.Category);
            return View(projectCategories.ToList());
        }

        // GET: ProjectCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectCategories projectCategories = db.ProjectCategories.Find(id);
            if (projectCategories == null)
            {
                return HttpNotFound();
            }
            return View(projectCategories);
        }

        // GET: ProjectCategories/Create
        public ActionResult Create()
        {
            ViewBag.ProjectProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
            ViewBag.CategoryCategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: ProjectCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectProjectId,CategoryCategoryId")] ProjectCategories projectCategories)
        {
            if (ModelState.IsValid)
            {
                db.ProjectCategories.Add(projectCategories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", projectCategories.ProjectProjectId);
            ViewBag.CategoryCategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", projectCategories.CategoryCategoryId);
            return View(projectCategories);
        }

        // GET: ProjectCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectCategories projectCategories = db.ProjectCategories.Find(id);
            if (projectCategories == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", projectCategories.ProjectProjectId);
            ViewBag.CategoryCategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", projectCategories.CategoryCategoryId);
            return View(projectCategories);
        }

        // POST: ProjectCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectProjectId,CategoryCategoryId")] ProjectCategories projectCategories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectCategories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", projectCategories.ProjectProjectId);
            ViewBag.CategoryCategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", projectCategories.CategoryCategoryId);
            return View(projectCategories);
        }

        // GET: ProjectCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectCategories projectCategories = db.ProjectCategories.Find(id);
            if (projectCategories == null)
            {
                return HttpNotFound();
            }
            return View(projectCategories);
        }

        // POST: ProjectCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectCategories projectCategories = db.ProjectCategories.Find(id);
            db.ProjectCategories.Remove(projectCategories);
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
