////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "INotesAccountManagerRepository.cs" company ="Bridgelabz">
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
    /// notes repository interface
    /// </summary>
    public interface INotesAccountManagerRepository
    {
        /// <summary>
        /// add notes method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> AddNotes(NotesModel model);

        /// <summary>
        /// delete notes method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteNotes(int id);

        /// <summary>
        /// update noets method
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> UpdatesNotes(NotesModel model, int id);

        /// <summary>
        /// get notes method
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<NotesModel> GetNotes(string userId, EnumNoteType noteType);
        Task<string> AddImage(string image);
    }
}
