using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Linq;
using NotesApp.Views;
using NotesApp.Repositories;
using NotesApp.Models;
using Xamarin.Forms;

namespace NotesApp.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly NoteItemRepository repository;
        public ObservableCollection<NoteItemViewModel> Items { get; set; }

        public MainViewModel(NoteItemRepository repository)
        {
            repository.OnItemAdded += (sender, item) => Items.Add(CreateNoteItemViewModel(item));
            repository.OnItemUpdated += (sender, item) => Task.Run(async () => await LoadData());

            this.repository = repository;
            Task.Run(async () => await LoadData());
        }

        private async Task LoadData()
        {
            var items = await repository.GetItems();
            var itemViewModels = items.Select(i => CreateNoteItemViewModel(i));
            Items = new ObservableCollection<NoteItemViewModel>(itemViewModels);
        }
        private NoteItemViewModel CreateNoteItemViewModel(NoteItem item)
        {
            var itemViewModel = new NoteItemViewModel(item);
            return itemViewModel;
        }

        public ICommand AddItem => new Command(async () =>
        {
            var itemView = Resolver.Resolve<ItemView>();
            await Navigation.PushAsync(itemView);
        });

        public NoteItemViewModel SelectedItem
        {
            get { return null; }
            set
            {
                Device.BeginInvokeOnMainThread(async () => await
                NavigateToItem(value));
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        private async Task NavigateToItem(NoteItemViewModel item)
        {
            if(item == null)
            {
                return;
            }

            var itemView = Resolver.Resolve<ItemView>();
            var vm = itemView.BindingContext as ItemViewModel;
            vm.Item = item.Item;
            await Navigation.PushAsync(itemView);
        }
    }
}
