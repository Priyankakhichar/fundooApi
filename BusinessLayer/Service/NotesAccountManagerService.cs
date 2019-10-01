﻿////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "NotesAccountManagerService.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Service
{
    using BusinessLayer.Interface;
    using CommonLayer.Models;
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
            catch(Exception ex)
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
                 var result = await this.accountRepository.DeleteNotes(id);
                 return result;
        }

        /// <summary>
        /// update notes method to update the notes
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>returns true if record updated successfully</returns>
        public async Task<bool> UpdateNotes(NotesModel model, int id)
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
        public IList<NotesModel> GetNotes(string userId, EnumNoteType noteType)
        {
            try
            {
                    var redisResult = new List<NotesModel>();
                    ////declared a key to set data to the redis
                    var cacheKey = "data" + userId;
                    using (var redis = new RedisClient())
                    {
                    redis.Remove(cacheKey);
                       ////condtion to check if there are record or not in redis
                       if (redis.Get(cacheKey) == null)
                       {
                        ////getting the result from database
                        var result = this.accountRepository.GetNotes(userId, noteType);
                        if (result != null)
                        {
                            ////sets the data to the redis
                            redis.Set(cacheKey, result);

                            ////getting the list from redis
                            redisResult = redis.Get<List<NotesModel>>(cacheKey); 
                        }
                    }
                    else
                    {
                        redisResult = redis.Get<List<NotesModel>>(cacheKey);
                    }

                     return redisResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> AddImage(string image)
        {
            try
            {
                return this.accountRepository.AddImage(image);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
