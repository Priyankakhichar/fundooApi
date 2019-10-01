////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "LableTest.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace XUnitTestCases.LabelTestCases
{
    using BusinessLayer.Service;
    using CommonLayer.Models;
    using Moq;
    using RepositoryLayer.Interface;
    using System;
    using Xunit;

    public class LableTest
    {
        public class NotesTest
        {
            /// <summary>
            /// create label unit test
            /// </summary>
            [Fact]
            public void CreateLabelTest()
            {
                var labelData = new Mock<ILabelAccountManager>();
                var labels = new LabelAccountManagerService(labelData.Object);
                var labelModel = new LabelModel()
                {
                    LableName = "Title",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    UserId = "UserId"
                };

                var result = labels.AddLabel(labelModel);
                Assert.NotNull(result);
            }

            /// <summary>
            /// get label unit test
            /// </summary>
            [Fact]
            public void GetLabel()
            {
                var labelData = new Mock<ILabelAccountManager>();
                var labels = new LabelAccountManagerService(labelData.Object);
                var labelModel = new LabelModel()
                {
                    UserId = "UserId"
                };

                var result = labels.GetLabel(labelModel.UserId);
                Assert.NotNull(result);
            }

            /// <summary>
            /// delete label unit test
            /// </summary>
            [Fact]
            public void DeleteNotes()
            {
                var labelData = new Mock<ILabelAccountManager>();
                var labels = new LabelAccountManagerService(labelData.Object);
                var labelModel = new LabelModel()
                {
                    Id = 2
                };

                var result = labels.GetLabel(labelModel.UserId);
                Assert.NotNull(result);
            }

            /// <summary>
            /// update lable unit test
            /// </summary>
            [Fact]
            public void UpdateNotes()
            {
                var labelData = new Mock<ILabelAccountManager>();
                var labels = new LabelAccountManagerService(labelData.Object);
                var labelModel = new LabelModel()
                {
                    LableName = "Title",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    UserId = "UserId",
                    Id = 2
                };

                var result = labels.UpdateLabel(labelModel, labelModel.Id);
                Assert.NotNull(result);
            }

        }
    }
}
