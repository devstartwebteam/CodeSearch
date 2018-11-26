using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeSearch.ViewModels
{
    public class SnippetsViewModel
    {
        public Data.Snippet Snippets { get; set; }

        [Required]
        public string SnippetName { get; set; }

        [Required]
        public string SnippetDescription { get; set; }

        [Required]
        public string SnippetContent { get; set; }

        public List<Data.Tag> TagList;
        public int[] TagNum;

        public List<Data.Note> NoteList;
        public int noteCount;

        public List<Data.Category> CategoryList;
        public int selectedCategory;
    }
}