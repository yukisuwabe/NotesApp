using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotesApp.Models;

namespace NotesApp.Repositories
{
    public interface INoteItemRepository
    {
        event EventHandler<NoteItem> OnItemAdded;
        event EventHandler<NoteItem> OnItemUpdated;

        Task<List<NoteItem>> GetItems();
        Task AddItem(NoteItem item);
        Task UpdateItem(NoteItem item);
        Task AddOrUpdate(NoteItem item);
    }
}
