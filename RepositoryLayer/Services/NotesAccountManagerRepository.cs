////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "NotesAccountManagerRepository.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Services
{
    using CommonLayer.Models;
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
                          select notes);

            foreach(var notesModels in notesData)
            {
                notesModels.Title = model.Title;
                notesModels.Description = model.Description;
                notesModels.UserId = model.UserId;
                notesModels.Color = model.Color;
                notesModels.NoteType = model.NoteType;
                notesModels.CreateDate = model.CreateDate;
                notesModels.ModifiedDate = model.ModifiedDate;
            }

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
       
        public async Task<string> AddImage(string image)
        {
            var notesModel = new NotesModel()
        }
    }
}
