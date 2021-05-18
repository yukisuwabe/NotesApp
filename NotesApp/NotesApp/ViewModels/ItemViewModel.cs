using System;
using System.Windows.Input;
using Xamarin.Forms;
using NotesApp.Models;
using NotesApp.Repositories;


namespace NotesApp.ViewModels
{
    public class ItemViewModel : ViewModel
    {
        private readonly NoteItemRepository repository;

        public NoteItem Item { get; set; }

        public ItemViewModel(NoteItemRepository repository)
        {

            this.repository = repository;
            Item = new NoteItem() { DateCreated = DateTime.Now };
        }

        public ICommand Save => new Command(async () =>
        {
            await repository.AddOrUpdate(Item);
            await Navigation.PopAsync();
        });
    }
}
