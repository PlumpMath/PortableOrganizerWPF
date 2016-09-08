using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;
using Assert = NUnit.Framework.Assert;

namespace OrganizerTask1.VMUnitTest
{
    [TestFixture]
    public class UIEditorTestWhite
    {
        private Application app;
        private Window mainWindow;

        [OneTimeSetUp]
        public void SetUp()
        {
            var path = @"C:\Users\VSoldatenko\MyApps\OrganizerTasks1\OrganizerTask1.UI\bin\Debug\OrganizerTask1.UI.exe";
            app = Application.Launch(path);

            mainWindow = app.GetWindow("Oraganizer Task");

            var notesButton = mainWindow.Get<Button>("Notes");
            notesButton.Click();
        }

        [Test]
        public void AddNewItemTest()
        {
            string newNoteName = "Test White added Note";
            var addButton = mainWindow.Get<Button>(SearchCriteria.ByText("New"));
            addButton.Click();

            mainWindow.Get<TextBox>("NameInput").Text = newNoteName;
            mainWindow.Get<TextBox>("DescriptionInput").Text = "Test generated description";

            var saveButton = mainWindow.Get<Button>(SearchCriteria.ByText("Save"));
            saveButton.Click();

            var notesList = mainWindow.Get<ListBox>("notesControl");
            var item = notesList.Item(newNoteName);

            Assert.IsNotNull(item);
        }

        private void EditItemNameChange(string editedNoteName)
        {
            var editButton = mainWindow.Get<Button>(SearchCriteria.ByText("Edit"));
            editButton.Click();
            mainWindow.Get<TextBox>("NameInput").Text = editedNoteName;
        }

        [Test] 
        public void EditItemTest()
        {
            string editedNoteName = Guid.NewGuid().ToString();
            var notesList = mainWindow.Get<ListBox>("notesControl");
            int count = notesList.Items.ToList().Count;
            Assert.GreaterOrEqual(count, 1);
            notesList.Select(0);

            EditItemNameChange(editedNoteName);

            var discardButton = mainWindow.Get<Button>(SearchCriteria.ByText("Discard"));
            discardButton.Click();

            Assert.IsFalse(notesList.SelectedItemText == editedNoteName);

            EditItemNameChange(editedNoteName);

            var saveButton = mainWindow.Get<Button>(SearchCriteria.ByText("Save"));
            saveButton.Click();

            var item = notesList.Item(editedNoteName);
            Assert.IsNotNull(item);
            Assert.IsTrue(notesList.SelectedItemText == editedNoteName);
        }

        [Test]
        public void RemoveItemTest()
        {
            var notesList = mainWindow.Get<ListBox>("notesControl");
            int count = notesList.Items.ToList().Count;
            Assert.GreaterOrEqual(count, 1);
            notesList.Select(count-1);

            var item = notesList.SelectedItem;

            var deleteButton = mainWindow.Get<Button>(SearchCriteria.ByText("Delete"));
            deleteButton.Click();

            Assert.IsFalse(notesList.Items.Contains(item));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            app.Close();
        }
    }
}
