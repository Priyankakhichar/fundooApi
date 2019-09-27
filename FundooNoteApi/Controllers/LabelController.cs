using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundooNoteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : ControllerBase
    {
        private ILabelBusinessManager labelBusinessManager;
        public LabelController(ILabelBusinessManager manager)
        {
            this.labelBusinessManager = manager;
        }

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

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateLabel(LabelModel model, int id)
        {
            var result = await this.labelBusinessManager.UpdateLabel(model, id);
            if(result != null)
            {
                return Ok(new { result });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("getLabel{userId}")]
       public List<LabelModel> GetLabel(string userId)
        {
            var result = this.labelBusinessManager.GetLabel(userId);
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