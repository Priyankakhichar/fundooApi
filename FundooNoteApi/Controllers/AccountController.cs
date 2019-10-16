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
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// account controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// service class reference variable
        /// </summary>
        private IAccountManager _accountmanager;

        /// <summary>
        /// constructor to initialize the service instance variable
        /// </summary>
        /// <param name="accountmanager"></param>
        public AccountController(IAccountManager accountmanager)
        {
            this._accountmanager = accountmanager;
        }

        /// <summary>
        /// registering the user
        /// </summary>
        /// <param name="registration"></param>
        /// <returns>returns result</returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> PostApplicationUser(UserRegistration registration)
        {
           bool result = await this._accountmanager.RegisterUser(registration);
           if(result )
            {
                return this.Ok(new { result });  
            }
            else
            {
                return this.BadRequest("mail not sent");
            }
        }

        /// <summary>
        /// login method verifiying the details stored in database
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            var result = await this._accountmanager.Login(login);
            if(result!= null)
            {
                ////subsrtring method to separate the token and login time
                var lastlogin = result.Substring(result.IndexOf('+') + 1);
                result = result.Substring(0, result.IndexOf('+'));

                ////returning the token and last login time
                return this.Ok(new { result , lastlogin});
            }
            else
            {
                return this.Unauthorized();
            }
        }

        /// <summary>
        /// forget password method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("forgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModel model)
        {
            var result = await this._accountmanager.ForgetPassword(model);
            if (result != null)
            {
                return this.Ok(new { result });
            }
            else
            {
                return this.Unauthorized();
            }
        }

        /// <summary>
        /// reset password method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model, string token)
        {
            var result = await this._accountmanager.ResetPassword(model, token);
            if (result != null)
            {
                return this.Ok(new { result });
            }
            else
            {
                throw new Exception("");
            }
        }

        /// <summary>
        /// upload image
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("uploadImage")]
        [Authorize]
        public string UploadImage(IFormFile filePath, string userId)
        {
            return this._accountmanager.UploadImage(filePath, userId);
        }

        /// <summary>
        /// social login
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("signin-facebook")]
        [Authorize]
        public async Task<IActionResult> SocialLogin(string email)
        {
            var result = await this._accountmanager.SocialLogin(email);
            return Ok(new { result });
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await this._accountmanager.Logout();
            return Ok(new { result });
        }
    }
}