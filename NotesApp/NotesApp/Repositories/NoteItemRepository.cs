using NotesApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;

namespace NotesApp.Repositories
{
    public class NoteItemRepository : INoteItemRepository
    {
        private SQLiteAsyncConnection connection;
        public event EventHandler<NoteItem> OnItemAdded;
        public event EventHandler<NoteItem> OnItemUpdated;

        public async Task<List<NoteItem>> GetItems()
        {
            await CreateConnection();
            return await connection.Table<NoteItem>().ToListAsync();
        }
        public async Task AddItem(NoteItem item)
        {
            await CreateConnection();
            await connection.InsertAsync(item);
            OnItemAdded?.Invoke(this, item);
        }
        public async Task UpdateItem(NoteItem item)
        {
            await CreateConnection();
            await connection.UpdateAsync(item);
            OnItemUpdated?.Invoke(this, item);
        }
        public async Task AddOrUpdate(NoteItem item)
        {
            if (item.Id == 0)
            {
                await AddItem(item);
            }
            else
            {
                await UpdateItem(item);
            }
        }
        private async Task CreateConnection()
        {
            if (connection != null) { return; }
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var databasePath = Path.Combine(documentPath, "NoteItems.db");

            connection = new SQLiteAsyncConnection(databasePath);
            await connection.CreateTableAsync<NoteItem>();

            if(await connection.Table<NoteItem>().CountAsync() == 0)
            {
                await connection.InsertAsync(new NoteItem()
                {
                    Title = "Welcome to Notes App",
                    DateCreated = DateTime.Now
                }); 
            }
        }
    }
}
