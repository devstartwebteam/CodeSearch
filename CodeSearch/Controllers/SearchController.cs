using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeSearch.Models;
using CodeSearch.Data;

namespace CodeSearch.Controllers
{
    public class SearchController : Controller
    {
        private CodeSearchModelContainer db = new CodeSearchModelContainer();

        //Search
        public ActionResult Results(string criteria)
        {
            string searchString = criteria;
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