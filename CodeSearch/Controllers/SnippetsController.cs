using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CodeSearch.Data;
using CodeSearch.ViewModels;
using CodeSearch.Helpers;
using System.Text;
using Microsoft.Security.Application;
using System.Web;

namespace CodeSearch.Controllers
{
    [Authorize]
    public class SnippetsController : Controller
    {
        private CodeSearchModelContainer db = new CodeSearchModelContainer();

        // GET: Snippets
        public ActionResult Index()
        {
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

            var selectedCatId = (from csa in db.CategorySnippetAssociations
                                 where csa.SnippetAssociationId == id
                                 select csa.Category.CategoryId).FirstOrDefault();

            Snippet snippet = db.Snippets.Find(id);
            HttpUtility.HtmlDecode(snippet.SnippetContent);

            SnippetsViewModel snippetViewModel = new SnippetsViewModel
            {
                Snippets = snippet,
                CategoryList = categories.ToList<Category>(),
                selectedCategory = selectedCatId
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
        [ValidateInput(false)]
        public ActionResult Create(SnippetsViewModel model, int categoryList, string SnippetLanguage)
        {
            if (ModelState.IsValid)
            {
                string snippetString = model.Snippets.SnippetContent.ToString();

                var newSnippet = new Snippet
                {
                    SnippetName = Sanitizer.GetSafeHtmlFragment(model.SnippetName),
                    SnippetContent = HtmlSanitizer.SanitizeHtml(model.Snippets.SnippetContent),
                    SnippetDescription = Sanitizer.GetSafeHtmlFragment(model.SnippetDescription),
                    ReferenceURL = Sanitizer.GetSafeHtmlFragment(model.Snippets.ReferenceURL),
                    SnippetLanguage = Sanitizer.GetSafeHtmlFragment(SnippetLanguage)
                };

                var snippetCategory = new CategorySnippetAssociations
                {
                    SnippetAssociationId = newSnippet.SnippetId,
                    CategoryAssociationId = categoryList
                };

                db.CategorySnippetAssociations.Add(snippetCategory);
                db.Snippets.Add(newSnippet);
                db.SaveChanges();
                
                TempData["SuccessMessage"] = "<div class='alert alert-success w-fade-out'><strong> Success!</strong> New Code Snippet Created</div>";
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

            //Get All Categories
            var categories = from c in db.Categories
                             where c.CategoryName != ""
                             select c;

            //Get Assigned Category based on Snippet Id
            /* var selectedCat = (from csa in db.CategorySnippetAssociations
                               join c in db.Categories on csa.CategoryAssociationId equals c.CategoryId
                               where csa.SnippetAssociationId == id
                               select c.CategoryName).FirstOrDefault(); */

            var selectedCatId = (from csa in db.CategorySnippetAssociations
                               where csa.SnippetAssociationId == id
                               select csa.Category.CategoryId).FirstOrDefault();

            Snippet snippet = db.Snippets.Find(id);
            SnippetsViewModel snippetViewModel = new SnippetsViewModel
            {
                Snippets = snippet,
                SnippetName = snippet.SnippetName,
                SnippetDescription = snippet.SnippetDescription,
                CategoryList = categories.ToList<Category>(),
                selectedCategory = selectedCatId
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
        [ValidateInput(false)]
        public ActionResult Edit(int id, SnippetsViewModel model, int categoryList, string SnippetLanguage)
        {
            Snippet snippet = db.Snippets.Find(id);

            CategorySnippetAssociations snippetCategoryAssociation = db.CategorySnippetAssociations.Find(id);

            if (ModelState.IsValid) {
       
                if (snippet == null || snippetCategoryAssociation == null)
                {
                    return new HttpNotFoundResult();
                }

                snippet.SnippetName = Sanitizer.GetSafeHtmlFragment(model.SnippetName);
                snippet.SnippetContent = HtmlSanitizer.SanitizeHtml(model.Snippets.SnippetContent);
                snippet.SnippetDescription = Sanitizer.GetSafeHtmlFragment(model.SnippetDescription);
                snippet.ReferenceURL = Sanitizer.GetSafeHtmlFragment(model.Snippets.ReferenceURL);
                snippet.SnippetLanguage = Sanitizer.GetSafeHtmlFragment(SnippetLanguage);

                snippetCategoryAssociation.CategoryAssociationId = categoryList;

                db.Entry(snippetCategoryAssociation).State = EntityState.Modified;
                db.Entry(snippet).State = EntityState.Modified;
                db.SaveChanges();

                TempData["UpdateMessage"] = "<div class='alert alert-info w-fade-out'>Code Snippet Successfully Updated!</div>";

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

            var selectedCatId = (from csa in db.CategorySnippetAssociations
                                 where csa.SnippetAssociationId == id
                                 select csa.Category.CategoryId).FirstOrDefault();

            Snippet snippet = db.Snippets.Find(id);
            SnippetsViewModel snippetViewModel = new SnippetsViewModel
            {
                Snippets = snippet,
                CategoryList = categories.ToList<Category>(),
                selectedCategory = selectedCatId

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

            TempData["DeleteMessage"] = "<div class='alert alert-info w-fade-out'>Code Snippet Successfully Removed!</div>";

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
