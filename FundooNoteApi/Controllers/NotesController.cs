using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNoteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private INoteBusinessManager noteManager;

        public NotesController(INoteBusinessManager noteManager)
        {
            this.noteManager = noteManager;
        }

        [HttpPost]
        [Route("addNotes")]
        public async Task<bool> AddNotes(NotesModel model)
        {
            var result = await this.noteManager.AddNotes(model);
            return result;
        }

        [HttpPut]
        [Route("updateNotes/{id}")]
        public async Task<bool> UpdateNotes(NotesModel model, int id)
        {
            var result = await this.noteManager.UpdateNotes(model, id);
            return result;
        }

        [HttpDelete]
        [Route("deleteNotes/{id}")]
        public async Task<IActionResult> DeleteNotes(int id)
        {
            
            var result = await this.noteManager.DeleteNotes(id);
            if (result == 1)
            {
                return this.Ok(new { result });
            }
            else
            {
                return this.BadRequest();
            }
               
        }
        
        [HttpGet]
        [Route("getNotes/{userId}")]
        public List<NotesModel> GetNotes(string userId)
        {
            var result = this.noteManager.GetNotes(userId);
            if(result != null)
            {
                return result;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}