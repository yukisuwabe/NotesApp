using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace NotesApp.Models
{
    public class NoteItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
