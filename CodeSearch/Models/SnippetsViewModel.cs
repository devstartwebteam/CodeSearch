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

        [Required]
        public string SnippetName { get; set; }

        [Required]
        public string SnippetDescription { get; set; }

        public List<Data.Tag> TagList;
        public int[] TagNum;

        public List<Data.Note> NoteList;
        public int[] noteNum;

        public List<Data.Category> CategoryList;
        public int selectedCategory;
    }
}