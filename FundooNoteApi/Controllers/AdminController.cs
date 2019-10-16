using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FundooNoteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminBussiness _admin;
        public AdminController(IAdminBussiness admin)
        {
            this._admin = admin;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(UserRegistration registration)
        {
            var result = await this._admin.RegisterUser(registration);
            return Ok(new { result });
        }

        [HttpPost]
        [Route("adminLogin")]
        public async Task<IActionResult> AdminLogin(LoginModel model)
        {
            var result = await this._admin.AdminLogin(model);
            return Ok(new { result });
        }

        [HttpPost]
        [Route("addService")]
        public async Task<IActionResult> AddService(ServiceModel model)
        {
            var result = await this._admin.AddService(model);
            return Ok(new { result });
        }

        [HttpGet]
        [Route("getUserList")]
        public (IList<ApplicationUser>, IList<ApplicationUser>) GetUserList(string token)
        {
            var result = this._admin.GetUserList(token);
            return result;
        }
    }
}
