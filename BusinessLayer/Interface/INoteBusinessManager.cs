////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "INoteBusinessManager.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Interface
{
    using CommonLayer.Enum;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
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
        Task<string> AddNotes(NotesModel model, string token);

        /// <summary>
        /// method to delete notes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> DeleteNotes(int id, string token);

        /// <summary>
        /// method to update notes
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> UpdatesNotes(NotesModel model, int id, string token);

        /// <summary>
        /// method to get notes
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        (IList<NotesModel>, IList<ApplicationUser>) GetNotes(string userId, EnumNoteType noteType, string token);

        /// <summary>
        /// upload image
        /// </summary>
        /// <param name="file"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        string AddImage(IFormFile file, int noteId, string userId);

        /// <summary>
        /// add reminder method
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        string AddReminder(int noteId, DateTime time);

        /// <summary>
        /// remove reminder method
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        string DeleteReminder(int noteId);

        /// <summary>
        /// sending push notififcation
        /// </summary>
        /// <returns></returns>
        Task<IList<NotesModel>> SendPushNotification();

        /// <summary>
        /// adding collabration
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
        /// search method to search notes by titile that contains search string
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
