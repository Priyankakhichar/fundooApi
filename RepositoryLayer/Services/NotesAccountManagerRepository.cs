////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "NotesAccountManagerRepository.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Services
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using CommonLayer;
    using CommonLayer.Enum;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Context;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class NotesAccountManagerRepository : INotesAccountManagerRepository
    {
        /// <summary>
        /// Authentication context instance variable
        /// </summary>
        private AuthenticationContext context;

        /// <summary>
        /// constructor to initialize the context variable 
        /// </summary>
        /// <param name="context"></param>
        public NotesAccountManagerRepository(AuthenticationContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// add notes method to add notes to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddNotes(NotesModel model)
        {
            try
            {
                var notesModel = new NotesModel()
                {
                    Title = model.Title,
                    Id = model.Id,
                    Description = model.Description,
                    UserId = model.UserId,
                    Color = model.Color,
                    NoteType = model.NoteType,
                    CreateDate = model.CreateDate,
                    ModifiedDate = model.ModifiedDate
                };

                ////adding the details to the the data base
                this.context.Add(notesModel);

                ////saving the changes to the database
                var result = await this.context.SaveChangesAsync();
                
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// delete notes accroding to the notes id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteNotes(int id)
        {
            ////linq for delete notes...it storing the information in delete variable for perticular id
            var delete = this.context.NotesModels.Where(d => d.Id == id).FirstOrDefault();

            ////removing the information from the database
            this.context.Remove(delete);

            ////saving the changes to the database
            var result = await this.context.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// get notes from the data base according to the data base
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<NotesModel> GetNotes(string userId, EnumNoteType noteType)
        {
            ///list of notes model type
            List<NotesModel> list = new List<NotesModel>();

            var result = (from notes in context.NotesModels where notes.UserId == userId && notes.NoteType.Equals(noteType) select notes);
            ////iterating the loop till there are records
            foreach (var res in result)
            {
                var notesModel = new NotesModel()
                {
                    Title = res.Title,
                    Description = res.Description,
                    UserId = res.UserId,
                    Color = res.Color,
                    NoteType = res.NoteType,
                    CreateDate = res.CreateDate,
                    ModifiedDate = res.ModifiedDate
                };

                ////adding the records to the list
                list.Add(notesModel);
            }

            return list;
        }

        /// <summary>
        /// update notes method to update notes
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> UpdatesNotes(NotesModel model, int id)
        {
            var notesData = (from notes in context.NotesModels
                          where notes.Id == id
                          select notes).FirstOrDefault();
            
                notesData.Title = model.Title;
                notesData.Description = model.Description;
                notesData.UserId = model.UserId;
                notesData.Color = model.Color;
                notesData.NoteType = model.NoteType;
                notesData.IsPin = model.IsPin;
                notesData.Image = model.Image;
                notesData.CreateDate = model.CreateDate;
                notesData.ModifiedDate = DateTime.Now;
            

            ////save changes to the database
            var result = await this.context.SaveChangesAsync();
            if(result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// uploading the image
        /// </summary>
        /// <param name="file">file</param>
        /// <param name="noteId">noteId</param>
        /// <returns></returns>
       public string AddImage(IFormFile file, int noteId)
        {
            ////object of custom class Image Cloudinary
            ImageCloudinary cloudinary = new ImageCloudinary();

            ////url from cloudinary
            var url = cloudinary.UploadImageAtCloudinary(file);

            ////getting the row according to the note id
            var updatableRow = context.NotesModels.Where(u => u.Id == noteId).FirstOrDefault();

            ////placing the url
            updatableRow.Image = url;
            var result = this.context.SaveChanges();
            if(result != 0)
            {
                return "image successfully added";
            }
            else
            {
                return "image upload failed";
            }
        }

        ///// <summary>
        ///// Is pin method to returns list of pinned notes
        ///// </summary>
        ///// <param name="noteId"></param>
        ///// <param name="isPin"></param>
        ///// <returns>returns the list of notes which is pinned</returns>
        //public IEnumerable<NotesModel> IsPin(int noteId, bool isPin)
        //{
        //    ////getting the rows according to the note id
        //    var updatableRow = this.context.NotesModels.Where(g => g.Id == noteId).FirstOrDefault();
        //    updatableRow.IsPin = isPin;

        //    ////saving the changes to the database
        //    this.context.SaveChanges();

        //    ////returning the list of notes
        //    IEnumerable<NotesModel> result = this.context.NotesModels.Where(g => g.IsPin == true);
        //    return result.ToList();
        //}

        /// <summary>
        /// adding the reminder
        /// </summary>
        /// <param name="noteId">note id</param>
        /// <param name="time">time</param>
        /// <returns>returns success and failure message</returns>
        public string AddReminder(int noteId, DateTime time)
        {
            ////getting the row according to the note id
            var updatableRow = this.context.NotesModels.Where(g => g.Id == noteId).FirstOrDefault();
            
            ////setting the reminder time
            updatableRow.Reminder = time;

            ////saving the changes to the database
            var result = this.context.SaveChanges();
            if(result != 0)
            {
                return "reminder added successfully";
            }
            else
            {
                return "reminder not added";
            }
        }

        /// <summary>
        /// removing the reminder
        /// </summary>
        /// <param name="noteId">note id</param>
        /// <returns>returns success and failure message</returns>
        public string DeleteReminder(int noteId)
        {
            ////getting the row according to the noteid
            var updatableRow = this.context.NotesModels.Where(u => u.Id == noteId).FirstOrDefault();

            ////setting the reminder value null
            updatableRow.Reminder = null;

            ////saving the changes to the database
            var result = this.context.SaveChanges();
            if(result != 0)
            {
                return "reminder removed";
            }
            else
            {
                return "somthing went wrong";
            }
        }
    }
}
