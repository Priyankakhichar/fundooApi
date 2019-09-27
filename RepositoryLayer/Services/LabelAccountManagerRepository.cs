using CommonLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
   public class LabelAccountManagerRepository : ILabelAccountManager
    {
        private AuthenticationContext context;
        public LabelAccountManagerRepository(AuthenticationContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddLabel(LabelModel model)
        {
            var labelModel = new LabelModel()
            {
                UserId = model.UserId,
                LableName = model.LableName,
                CreatedDate = model.CreatedDate,
                ModifiedDate = model.ModifiedDate
            };

            this.context.Add(labelModel);
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

        public async Task<string> DeleteLabel(int id)
        {
            var delete = this.context.LabelModels.Where (d => d.Id == id).FirstOrDefault();
            this.context.Remove(delete);
            var result = await this.context.SaveChangesAsync();
            if(result != 0)
            {
                return "Label deleted successfully";
            }
            else
            {
                return "no label found for the id";
            }
        }

        public List<LabelModel> GetLabel(string userId)
        {
            List<LabelModel> list = new List<LabelModel>();
            var result = from label in context.LabelModels.Where(s => s.UserId == userId) select label;
            foreach (var label in result)
            {
                var labelModel = new LabelModel()
                {
                    UserId = label.UserId,
                    LableName = label.LableName,
                    CreatedDate = label.CreatedDate,
                    ModifiedDate = label.ModifiedDate
                };

                list.Add(labelModel);
            }

            return list;
        }

        public async Task<string> UpdateLabel(LabelModel model ,int id)
        {
            var result = from label in context.LabelModels.Where(u => u.Id == id) select label;
            foreach(var label in result)
            {
                label.UserId = model.UserId;
                label.LableName = model.LableName;
                label.CreatedDate = model.CreatedDate;
                label.ModifiedDate = model.ModifiedDate;
            }

            var labelResult = await context.SaveChangesAsync();
            if(labelResult != 0)
            {
                return "updated successfully";
            }
            else
            {
                return "id is not availabel";
            }
        }
    }
}
