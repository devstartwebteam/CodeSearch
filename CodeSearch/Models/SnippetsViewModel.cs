using System;
using System.Collections.Generic;
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

        public List<Models.Data.Category> CategoryList;
    }
}