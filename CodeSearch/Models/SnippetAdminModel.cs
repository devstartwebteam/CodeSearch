using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeSearch.Data;

namespace CodeSearch.Models
{
    public class SnippetAdminModel
    {
        public void AddNotes(Snippet noteSnippet, int[] noteNum, List<Note> newNotes, CodeSearchModelContainer db)
        {
            if (noteNum != null)
            {
                foreach (var note in newNotes)
                {
                  Note AddNote = new Note
                  {
                      NoteTitle = note.NoteTitle,
                      NoteContent = note.NoteContent,
                     
                  };
                }
                /*for (int i = 0; i < noteNum.Count(); i++)
                {
                    Note note = new Note();
                    cat_asoc.CategoryId = checkboxList[i];
                    cat_asoc.Type = type;
                    cat_asoc.SubItemtId = item.Id;

                    item.CategoryAssociations.Add(cat_asoc);
                }*/
            }

            //db.Notes.Add(AddNote);
            //db.SaveChanges();
        }
    }
}