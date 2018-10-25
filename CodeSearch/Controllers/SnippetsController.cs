using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CodeSearch.Models.Data;
using CodeSearch.ViewModels;

namespace CodeSearch.Controllers
{
    public class SnippetsController : Controller
    {
        private CodeSearchModelContainer db = new CodeSearchModelContainer();

        // GET: Snippets
        public ActionResult Index()
        {
            //var snippets = db.Snippets.Include(x => x.CategorySnippetAssociations);

            /*var snippets = from Snippets in db.Snippets
                           from Categories in db.Categories
                           select new {
                               SnippetName = Snippets.SnippetName,
                               SnippetDescription = Snippets.SnippetDescription,
                               ReferenceURL = Snippets.ReferenceURL,
                               CategoryName = Categories.CategoryName
                           };*/

            var snippets = db.Snippets.Include(x => x.CategorySnippetAssociations);

            return View(snippets.ToList());
        }

        // GET: Snippets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var categories = from r in db.Categories
                             where r.CategoryName != ""
                             select r;

            Snippet snippet = db.Snippets.Find(id);

            SnippetsViewModel snippetViewModel = new SnippetsViewModel
            {
                CategoryList = categories.ToList<Category>(),
                Snippets = snippet
            };

            if (snippet == null)
            {
                return HttpNotFound();
            }
            return View(snippetViewModel);
        }

        // GET: Snippets/Create
        public ActionResult Create()
        {
            var categories = from r in db.Categories
                             where r.CategoryName != ""
                             select r;

            SnippetsViewModel model = new SnippetsViewModel();
            model.CategoryList = categories.ToList<Category>();

            return View(model);
        }

        // POST: Snippets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SnippetsViewModel model, int categoryList, string SnippetLanguage)
        {
            if (ModelState.IsValid)
            {
                var newSnippet = new Snippet
                {
                    SnippetId = model.Snippets.SnippetId,
                    SnippetName = model.Snippets.SnippetName,
                    SnippetContent = model.Snippets.SnippetContent,
                    SnippetDescription = model.Snippets.SnippetDescription,
                    ReferenceURL = model.Snippets.ReferenceURL,
                    SnippetLanguage = SnippetLanguage

                };

                var snippetCategory = new CategorySnippetAssociations
                {
                    SnippetAssociationId = model.Snippets.SnippetId,
                    CategoryAssociationId = categoryList
                };


                db.CategorySnippetAssociations.Add(snippetCategory);
                db.Snippets.Add(newSnippet);
                db.SaveChanges();
                
            }

            return RedirectToAction("Index");

        }

        // GET: Snippets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var categories = from r in db.Categories
                             where r.CategoryName != ""
                             select r;

            Snippet snippet = db.Snippets.Find(id);
            SnippetsViewModel snippetViewModel = new SnippetsViewModel
            {
                CategoryList = categories.ToList<Category>(),
                Snippets = snippet
            };

            if (snippet == null)
            {
                return HttpNotFound();
            }
            return View(snippetViewModel);
        }

        // POST: Snippets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SnippetsViewModel model, int categoryList, string SnippetLanguage)
        {
            Snippet snippet = db.Snippets.Find(id);

            CategorySnippetAssociations snippetCategory = db.CategorySnippetAssociations.Find(id);

            if (ModelState.IsValid) {
       
                if (snippet == null || snippetCategory == null)
                {
                    return new HttpNotFoundResult();
                }

                snippet.SnippetName = model.Snippets.SnippetName;
                snippet.SnippetContent = model.Snippets.SnippetContent;
                snippet.SnippetDescription = model.Snippets.SnippetDescription;
                snippet.ReferenceURL = model.Snippets.ReferenceURL;
                snippet.SnippetLanguage = SnippetLanguage;

                snippetCategory.CategoryAssociationId = categoryList;

                db.Entry(snippetCategory).State = EntityState.Modified;
                db.Entry(snippet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(snippet);
        }

        // GET: Snippets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Get All Categories
            var categories = from r in db.Categories
                             where r.CategoryName != ""
                             select r;

            Snippet snippet = db.Snippets.Find(id);
            SnippetsViewModel snippetViewModel = new SnippetsViewModel
            {
                CategoryList = categories.ToList<Category>(),
                Snippets = snippet
            };

            if (snippet == null)
            {
                return HttpNotFound();
            }
            return View(snippetViewModel);
        }

        // POST: Snippets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id )
        {
            Snippet snippet = db.Snippets.Find(id);
            CategorySnippetAssociations snippetCategory = db.CategorySnippetAssociations.Find(id);

            db.Snippets.Remove(snippet);
            db.CategorySnippetAssociations.Remove(snippetCategory);

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

        //Search
        public ActionResult Search(string id)
        {
            string searchString = id;
            var snippets = from s in db.Snippets
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                snippets = snippets.Where(s => s.SnippetName.Contains(searchString));
            }

            return View(snippets);
        }
    }
}
