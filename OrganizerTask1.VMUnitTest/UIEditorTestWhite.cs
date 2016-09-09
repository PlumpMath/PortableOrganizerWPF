using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OrganizerTask1.VMUnitTest.Base;
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
        private Windows windows = new Windows();

        private Application app;
        private Window mainWindow;

        [OneTimeSetUp]
        public void SetUp()
        {
            var path = @"C:\Users\VSoldatenko\MyApps\OrganizerTasks1\OrganizerTask1.UI\bin\Debug\OrganizerTask1.UI.exe";
            app = Application.Launch(path);

            windows.Init(app);
            windows.MainWindow.OpenNotes();
        }

        [Test]
        public void AddNewItemTest()
        {
            windows.MainWindow.AddNote("Test White added Note", "Test generated description");
            bool isNoteAdded = windows.MainWindow.HasNote("Test White added Note");

            Assert.IsTrue(isNoteAdded);
        }

        [Test] 
        public void EditItemTest()
        {
            string newName = Guid.NewGuid().ToString();

            Assert.IsFalse(windows.MainWindow.IsNotesListEmpty(), "Notes list is empty");
            windows.MainWindow.EditNote(0, newName, false);

            Assert.IsFalse(windows.MainWindow.HasNote(newName));

            windows.MainWindow.EditNote(0, newName, true);

            Assert.IsTrue(windows.MainWindow.HasNote(newName));
        }

        [Test]
        public void RemoveItemTest()
        {
            Assert.IsFalse(windows.MainWindow.IsNotesListEmpty(), "Notes list is empty");

            bool deletionResult = windows.MainWindow.RemoveLastNote();
            Assert.IsTrue(deletionResult, "Not hasn't been deleted");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            app.Close();
        }
    }
}
