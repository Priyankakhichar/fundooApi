////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "LabelAccountManagerRepository.cs" company ="Bridgelabz">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// label repository class
    /// </summary>
    public class LabelAccountManagerRepository : ILabelAccountManager
    {
        /// <summary>
        /// Authentication context variable
        /// </summary>
        private AuthenticationContext context;

        /// <summary>
        /// constructor to initilize the authentication context
        /// </summary>
        /// <param name="context"></param>
        public LabelAccountManagerRepository(AuthenticationContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// add label method 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>returns true if label successfully added to the database</returns>
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

            ////save chnages method to save the data to the database
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
        /// delete label method to delete label from databse according to the label id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteLabel(int id)
        {
            ////linq query
            var delete = this.context.LabelModels.Where (d => d.Id == id).FirstOrDefault();

            ////removing row from database
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

        /// <summary>
        /// get label method to get label from data base accrding to the user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>returns the list of labels</returns>
        public IList<LabelModel> GetLabel(string userId)
        {
            IList<LabelModel> list = new List<LabelModel>();
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
                ////adding the labels to the list
                list.Add(labelModel);
            }

            return list;
        }

        /// <summary>
        /// update label method to update the details of a label
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>returns the success message if update is successfully executed</returns>
        public async Task<string> UpdateLabel(LabelModel model ,int id)
        {
            var result = from label in context.LabelModels.Where(u => u.Id == id) select label;
            foreach (var label in result)
            {
                label.UserId = model.UserId;
                label.LableName = model.LableName;
                label.CreatedDate = model.CreatedDate;
                label.ModifiedDate = model.ModifiedDate;
            }

            var labelResult = await context.SaveChangesAsync();
            if (labelResult != 0)
            {
                return "updated successfully";
            }
            else
            {
                return "id is not availabel";
            }
        }

        public async Task<bool> AddLabelToNote(int labelId, int noteId, string userId)
        {
            ////verifying if the details already exist or not
            var notelabel = this.context.NotesLabel.Where(g => g.LabelId == labelId && g.NoteId == noteId && g.UserId == userId).FirstOrDefault();

            ////if notelabel does not have any record it will allow to add record in NotesLabel tabel
            if (notelabel == null)
            {
                NotesLabel notesLabel = new NotesLabel()
                {
                    NoteId = noteId,
                    LabelId = labelId,
                    UserId = userId
                };

                ////adding records to the NotesLabel tabel
                this.context.Add(notesLabel);

                ////saving the changes to the database
                var result = await this.context.SaveChangesAsync();

                ////if any changes made in database then result will have row count
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                ////if tabel already have same record it will return false. it avoids the duplicate records.
                return false;
            }
        }
    }
}
