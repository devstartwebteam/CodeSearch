using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSearch.ViewModels
{
    public class ProjectsViewModel
    {
        public Data.Project Projects;

        public List<Data.Snippet> SnippetList;
        public int snippetCount;

    }
}