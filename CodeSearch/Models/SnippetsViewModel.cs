using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeSearch.ViewModels
{
    public class SnippetsViewModel
    {
        public Data.Snippet Snippets { get; set; }

        public Data.Category Categories { get; set; }
        public Data.Note Notes { get; set; }
        public Data.Tag Tags { get; set; }

        [Required]
        public string SnippetName { get; set; }

        [Required]
        public string SnippetDescription { get; set; }

        public List<Data.Category> CategoryList;
        public int selectedCategory;
    }
}