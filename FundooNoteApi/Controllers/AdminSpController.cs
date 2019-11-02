using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNoteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminSpController : ControllerBase
    {
        private readonly IAdminSpBusiness _business;
        public AdminSpController(IAdminSpBusiness business)
        {
            this._business = business;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult RegisterUser(UserRegistration model)
        {
            var result = this._business.RegisterUser(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel model)
        {
            var result = this._business.Login(model);
            return Ok(result);
        }

        [HttpGet]
        [Route("userStatistics")]
        public IActionResult GetUserStatistics(string token)
        {
            var result = this._business.GetUserStatistics(token);
            return Ok(result);
        }

        [HttpGet]
        [Route("update")]
        public IActionResult UpdateUser(int userId, string tokenString, string role)
        {
            var result = this._business.UpdateUser(userId, tokenString, role);
            return Ok(result);
        }
    }
}