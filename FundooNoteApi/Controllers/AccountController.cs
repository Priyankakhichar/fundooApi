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
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountManager _accountmanager;

        public AccountController(IAccountManager accountmanager)
        {
            this._accountmanager = accountmanager;
        }

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

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            var result = await this._accountmanager.Login(login);
            if(result!= null)
            {
              
                //var lastlogin = result.Substring(result.LastIndexOf('+') + 1);
                return this.Ok(new { result });
            }
            else
            {
                return this.Unauthorized();
            }
        }

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

        [HttpPost]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            var result = await this._accountmanager.ResetPassword(model);
            if (result != null)
            {
                return this.Ok(new { result });
            }
            else
            {
                throw new Exception("");
            }
        }
    }
}