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
    using System.Collections.Generic;

    public class NotesTest
    {
        /// <summary>
        /// create notes unit test
        /// </summary>
       
        //[Fact]
        //public void CreateNotesTest()
        //{
        //    var notesdata = new Mock<INotesAccountManagerRepository>();
        //    var notes = new NotesAccountManagerService(notesdata.Object);
        //    var notesModel = new NotesModel()
        //    {
        //        Description = "Desciption",
        //        Title = "Title",
        //        Color = "pink",
        //        CreateDate = DateTime.Now,
        //        ModifiedDate = DateTime.Now,
        //        UserId = "UserId"
        //    };

        //    var result = notes.AddNotes(notesModel);
        //    Assert.NotNull(result);
        //}

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
        /// Delete notes unit test case
        /// </summary>
        [Fact]
        public async void DeleteNotesEqualsTest()
        {
            var notesData = new Mock<INotesAccountManagerRepository>();
            var notes = new NotesAccountManagerService(notesData.Object);
            var notesModel = new NotesModel()
            {
                Id = 5
            };

            object excepted = 1;
            object actual = await notes.DeleteNotes(notesModel.Id);
            Assert.NotEqual(excepted, actual);
        }

        /// <summary>
        /// update notes unit test
        /// </summary>
        //[Fact]
        //public void UpdateNotes()
        //{
        //    var notesData = new Mock<INotesAccountManagerRepository>();
        //    var notes = new NotesAccountManagerService(notesData.Object);
        //    var notesModel = new NotesModel()
        //    {
        //        Id = 1,
        //        Title = "Title",
        //        Description = "Description",
        //        UserId = "UserId",
        //        Color = "Color",
        //        CreateDate = DateTime.Now,
        //        ModifiedDate = DateTime.Now,
        //    };

        //    var result = notes.UpdateNotes(notesModel, notesModel.Id,2);
        //    Assert.NotNull(result);
        //}

        /// <summary>
        /// add collabration not null test case
        /// </summary>
        [Fact]
        public void AddCollabrationNotNull()
        {
            var notesData = new Mock<INotesAccountManagerRepository>();
            var notes = new NotesAccountManagerService(notesData.Object);
            var collaborations = new NotesCollaboration()
            {
                NoteId = 1,
                UserId = "userId",
                CreatedBy = "createdBy"
            };

            var result = notes.AddCollabration(collaborations);
            Assert.NotNull(result);
        }

        [Fact]
        public async void BulkTrashService()
        {
            var notesData = new Mock<INotesAccountManagerRepository>();
            var notes = new NotesAccountManagerService(notesData.Object);
            IList<int> list = new List<int>();
            object result = await notes.BulkTrash(list);
            Assert.Equal("List is empty", result);
        }

        [Fact]
        public async void BulkTrashRepository()
        {
            var notesData = new Mock<INotesAccountManagerRepository>();
            var notes = new NotesAccountManagerService(notesData.Object);
            IList<int> list = new List<int>();
            list.Add(2);
            list.Add(3);
            object result = await notes.BulkTrash(list);
            Assert.Equal("notes trashed successfully", result);
        }

        
       
    }
}
