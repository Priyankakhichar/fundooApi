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
        (IList<NotesModel>, IList<ApplicationUser>) GetNotes(string userId, EnumNoteType noteType);

        /// <summary>
        /// upload image
        /// </summary>
        /// <param name="file"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        string AddImage(IFormFile file, int noteId);

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
        IList<NotesModel> SendPushNotification();
        Task<string> AddCollabration(NotesCollaboration collaboration);
        Task<string> RemoveCollabration(int id);
        IList<NotesModel> Search(string searchString);
    }
}
