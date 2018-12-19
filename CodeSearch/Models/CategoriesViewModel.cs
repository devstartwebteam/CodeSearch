using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSearch.ViewModels
{
    public class CategoriesViewModel
    {
        public Data.Category Category;

        public List<Data.Snippet> SnippetList;
        public int SnippetCount;

        public List<Data.Note> NoteList;
        public int NoteCount;
    }
}