//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeSearch.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Note
    {
        public int NoteId { get; set; }
        public string NoteTitle { get; set; }
        public string NoteContent { get; set; }
        public int NoteSnippetId { get; set; }
        public Nullable<int> NoteCount { get; set; }
    
        public virtual Snippet Snippet { get; set; }
    }
}
