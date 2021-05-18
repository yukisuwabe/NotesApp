using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using NotesApp.Models;

namespace NotesApp.ViewModels
{
    public class NoteItemViewModel : ViewModel 
    {
        public NoteItemViewModel(NoteItem item) => Item = item;

        public event EventHandler ItemStatusChanged;
        public NoteItem Item { get; private set; }
    }
}
