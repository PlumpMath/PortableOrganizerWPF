using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OrganizerTask1.UI.Misc;
using OrganizerTask1.UI.ViewModels;
using OrganizerTasks1.DAL;
using Moq;
using OrganizerTask1.UI.ViewModels.Interfaces;
using OrganizerTasks1.Model;

namespace OrganizerTask1.VMUnitTest
{
    [TestFixture]
    class NotesVMTest
    {
        private NotesViewModel notesVM;

        private INotificationCenter notificationCenter;
        private Mock<INotificationCenter> mockNotificationCenter;
        private NotificationArgs notificationArgs;

        [SetUp]
        public void SetUp()
        {
            List<Note> notes = new List<Note>
            {
                new Note {Name = "TestNote1", Description = "TestDescription1"},
                new Note {Name = "TestNote2", Description = "TestDescription2"}
            };

            var stubDataProvider = new Mock<IDataProvider>();
            stubDataProvider.SetupAllProperties();
            stubDataProvider.Setup(dp => dp.Notes).Returns(notes);
            stubDataProvider.Setup(dp => dp.GetCollection<Note>()).Returns(notes);

            mockNotificationCenter = new Mock<INotificationCenter>();
            mockNotificationCenter.Setup(nc => nc.PostNotification(It.IsAny<NotificationName>(), It.IsAny<NotificationArgs>()))
                .Callback<NotificationName, NotificationArgs>((nName, nArgs) => notificationArgs = nArgs);
            notificationCenter = mockNotificationCenter.Object;

            notesVM = new NotesViewModel(stubDataProvider.Object, notificationCenter);
        }

        private NoteViewModel ConvertResultToNoteViewModel()
        {
            Assert.IsInstanceOf<NotificationArgsItemEditModalShow>(notificationArgs);
            var args = notificationArgs as NotificationArgsItemEditModalShow;

            Assert.IsInstanceOf<IItemEditorVM>(args.ModalViewModel);
            var editor = args.ModalViewModel as IItemEditorVM;

            Assert.IsInstanceOf<NoteViewModel>(editor.EditingItem);
            return editor.EditingItem as NoteViewModel;
        }

        [Test]
        public void NoteItemCreationTest()
        {
            notesVM.NewCommand.Execute(null);

            mockNotificationCenter.Verify(nc => 
                nc.PostNotification(NotificationName.SHOW_ITEM_EDIT_MODAL, 
                It.IsAny<NotificationArgsItemEditModalShow>()),
                Times.Once);

            var item = ConvertResultToNoteViewModel();

            Assert.IsTrue(string.IsNullOrEmpty(item.Name));
            Assert.IsTrue(string.IsNullOrEmpty(item.Description));
        }

        [Test]
        public void NoteEditTest()
        {
            notesVM.SelectedEntity = notesVM.Entities.ToList().Find((n) => n.Name == "TestNote1");
            Assert.IsNotNull(notesVM.SelectedEntity);
            notesVM.EditCommand.Execute(null);

            var item = ConvertResultToNoteViewModel();

            Assert.AreEqual(item.Name, "TestNote1");
            Assert.AreEqual(item.Description, "TestDescription1");
        }

        [Test]
        public void NoteRemovalTest()
        {
            int oldCount = notesVM.Entities.Count;
            notesVM.SelectedEntity = notesVM.Entities.ToList().Find((n) => n.Name == "TestNote1");
            Assert.IsNotNull(notesVM.SelectedEntity);
            notesVM.DeleteCommand.Execute(null);

            var deletedItem = notesVM.Entities.ToList().Find((n) => n.Name == "TestNote1");
            Assert.IsNull(deletedItem);
            int newCount = notesVM.Entities.Count;
            Assert.AreEqual(1, (oldCount - newCount));
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
