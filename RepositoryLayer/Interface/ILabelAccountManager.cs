////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "ILabelAccountManager.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Interface
{
    using CommonLayer.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// label repository interface
    /// </summary>
    public interface ILabelAccountManager
    {
        /// <summary>
        /// add label method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> AddLabel(LabelModel model);

        /// <summary>
        /// delete label method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> DeleteLabel(int id);

        /// <summary>
        /// update label method
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> UpdateLabel(LabelModel model, int id);

        /// <summary>
        /// get label method
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<LabelModel> GetLabel(string userId);

        /// <summary>
        /// addding label to notes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> AddLabelToNote(int labelId, int noteId, string userId);
    }
}
