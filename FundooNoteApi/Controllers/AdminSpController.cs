////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "AdminSpController.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace FundooNoteApi.Controllers
{
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using CommonLayer.Struct;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// admin controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminSpController : ControllerBase
    {
        private readonly IAdminSpBusiness _business;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="business"></param>
        public AdminSpController(IAdminSpBusiness business)
        {
            this._business = business;
        }


        /// <summary>
        /// rigister user method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public IActionResult RegisterUser(UserRegistration model)
        {
            var result = this._business.RegisterUser(model);
            return Ok(result);
        }

        /// <summary>
        /// login method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel model)
        {
            var result = this._business.Login(model);
            return Ok(result);
        }

        /// <summary>
        /// user statistics method to get users information
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("userStatistics")]
        public IActionResult GetUserStatistics(string token)
        {
            var result = this._business.GetUserStatistics(token);
            return Ok(result);
        }

        /// <summary>
        /// update user method to update user role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tokenString"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        public IActionResult UpdateUser(int userId, string tokenString, string role, bool isSuspended)
        {
            var result = this._business.UpdateUser(userId, tokenString, role, isSuspended);
            return Ok(result);
        }

        [HttpGet]
        [Route("getNotes")]
        public async Task<NoteTypes> GetTotalNotes(string userId)
        {
            var result = await this._business.GetTotalNotes(userId);
            return result;
        }
    }
}