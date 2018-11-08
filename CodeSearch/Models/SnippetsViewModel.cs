using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeSearch.ViewModels
{
    public class SnippetsViewModel
    {
        public Models.Data.Snippet Snippets { get; set; }

        public Models.Data.Category Categories { get; set; }
        public Models.Data.Note Notes { get; set; }
        public Models.Data.Tag Tags { get; set; }

        [Required]
        public string SnippetName { get; set; }

        [Required]
        public string SnippetDescription { get; set; }

        public List<Models.Data.Category> CategoryList;
        public int selectedCategory;
    }
}