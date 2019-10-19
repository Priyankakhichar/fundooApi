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

namespace XUnitTestCases.NotesTestCases
{
    public class NotesTestCases
    {
        private INotesAccountManagerRepository repository;
        public NotesTestCases(INotesAccountManagerRepository repository)
        {
            this.repository = repository;
        }

        //[Fact]
        //public void GetAllNotes()
        //{
        //    //arrang
        //    var context = CreateDbContextMock();

        //    var service = new NotesAccountManagerRepository(context.Object);

        //    var result = service.GetNotes("f949c013-dade-4cf2-8aaa-3a47ea80f739", 0);

        //    var count = result.Item1.Count();

        //    // assert
        //    Assert.Equal(4, count);
        //}

        //private Mock<AuthenticationContext> CreateDbContextMock()
        //{
        //    var notes = GetFakeData().AsQueryable();

        //    var dbSet = new Mock<DbSet<NotesModel>>();
        //    dbSet.As<IQueryable<NotesModel>>().Setup(m => m.Provider).Returns(notes.Provider);
        //    dbSet.As<IQueryable<NotesModel>>().Setup(m => m.Expression).Returns(notes.Expression);
        //    dbSet.As<IQueryable<NotesModel>>().Setup(m => m.ElementType).Returns(notes.ElementType);
        //    dbSet.As<IQueryable<NotesModel>>().Setup(m => m.GetEnumerator()).Returns(notes.GetEnumerator());

        //    var context = new Mock<AuthenticationContext>();
        //    context.Setup(c => c.NotesModels).Returns(dbSet.Object);
        //    return context;
        //}

        //private IEnumerable<NotesModel> GetFakeData()
        //{
        //    var i = 1;
        //    var notes = A.ListOf<NotesModel>(4);
        //    notes.ForEach(x => x.Id = i++);
        //    return notes.Select(_ => _);
        //}


        [Fact]
        public void GetNotes()
        {
            var notes = this.repository.GetNotes("f949c013-dade-4cf2-8aaa-3a47ea80f739", 0);
            var count = notes.Item1.Count();
            Assert.Equal(4, count);
           
           
        }

       
    }
}
