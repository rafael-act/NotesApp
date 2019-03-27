using NotesApp.Model;
using NotesApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.ViewModel
{
    public class NotesVM
    {
        public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            ReadNotebooks();
            ReadNotes();
        }
        public ObservableCollection<Notebook> Notebooks { get; set; }
        private Notebook selectedNotebook;

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                ReadNotes();
                NewNoteCommand.CanExecute(selectedNotebook);
            }
        }

        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }

        public void CreateNote(int notebookID)
        {
            Note newNote = new Note()
            {
                NotebookId = notebookID,
                CreateTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                Title = "New Note"
            };

            DatabaseHelper.Insert(newNote);
            ReadNotes();
        }

        public void CreateNotebook()
        {
            Notebook newNotebook = new Notebook()
            {
                Name = "New notebook"
                , UserId = int.Parse(App.UserId)
            };
            DatabaseHelper.Insert(newNotebook);

            ReadNotebooks();
        }

        public void ReadNotebooks()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile))
            {
                try
                {
                    var notebooks = conn.Table<Notebook>().ToList();
                    Notebooks.Clear();
                    foreach (var notebook in notebooks)
                    {
                        Notebooks.Add(notebook);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("no such table: Notebook"))
                    {
                        conn.CreateTable<Notebook>();
                    }
                }
            }
        }

        public void ReadNotes()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile))
            {
                if (SelectedNotebook != null)
                {
                    try
                    {
                        var notes = conn.Table<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();

                        Notes.Clear();
                        foreach (var note in notes)
                        {
                            Notes.Add(note);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("no such table: Notebook"))
                        {
                            conn.CreateTable<Note>();
                        }
                    }
                    
                }
            }
        }
    }
}
