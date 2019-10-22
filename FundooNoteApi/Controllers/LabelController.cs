////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "LabelController.cs" company ="Bridgelabz">
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
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// label controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : ControllerBase
    {
        /// <summary>
        /// variable of business interface
        /// </summary>
        private ILabelBusinessManager labelBusinessManager;

        /// <summary>
        /// constructor to initialize the interface instance variable
        /// </summary>
        /// <param name="manager"></param>
        public LabelController(ILabelBusinessManager manager)
        {
            this.labelBusinessManager = manager;
        }

        /// <summary>
        /// add label method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addLabel")]
        public async Task<IActionResult> AddLabel(LabelModel model)
        {
            var result = await this.labelBusinessManager.AddLabel(model);
            if (result == true)
            {
                return Ok(new { result });
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// delete label method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteLabel(int id)
        {
          var result = await this.labelBusinessManager.DeleteLabel(id);
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
        /// upadte label
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateLabel(LabelModel model, int id)
        {
            var result = await this.labelBusinessManager.UpdateLabel(model, id);
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
        /// get label
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getLabel{userId}")]
       public IList<LabelModel> GetLabel(string userId)
        {
            var result = this.labelBusinessManager.GetLabel(userId);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// adding label to notes
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addLabelToNote")]
        public async Task<IActionResult> AddLabelToNote(int labelId, int noteId)
        {
            var result = await this.labelBusinessManager.AddLabelToNote(labelId, noteId);
            return Ok(new { result });
        }
    }
}