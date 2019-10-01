////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "LabelAccountManagerService.cs" company ="Bridgelabz">
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
    /// Label account manager service class
    /// </summary>
    public class LabelAccountManagerService : ILabelBusinessManager
    {
        /// <summary>
        /// variable of repository class
        /// </summary>
        private ILabelAccountManager accountRepository;

        /// <summary>
        /// constructor to initilize the repository variable
        /// </summary>
        /// <param name="accountManager"></param>
        public LabelAccountManagerService(ILabelAccountManager accountManager)
        {
            this.accountRepository = accountManager;
        }

        /// <summary>
        /// method to add label to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>if records added successfully it returns true else returns false</returns>
        public async Task<bool> AddLabel(LabelModel model)
        {
            try
            {
                ////if model is null it will throw exception
                if(!model.Equals(null))
                {
                    ////getting the result from repository class
                    var result = await this.accountRepository.AddLabel(model);
                    return result;
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
        /// delete label method to delete label according to the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>if label deleted successfully it returns string value in result</returns>
        public async Task<string> DeleteLabel(int id)
        {
            try
            {
                if(!id.Equals(null))
                {
                    ////getting result from repository class
                    var result = await this.accountRepository.DeleteLabel(id);
                    return result;
                }
                else
                {
                    return "label id is empty";
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get label method for getting the label from database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>returning the list of labels</returns>
        public IList<LabelModel> GetLabel(string userId)
        {
            try
            {
                var redisResult = new List<LabelModel>();

                ////declared a key to set data to the redis
                var cacheKey = "data" + userId;
                using (var redis = new RedisClient())
                {
                    ////removing the cache from redis
                    redis.Remove("data" + userId);

                    ////condtion to check if there are record or not in redis
                    if (redis.Get(cacheKey) == null)
                    {
                        ////getting the result from database
                        var result = this.accountRepository.GetLabel(userId);
                        if (result != null)
                        {
                            ////sets the data to the redis
                            redis.Set(cacheKey, result);

                            ////getting the list from redis
                            redisResult = redis.Get<List<LabelModel>>(cacheKey);
                        }
                    }
                    else
                    {
                        redisResult = redis.Get<List<LabelModel>>(cacheKey);
                    }

                    return redisResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// update label method to update the label details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>returns string value for success if label updated successfully.</returns>
        public async Task<string> UpdateLabel(LabelModel model , int id)
        {
            try
            {
                var result = await this.accountRepository.UpdateLabel(model, id);
                var cacheKey = "data" + model.UserId;
                using (var redis = new RedisClient())
                {

                    ////removing the cache from redis
                    redis.Remove(cacheKey);

                    ////condtion to check if there are record or not in redis
                    if (redis.Get(cacheKey) == null)
                    {
                        if (result != null)
                        {
                            ////sets the data to the redis
                            redis.Set(cacheKey, result);
                        }
                    }
                }

                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
