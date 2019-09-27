using CommonLayer.Models;
using Microsoft.AspNetCore.Identity;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NotesAccountManagerRepository : INotesAccountManagerRepository
    {
        private AuthenticationContext context;
        public NotesAccountManagerRepository(AuthenticationContext context)
        {
            this.context = context;
        }

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

        public List<NotesModel> GetNotes(string userId)
        {
            List<NotesModel> list = new List<NotesModel>();
            var result = (from notes in context.NotesModels where notes.UserId == userId select notes);
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
            }

            return list;
        }

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
    }
}
