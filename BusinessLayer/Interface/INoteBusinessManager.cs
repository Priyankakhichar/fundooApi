////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "INoteBusinessManager.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Interface
{
    using CommonLayer.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Notes business Manager interface
    /// </summary>
    public interface INoteBusinessManager
    {
        /// <summary>
        /// method to add notes
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> AddNotes(NotesModel model);

        /// <summary>
        /// method to delete notes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteNotes(int id);

        /// <summary>
        /// method to update notes
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> UpdateNotes(NotesModel model, int id);

        /// <summary>
        /// method to get notes
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<NotesModel> GetNotes(string userId, EnumNoteType noteType);
        Task<string> AddImage(string image);
    }
}
