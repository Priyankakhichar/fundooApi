////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "NotesAccountManagerRepository.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Services
{
    using CommonLayer.Enum;
    using CommonLayer.Models;
    using Newtonsoft.Json;
    using RepositoryLayer.Context;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
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
                    ModifiedDate = model.ModifiedDate,
                    Image = model.Image
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
        /// get notes from the database according to the user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public (IList<NotesModel>, IList<ApplicationUser>) GetNotes(string userId, EnumNoteType noteType)
        {
            ////list of notes model type
            IList<NotesModel> list = new List<NotesModel>();

            ////list of application user type
            IList<ApplicationUser> userList = new List<ApplicationUser>();

            ////getting all notes according to the noteType
            var results = (from notes in context.NotesModels where notes.UserId == userId && notes.NoteType.Equals(noteType) select notes);

            ////iterating the loop till there are records
            foreach (var result in results)
            {
                ////adding to the list
                list.Add(result);
            }

            ////getting noteId from Collaboration table by userId
            var noteById = from notes in context.NotesModels
                          join Collaboration in context.Collaborations on notes.Id equals Collaboration.NoteId
                          where Collaboration.UserId == userId
                          select new NotesCollaboration
                          {
                              NoteId = Collaboration.NoteId,
                          };

            ////to get result by every noteId
            foreach (var noteid in noteById)
            {
                var notes = (from note in context.NotesModels where note.Id == noteid.NoteId select note).FirstOrDefault();
                list.Add(notes);
            }

            ////iterating the all notes to get user id for each note
            foreach (var user in results)
            {
                ////getting the user id according to the note id
                var result1 = from notes in context.NotesModels
                              join Collaboration in context.Collaborations on notes.Id equals Collaboration.NoteId
                              where Collaboration.NoteId == user.Id
                              select new NotesCollaboration
                              {
                                  UserId = Collaboration.UserId,
                              };

                ////iterating the result1 variable to get user details from Application user
                foreach (var res in result1)
                {
                    var result = from userModel in context.ApplicationUser where userModel.Id == res.UserId select userModel;
                    foreach (var res1 in result)
                    {
                        ////adding to the list
                        userList.Add(res1);
                    }
                }
            }

            return (list, userList);
        }

        /// <summary>
        /// update notes method to update notes
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> UpdatesNotes(NotesModel model, int id)
        {
            ////getting the records by id
            var notesData = (from notes in context.NotesModels
                             where notes.Id == id
                             select notes).FirstOrDefault();

            ////if notes data have records then it will update the records
            if (notesData != null)
            {
                notesData.Title = model.Title;
                notesData.Description = model.Description;
                notesData.UserId = model.UserId;
                notesData.Color = model.Color;
                notesData.NoteType = model.NoteType;
                notesData.IsPin = model.IsPin;
                notesData.Image = model.Image;
                notesData.CreateDate = model.CreateDate;
                notesData.ModifiedDate = DateTime.Now;
            }
            else
            {
                ////getting the notes from notes model by notes id comparing from collaboration table 
                var userNotes = from notes in context.NotesModels
                            join Collaboration in context.Collaborations on notes.Id equals Collaboration.NoteId
                            where Collaboration.NoteId == id
                            select notes;

                ////condition to check empty list
                if (userNotes.ToList() != null)
                {
                    ////iterating the loop
                    foreach (var user in userNotes)
                    {
                        user.Title = model.Title;
                        user.Description = model.Description;
                        user.UserId = model.UserId;
                        user.Color = model.Color;
                        user.NoteType = model.NoteType;
                        user.IsPin = model.IsPin;
                        user.Image = model.Image;
                        user.CreateDate = model.CreateDate;
                        user.ModifiedDate = DateTime.Now;
                    }
                }
            }

            ////save changes to the database
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

        /// <summary>
        /// uploading the image
        /// </summary>
        /// <param name="file">file</param>
        /// <param name="noteId">noteId</param>
        /// <returns></returns>
        public string AddImage(string url, int noteId)
        {

            ////getting the row according to the note id
            var user = context.NotesModels.Where(u => u.Id == noteId).FirstOrDefault();

            ////placing the url
            user.Image = url;
            var result = this.context.SaveChanges();
            if (result != 0)
            {
                return "image successfully added";
            }
            else
            {
                return "image upload failed";
            }
        }

        /// <summary>
        /// adding the reminder
        /// </summary>
        /// <param name="noteId">note id</param>
        /// <param name="time">time</param>
        /// <returns>returns success and failure message</returns>
        public string AddReminder(int noteId, DateTime time)
        {
            ////getting the row according to the note id
            var user = this.context.NotesModels.Where(g => g.Id == noteId).FirstOrDefault();

            ////setting the reminder time
            user.Reminder = time;

            ////saving the changes to the database
            var result = this.context.SaveChanges();
            if (result != 0)
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
            var user = this.context.NotesModels.Where(u => u.Id == noteId).FirstOrDefault();

            ////setting the reminder value null
            user.Reminder = null;

            ////saving the changes to the database
            var result = this.context.SaveChanges();
            if (result != 0)
            {
                return "reminder removed";
            }
            else
            {
                return "something went wrong";
            }
        }

        /// <summary>
        /// sending push notification
        /// </summary>
        /// <returns></returns>
        public async Task<IList<NotesModel>> SendPushNotification()
        {
            var data = @"
            'headers':
                    {
                        'Authrozation': 'key=AAAAzTkiUQk:APA91bEoJuNgLH-6k-ozHKT_AUfagNVfsdNHmcUeWd4Hhva0vA0pHBGxZq1kDl01Wn15PM7YXk6g0PbWRzBCTnZIY1wy79xpNYcTH8-y0gx5XrCKsh97LK7G5ke_hpqfIANUJ_JnFcG7'
                    },

            'notification':
                    {
                      'message' : 'my first message',
                      'name' : 'priyanka'   
                    },

             'To':'fYDlq1Xg1FM:APA91bGC_pXsuVn3yku9mtlLaHWgn32RNGPBDWAo4rGL7z1uevJ-cY-BpsDH--2zdWVymaBzvovi4H3dfilPHGw08RxrJDsPnmlXKQo9CIGI4DgPOmTiFZnmj7lJ7ud0M9H762YthB-X'
                 ";

            var client = new HttpClient();

          

            var currentTime = DateTime.Now;
            var x5MinsLater = currentTime.AddMinutes(30);

            ////getting the notes by reminder
            var result = from notes in this.context.NotesModels.Where(g =>g.Reminder >= currentTime && g.Reminder <= x5MinsLater) select notes;


            ////sending the get request to the firebase api
            var api =  await client.GetAsync("https://fcm.googleapis.com/fcm/send");

            //////converting data to the string formate
            //var json = JsonConvert.SerializeObject(data);
            //var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
 

            //var messageResponse = await client.PostAsync("https://fcm.googleapis.com/fcm/send", httpContent);
            ////returning the list of notes
            return result.ToList();
        }

        /// <summary>
        /// adding collabration
        /// </summary>
        /// <param name="collaboration"></param>
        /// <returns>returning the success or failure message</returns>
        public async Task<string> AddCollabration(NotesCollaboration collaboration)
        {
            NotesCollaboration collaborations = new NotesCollaboration()
            {
                NoteId = collaboration.NoteId,
                UserId = collaboration.UserId,
                CreatedBy = collaboration.CreatedBy
            };

            ////adding the record to the database
            this.context.Add(collaborations);

            ////saving the changes
            var result = await this.context.SaveChangesAsync();
            if(result > 0)
            {
                return "notes collaborated successfully";
            }
            else
            {
                return "something went wrong";
            }
        }

        /// <summary>
        /// removing the collabration
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returning the success or failure message</returns>
        public async Task<string> RemoveCollabration(int id)
        {
            ////getting the record by id
            var user =  this.context.Collaborations.Where(d => d.Id == id).FirstOrDefault();

            ////removing the record from database
            this.context.Remove(user);

            ////saving the changes
            var result = await this.context.SaveChangesAsync();
            if(result  > 0)
            {
                return "Collaboration removed successfully";
            }
            else
            {
                return "somthing went wrong";
            }
        }

        /// <summary>
        /// search operation
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>returning the list of notes that have contains search string</returns>
        public IList<NotesModel> Search(string searchString)
        {
            ////getting the record according to the title and description that contains search string
            var result = from notes in this.context.NotesModels.Where(s => s.Title.Contains(searchString) || s.Description.Contains(searchString)) select notes;
            return result.ToList();
        }

        public async Task<string> BulkTrash(IList<int> noteId)
        {
            ////iterating the noteId from list
            foreach (var noteid in noteId)
            {
                ////getting notes by id
                var note =  this.context.NotesModels.Where(g => g.Id == noteid).FirstOrDefault();
                this.context.Remove(note);
            }

            ////saving the changes to the database
            var result = await this.context.SaveChangesAsync();
            if(result > 0)
            {
                return "Notes trashed successfully";
            }
            else
            {
                return "Something went wrong";
            }

        }
    }
}
