////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "NotesTest.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace XUnitTestCases.NotesTestCases
{
    using BusinessLayer.Service;
    using CommonLayer.Models;
    using RepositoryLayer.Interface;
    using System;
    using Xunit;
    using Moq;
    public class NotesTest
    {
        /// <summary>
        /// create notes unit test
        /// </summary>
        [Fact]
        public void CreateNotesTest()
        {
            var notesdata = new Mock<INotesAccountManagerRepository>();
            var notes = new NotesAccountManagerService(notesdata.Object);
            var notesModel = new NotesModel()
            {
                Description = "Desciption",
                Title = "Title",
                Color = "pink",
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                UserId = "UserId"
            };

            var result = notes.AddNotes(notesModel);
            Assert.NotNull(result);
        }

        /// <summary>
        /// get notes test
        /// </summary>
        [Fact]
        public void GetNotes()
        {
            var notesData = new Mock<INotesAccountManagerRepository>();
            var notes = new NotesAccountManagerService(notesData.Object);
            var notesModel = new NotesModel()
            {
                UserId = "UserId",
                NoteType = 0
            };

            var result = notes.GetNotes(notesModel.UserId,notesModel.NoteType);
            Assert.NotNull(result);
        }

        /// <summary>
        /// delete note unit test
        /// </summary>
        [Fact]
        public void DeleteNotes()
        {
            var notesData = new Mock<INotesAccountManagerRepository>();
            var notes = new NotesAccountManagerService(notesData.Object);
            var notesModel = new NotesModel()
            {
                Id = 1
            };

            var result = notes.DeleteNotes(notesModel.Id);
            Assert.NotNull(result);
        }

        /// <summary>
        /// update notes unit test
        /// </summary>
        [Fact]
        public void UpdateNotes()
        {
            var notesData = new Mock<INotesAccountManagerRepository>();
            var notes = new NotesAccountManagerService(notesData.Object);
            var notesModel = new NotesModel()
            {
                Id = 1,
                Title = "Title",
                Description = "Description",
                UserId = "UserId",
                Color = "Color",
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            var result = notes.UpdateNotes(notesModel, notesModel.Id);
            Assert.NotNull(result);
        }

    }
}
