////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "INotesAccountManagerRepository.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Interface
{
    using CommonLayer.Enum;
    using CommonLayer.Models;
    using System;
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
        Task<bool> AddNotes(NotesModel model, string token);

        /// <summary>
        /// delete notes method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteNotes(int id);

        /// <summary>
        /// update noets method
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        Task<bool> UpdatesNotes(NotesModel model, int id);

        /// <summary>
        /// get notes method
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>returns list of notes</returns>
        (IList<NotesModel>, IList<ApplicationUser>) GetNotes(string userId, EnumNoteType noteType);

        /// <summary>
        /// uploading image to Cloudinary
        /// </summary>
        /// <param name="file">file</param>
        /// <param name="noteId">noteId</param>
        /// <returns>returns the success or failure message</returns>
        string AddImage(string url, int noteId, string userId);

        /// <summary>
        /// adding the reminder
        /// </summary>
        /// <param name="noteId">noteId</param>
        /// <param name="time">time</param>
        /// <returns>returns the success or failure message</returns>
        string AddReminder(int noteId, DateTime time);

        /// <summary>
        /// removing the reminder
        /// </summary>
        /// <param name="noteId">note-id</param>
        /// <returns>returns the success or failure message</returns>
        string DeleteReminder(int noteId);

        /// <summary>
        /// sending push notification
        /// </summary>
        /// <returns></returns>
        Task<IList<NotesModel>> SendPushNotification();

        /// <summary>
        /// adding collaborator
        /// </summary>
        /// <param name="collaboration"></param>
        /// <returns></returns>
        Task<string> AddCollabration(NotesCollaboration collaboration);

        /// <summary>
        /// removing collabration
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> RemoveCollabration(int id);

        /// <summary>
        /// searching notes by title
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        IList<NotesModel> Search(string searchString);

        /// <summary>
        /// bulk trash
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<string> BulkTrash(IList<int> noteId);
    }
}
