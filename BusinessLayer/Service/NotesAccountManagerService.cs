////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "NotesAccountManagerService.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Service
{
    using BusinessLayer.Interface;
    using CommonLayer;
    using CommonLayer.Enum;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Interface;
    using ServiceStack.Redis;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// notes account manager service
    /// </summary>
    public class NotesAccountManagerService : INoteBusinessManager
    {
        /// <summary>
        /// repository interface instance variable
        /// </summary>
        private INotesAccountManagerRepository accountRepository;

        /// <summary>
        /// constructor to initialize the repository instance varible
        /// </summary>
        /// <param name="accountRepository"></param>
        public NotesAccountManagerService(INotesAccountManagerRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        /// <summary>
        /// adding the notes to the data base
        /// </summary>
        /// <param name="model"></param>
        /// <returns>returning true if notes added to the database</returns>
        public async Task<bool> AddNotes(NotesModel model)
        {
            try
            {
                if (!model.Equals(null))
                {
                    return await this.accountRepository.AddNotes(model);
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
        /// delete notes by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns the number of rows deleted</returns>
        public async Task<int> DeleteNotes(int id)
        {
            try
            {
                return await this.accountRepository.DeleteNotes(id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// update notes method to update the notes
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>returns true if record updated successfully</returns>
        public async Task<bool> UpdatesNotes(NotesModel model, int id)
        {
            try
            {
                var result = await this.accountRepository.UpdatesNotes(model, id);

                ////key to store value in redis
                var cacheKey = "data" + model.UserId;
                using (var redis = new RedisClient())
                {

                    ////removing the cache from redis
                    redis.Remove(cacheKey);

                    ////condtion to check if there are record or not in redis
                    if (redis.Get(cacheKey) == null)
                    {
                        if (result == true)
                        {
                            ////sets the data to the redis
                            redis.Set(cacheKey, result);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get notes method to get notes according to the user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>returns the list of notes</returns>
        public (IList<NotesModel>, IList<ApplicationUser>) GetNotes(string userId, EnumNoteType noteType)
        {

            try
            {
                ////getting the result from database
                return this.accountRepository.GetNotes(userId, noteType);   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// uploading image to notes
        /// </summary>
        /// <param name="file"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public string AddImage(IFormFile file, int noteId)
        {
            try
            {
                ////object of custom class Image Cloudinary
                ImageCloudinary cloudinary = new ImageCloudinary();

                ////url from cloudinary
                var url = cloudinary.UploadImageAtCloudinary(file);


                ////added the reference to the repository class
                return this.accountRepository.AddImage(url, noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// adding the reminder
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public string AddReminder(int noteId, DateTime time)
        {
            try
            {
                ////added the reference to the repository class
                return this.accountRepository.AddReminder(noteId, time);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// removing the reminder
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public string DeleteReminder(int noteId)
        {
            try
            {
                ////added the reference to the repository class
                return this.accountRepository.DeleteReminder(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// push notification
        /// </summary>
        /// <returns>returning the list of notes that have reminder between current time and after 10 min</returns>
        public  async Task<IList<NotesModel>> SendPushNotification()
        {
            try
            {
                return await this.accountRepository.SendPushNotification();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// adding the collabration
        /// </summary>
        /// <param name="collaboration"></param>
        /// <returns>returns the success and failure message</returns>
        public async Task<string> AddCollabration(NotesCollaboration collaboration)
        {
            try
            {
                ////if collaboration attributes is not null it will go to the repository
                if (collaboration != null)
                {
                    return await this.accountRepository.AddCollabration(collaboration);
                }
                else
                {
                    return "empty fields";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// remove collaboration
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return success or failure message</returns>
        public async Task<string> RemoveCollabration(int id)
        {
            try
            {
                if (id > 0)
                {
                    ////if id is greater than 0 it will go to the repository layer
                    return await this.accountRepository.RemoveCollabration(id);
                }
                else
                {
                    return "invalid id";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// search operation by title
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>returning the list of notes that title contains the search string</returns>
       public IList<NotesModel> Search(string searchString)
       {
            try
            {
                ////if searchString is not empty it will go to the repository
                if (!string.IsNullOrEmpty(searchString))
                {
                    return this.accountRepository.Search(searchString);
                }
                else
                {
                    ////if search string is empty it will throw the exception
                    throw new Exception("Empty string");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
       }

        /// <summary>
        /// Bulk trash to delete the notes
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public async Task<string> BulkTrash(IList<int> noteId)
        {
            ////if list is not empty
            if (noteId.Count > 0)
            {
                try
                {
                   var result = await this.accountRepository.BulkTrash(noteId);
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                return "List is empty";
            }   
        }
    }
}
