using CodeSearch.Data;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.Security.Application;
using CodeSearch.Helpers;
using System.Data.Entity;

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

        public void CreateNewSnippet(SnippetsViewModel model)
        {
            Snippet snippet = new Snippet
            {
                SnippetName = Sanitizer.GetSafeHtmlFragment(model.SnippetName),
                SnippetContent = HtmlSanitizer.SanitizeHtml(model.SnippetContent),
                SnippetDescription = Sanitizer.GetSafeHtmlFragment(model.SnippetDescription),
                ReferenceURL = Sanitizer.GetSafeHtmlFragment(model.ReferenceUrl),
                SnippetLanguage = Sanitizer.GetSafeHtmlFragment(model.SnippetLanguage)
            };

            CategorySnippetAssociations snippetCategory = new CategorySnippetAssociations
            {
                SnippetAssociationId = snippet.SnippetId,
                CategoryAssociationId = model.SelectedCategoryId
            };

            if (model.NoteList != null)
            {
                for (int i = 0; i < model.NoteCount; i++)
                {
                    Note newNote = new Note
                    {
                        NoteSnippetId = snippet.SnippetId,
                        NoteTitle = model.NoteList[i].NoteTitle,
                        NoteContent = model.NoteList[i].NoteContent,
                        NoteCount = model.NoteCount,
                    };

                    db.Notes.Add(newNote);
                }
            }

            db.CategorySnippetAssociations.Add(snippetCategory);
            db.Snippets.Add(snippet);
            db.SaveChanges();
        }

        public SnippetsViewModel EditSnippet(SnippetsViewModel model, int id)
        {
            Snippet snippet = db.Snippets.Find(id);

            CategorySnippetAssociations snippetCategoryAssociation = db.CategorySnippetAssociations.Find(id);

            //If we have new notes being passed to the controller
            if (model.NoteList.Any() && model.NoteList != null)
            {
                //Remove all of the previous notes
                List<Note> removeNotes = (from n in db.Notes
                                   where n.NoteSnippetId == id
                                   select n).ToList();

                for (int i = 0; i < removeNotes.Count(); i++)
                {
                    db.Notes.Remove(removeNotes[i]);
                }

                //Add all the new notes
                for (int i = 0; i < model.NoteCount; i++)
                {
                    Note newNote = new Note
                    {
                        NoteSnippetId = snippet.SnippetId,
                        NoteTitle = model.NoteList[i].NoteTitle,
                        NoteContent = model.NoteList[i].NoteContent,
                        NoteCount = model.NoteCount
                    };

                    db.Notes.Add(newNote);
                }
            }

            snippet.SnippetName = Sanitizer.GetSafeHtmlFragment(model.SnippetName);
            snippet.SnippetContent = HtmlSanitizer.SanitizeHtml(model.SnippetContent);
            snippet.SnippetDescription = Sanitizer.GetSafeHtmlFragment(model.SnippetDescription);
            snippet.ReferenceURL = Sanitizer.GetSafeHtmlFragment(model.ReferenceUrl);
            snippet.SnippetLanguage = Sanitizer.GetSafeHtmlFragment(model.SnippetLanguage);

            snippetCategoryAssociation.CategoryAssociationId = model.SelectedCategoryId;

            db.Entry(snippetCategoryAssociation).State = EntityState.Modified;
            db.Entry(snippet).State = EntityState.Modified;
            db.SaveChanges();

            return model;
        }

        public void DeleteSnippet(int id)
        {
            Snippet snippet = db.Snippets.Find(id);
            CategorySnippetAssociations snippetCategory = db.CategorySnippetAssociations.Find(id);

            var allNotes = (from n in db.Notes
                            where n.NoteSnippetId == id
                            select n).ToList();

            foreach (Note note in allNotes)
            {
                db.Notes.Remove(note);
            }

            db.Snippets.Remove(snippet);
            db.CategorySnippetAssociations.Remove(snippetCategory);

            db.SaveChanges();
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