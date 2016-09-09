using System;
using System.Linq;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;
using NUnit.Framework;

namespace OrganizerTask1.VMUnitTest.Base
{
    public class MainWindow : WindowObject
    {

        public MainWindow(Window window) : base(window)
        {
        }

        public void OpenNotes()
        {
            GetButton("Notes").Click();
        }

        public ListBox GetNotes()
        {
            return window.Get<ListBox>("notesControl");
        }

        public bool IsNotesListEmpty()
        {
            return GetNotes().Items.Count < 1;
        }

        public void AddNote(string name, string description)
        {
            GetButtonByText("New").Click();

            GetTextBlock("NameInput").Text = name;
            GetTextBlock("DescriptionInput").Text = description;

            GetButtonByText("Save").Click();
        }

        public bool HasNote(string name)
        {
            var item = GetNotes().Items.ToList().Find((note) => note.Text == name);
            return item != null;
        }


        public void EditNote(int index, string newName, bool save)
        {
            var notesList = GetNotes();
            int count = notesList.Items.ToList().Count;
            Assert.GreaterOrEqual(count, 1);
            notesList.Select(index);

            GetButtonByText("Edit").Click();

            GetTextBlock("NameInput").Text = newName;

            if (save)
            {
                GetButtonByText("Save").Click();
            }
            else
            {
                GetButtonByText("Discard").Click();
            }
        }

        public bool RemoveLastNote()
        {
            int count = GetNotes().Items.ToList().Count;
            GetNotes().Select(count - 1);

            var item = GetNotes().SelectedItem;

            GetButtonByText("Delete").Click();

            return !GetNotes().Items.Contains(item);
        }
    }
}
