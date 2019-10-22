////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "AccountController.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace FundooNoteApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Enum;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// notes controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// notes service reference variable
        /// </summary>
        private INoteBusinessManager noteManager;

        /// <summary>
        /// constructor to initialize the service variable 
        /// </summary>
        /// <param name="noteManager"></param>
        public NotesController(INoteBusinessManager noteManager)
        {
            this.noteManager = noteManager;
        }

        /// <summary>
        /// add notes method to add notes to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("addNotes")]
        public async Task<bool> AddNotes(NotesModel model)
        {
            var result = await this.noteManager.AddNotes(model);
            return result;
        }

        /// <summary>
        /// update notes method to update notes in database
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPut]
        [Route("updateNotes")]
        public async Task<bool> UpdatesNotes(NotesModel model, int id )
        {
            var result = await this.noteManager.UpdatesNotes(model, id);
            return result;
        }

        /// <summary>
        /// delete notes method to delete notes according to the id of notes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete]
        [Route("deleteNotes/{id}")]
        public async Task<IActionResult> DeleteNotes(int id)
        {

            var result = await this.noteManager.DeleteNotes(id);
            if (result != 0)
            {
                return this.Ok(new { result });
            }
            else
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// get notes method for retrieve the notes record from database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        
        [HttpGet]
        [Route("getNotes/{userId}")]
        [Authorize]
        public (IList<NotesModel>, IList<ApplicationUser>) GetNotes(string userId, EnumNoteType noteType)
        {
            var result = this.noteManager.GetNotes(userId, noteType);
            if (result != (null,null))
            {
                return result;
            }
            else
            {
                throw new Exception();
            }
        }


        /// <summary>
        /// uploading image at cloudinary and storing cloudinary link to the database
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="noteId"></param>
        /// <returns>returns the success or fail message</returns>
        
        [HttpPost]
        [Route("upoadImage")]
        public string UploadImage(IFormFile filePath, int noteId)
        {
            return this.noteManager.AddImage(filePath, noteId);    
        }

        /// <summary>
        /// add reminder method to add reminder to the notes
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        
        [HttpPut]
        [Route("reminder")]
        public string AddReminder(int noteId, DateTime time)
        {
            return this.noteManager.AddReminder(noteId, time);
        }

        /// <summary>
        /// remove reminder method to remove reminder from notes
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        
        [HttpDelete]
        [Route("removeReminder")]
        public string DeleteReminder(int noteId)
        {
            return this.noteManager.DeleteReminder(noteId);
        }

        /// <summary>
        /// sending push notification
        /// </summary>
        /// <returns></returns>
        
        [AllowAnonymous]
        [HttpGet]
        [Route("sendMessage")]
        public async Task<IActionResult> SendPushNotification()
        {
            var result = await this.noteManager.SendPushNotification();
            if (result != null)
            {
                return Ok(new { result });
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Adding collaboration
        /// </summary>
        /// <param name="collaboration"></param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("collaboration")]
        public async Task<IActionResult> AddCollabration(NotesCollaboration collaboration)
        {
            var result = await this.noteManager.AddCollabration(collaboration);
            return Ok(new { result });
        }
         
        /// <summary>
        /// removing collaboration
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpDelete]
        [Route("remove")]
        public async Task<IActionResult> RemoveCollabration(int id)
        {
            var result = await this.noteManager.RemoveCollabration(id);
            return Ok(new { result });
        }

        /// <summary>
        /// search operation by title or description
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        
        [HttpGet]
        [Route("search")]
        public IActionResult Search(string searchString)
        {
            var result = this.noteManager.Search(searchString);
            return Ok(new { result });
        }

        /// <summary>
        /// bulk trash method to delete many notes
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("bulkTrash")]
        public async Task<IActionResult> BulkTrash(IList<int> noteId)
        {
            var result = await this.noteManager.BulkTrash(noteId);
            return Ok(new { result });
        }
    }
}