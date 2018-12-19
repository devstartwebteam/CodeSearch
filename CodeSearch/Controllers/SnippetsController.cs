using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CodeSearch.Data;
using CodeSearch.ViewModels;
using CodeSearch.Helpers;
using Microsoft.Security.Application;
using System.Web;
using System.Data;

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

            SnippetsViewModel model = new SnippetsViewModel(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Snippets/Create
        public ActionResult Create()
        {          
            SnippetsViewModel model = new SnippetsViewModel();
            model.GetSnippetCategories(model);

            return View(model);
        }

        // POST: Snippets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(SnippetsViewModel model)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            try
            {
                if (ModelState.IsValid)
                {
                    model.CreateNewSnippet(model);

                    TempData["SuccessMessage"] = "<div class='alert alert-success w-fade-out'><strong> Success!</strong> New Code Snippet Created</div>";
                }
            }

            catch(DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator");
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

            SnippetsViewModel model = new SnippetsViewModel(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // POST: Snippets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(SnippetsViewModel model, int id)
        {

            if (ModelState.IsValid) {
       
                if (model == null)
                {
                    return new HttpNotFoundResult();
                }

                model.EditSnippet(model, id);

                TempData["UpdateMessage"] = "<div class='alert alert-info w-fade-out'>Code Snippet Successfully Updated!</div>";

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Snippets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SnippetsViewModel model = new SnippetsViewModel(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // POST: Snippets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id )
        {
            SnippetsViewModel model = new SnippetsViewModel();
            model.DeleteSnippet(id);
            
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
