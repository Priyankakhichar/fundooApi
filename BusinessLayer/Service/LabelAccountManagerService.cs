using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class LabelAccountManagerService : ILabelBusinessManager
    {
        private ILabelAccountManager accountRepository;
        public LabelAccountManagerService(ILabelAccountManager accountManager)
        {
            this.accountRepository = accountManager;
        }
        public async Task<bool> AddLabel(LabelModel model)
        {
            try
            {
                if(!model.Equals(null))
                {
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

        public async Task<string> DeleteLabel(int id)
        {
            try
            {
                if(!id.Equals(null))
                {
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

        public List<LabelModel> GetLabel(string userId)
        {
            try
            {
                if(!userId.Equals(null))
                {
                    var result =  this.accountRepository.GetLabel(userId);
                    return result;
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UpdateLabel(LabelModel model , int id)
        {
            try
            {
                if(!model.Equals(null))
                {
                    var result = await this.accountRepository.UpdateLabel(model, id);
                    return result;
                }
                else
                {
                    return "model is empty";
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
