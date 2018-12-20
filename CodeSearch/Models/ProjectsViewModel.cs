using CodeSearch.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeSearch.ViewModels
{
    public class ProjectsViewModel
    {
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectDoc { get; set; }
        public List<SelectListItem> SnippetList { get; set; }
        public List<SelectListItem> SelectedSnippets { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public int SelectedCategoryId { get; set; }

    }
}