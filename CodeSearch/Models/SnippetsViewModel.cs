using CodeSearch.Data;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CodeSearch.ViewModels
{
    public class SnippetsViewModel
    {
        private CodeSearchModelContainer db = new CodeSearchModelContainer();

        public int SnippetId { get; set; }

        [Required]
        public string SnippetName { get; set; }
    
        [Required]
        public string SnippetDescription { get; set; }
        public string SnippetLanguage { get; set; }

        [Required]
        public string SnippetContent { get; set; }
        public string ReferenceUrl { get; set; }
        public List<Tag> TagList { get; set; }
        public int[] TagNum { get; set; }
        public List<Note> NoteList { get; set; }
        public int? NoteCount { get; set; }
        public List<Category> CategoryList { get; set; }
        public int SelectedCategoryId { get; set; }

        public SnippetsViewModel(int? id)
        {
            Snippet snippet = db.Snippets.Find(id);
            HttpUtility.HtmlDecode(snippet.SnippetContent);

            var categories = from r in db.Categories
                             where r.CategoryName != ""
                             select r;

            var selectedCatId = (from csa in db.CategorySnippetAssociations
                                 where csa.SnippetAssociationId == id
                                 select csa.Category.CategoryId).FirstOrDefault();

            var allNotes = (from n in db.Notes
                            where n.NoteSnippetId == id
                            select n).ToList();

            var noteCount = (from n in db.Notes
                             where n.NoteSnippetId == id
                             select n.NoteCount).FirstOrDefault();

            SnippetId = snippet.SnippetId;
            SnippetName = snippet.SnippetName;
            SnippetDescription = snippet.SnippetDescription;
            SnippetContent = snippet.SnippetContent;
            SnippetLanguage = snippet.SnippetLanguage;
            ReferenceUrl = snippet.ReferenceURL;
            CategoryList = categories.ToList<Category>();
            SelectedCategoryId = selectedCatId;
            NoteList = allNotes;
            NoteCount = noteCount;
        }

        public SnippetsViewModel()
        {     
        }

        public SnippetsViewModel GetSnippetCategories(SnippetsViewModel model)
        {
            var categories = from r in db.Categories
                             where r.CategoryName != ""
                             select r;

            model.CategoryList = categories.ToList<Category>();
            return model;
        }
    }
}