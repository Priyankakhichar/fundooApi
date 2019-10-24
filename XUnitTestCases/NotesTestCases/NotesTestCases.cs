using Moq;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

using System.Linq;
using Microsoft.EntityFrameworkCore;
using CommonLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using BusinessLayer.Service;
using NUnit.Framework;
using BusinessLayer.Interface;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace XUnitTestCases.NotesTestCases
{
    [TestFixture]
    public class NotesTestCases
    {
        
        //[Fact]
        //public void GetNotes()
        //{
        //    var notes = this.repository.GetNotes("f949c013-dade-4cf2-8aaa-3a47ea80f739", 0);
        //    var count = notes.Item1.Count();
        //    Xunit.Assert.Equal(4, count);
        //}

        private INotesAccountManagerRepository MockObject()
        {
            var notesData = new Mock<INotesAccountManagerRepository>();
            notesData.Setup(m => m.DeleteNotes(2)).Returns(Task.FromResult(1));
            return notesData.Object;
        }

        [Test(Description = "passing Id 1. Expected result is 1.")]
        [Fact]
        public async void DeleteNotesBy_ID_1()
        {
            INotesAccountManagerRepository feed = this.MockObject();
            INoteBusinessManager noteBusiness = new NotesAccountManagerService(feed);
            var actualResult = await noteBusiness.DeleteNotes(2);
            var expectedResult = 2;
            NUnit.Framework.Assert.AreEqual(expectedResult, actualResult);
        }

        private INotesAccountManagerRepository MockObjectForImage()
        {
            var notesData = new Mock<INotesAccountManagerRepository>();
            notesData.Setup(m => m.AddImage("path/to/file", 1)).Returns("string");
            return notesData.Object;
        }
     

        [Test(Description = "passing url .Excepted result success string value.")]
        [Fact]
        public void AddImageTest()
        {
            IFormFile file = new Microsoft.AspNetCore.Http.Internal.FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var inputFile = fileMock.Object;

            INotesAccountManagerRepository repository = this.MockObjectForImage();
            INoteBusinessManager noteBusiness = new NotesAccountManagerService(repository);
           // var actualResult = noteBusiness.AddImage(inputFile, 2);
            NUnit.Framework.Assert.Throws<Exception>(() => noteBusiness.AddImage(inputFile, 2));
           // Is.TypeOf<Exception>()
               // .And.Message.EqualTo("object refrence not set to an instance of object"), 
            //var exceptedResult = "string";
            //NUnit.Framework.Assert.AreEqual(actualResult, exceptedResult);
        }

        private INotesAccountManagerRepository MockObjectForReminder()
        {
            var notesData = new Mock<INotesAccountManagerRepository>();
            notesData.Setup(m => m.AddReminder(2, DateTime.Now)).Returns("reminder added successfully");
            return notesData.Object;
        }


        [Test(Description = "passing Id 2 and datetime of now. Expected result is string .")]
        [Fact]
        public void AddReminderAt_ID_2()
        {
            INotesAccountManagerRepository repository = this.MockObject();
            INoteBusinessManager noteBusiness = new NotesAccountManagerService(repository);
            var actualResult = noteBusiness.AddReminder(2, DateTime.Now);
            var expectedResult = "reminder added successfully";
            NUnit.Framework.Assert.AreEqual(expectedResult, actualResult);
        }

        private INotesAccountManagerRepository MockObjectForBulkTrash()
        {
            var notesData = new Mock<INotesAccountManagerRepository>();
            IList<int> list = new List<int>();
            list.Add(2);
            list.Add(3);
            notesData.Setup(m => m.BulkTrash(list)).Returns(Task.FromResult("notes trashed successfully"));
            return notesData.Object;
        }
        [Fact]
        public void BulkTrashOfNotes()
        {
            INotesAccountManagerRepository repository = this.MockObject();
            INoteBusinessManager noteBusiness = new NotesAccountManagerService(repository);
           
            IList<int> list = new List<int>();
            list.Add(2);
            list.Add(3);
           
            var actualResult =  noteBusiness.BulkTrash(list);
            var expectedResult = "notes trashed successfully";
             NUnit.Framework.Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
