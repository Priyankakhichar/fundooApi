using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class AccountManagerService : INoteBusinessManager
    {
        private INotesAccountManagerRepository accountRepository;
        public AccountManagerService(INotesAccountManagerRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        public async Task<bool> AddNotes(NotesModel model)
        {
            try
            {
                if (!model.Equals(null))
                {
                    var result = await this.accountRepository.AddNotes(model);
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
        public async Task<int> DeleteNotes(int id)
        {
                 var result = await this.accountRepository.DeleteNotes(id);
                 return result;
        }
        public async Task<bool> UpdateNotes(NotesModel model, int id)
        {
            try
            {
                if (!model.Equals(null))
                {
                    var result = await this.accountRepository.UpdatesNotes(model, id);
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
        public List<NotesModel> GetNotes(string userId)
        {
            try
            {
                    var cacheKey = "data" + userId;
                    using (var redis = new RedisClient())
                    {
                        redis.Remove("data" + userId);
                       if (redis.Get(cacheKey) == null)
                       {
                        var result = this.accountRepository.GetNotes(userId);
                        if (result != null)
                        {
                            redis.Set(cacheKey, result);
                           
                        }
                        return result;
                    }
                    else
                       {
                        var result1 = redis.Get<List<NotesModel>>(cacheKey);
                        return result1;
                       }
                        
                    }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
