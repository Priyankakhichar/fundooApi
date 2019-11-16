////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "AdminController.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace FundooNoteApi.Controllers
{
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// admin controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        /// <summary>
        /// admin bussiness variable
        /// </summary>
        private IAdminBussiness _admin;

        /// <summary>
        /// admin bussiness variable is initilized through constructor
        /// </summary>
        /// <param name="admin"></param>
        public AdminController(IAdminBussiness admin)
        {
            this._admin = admin;
        }

        /// <summary>
        /// register the  admin user
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(UserRegistration registration)
        {
            var result = await this._admin.RegisterUser(registration);
            return Ok(new { result });
        }

        /// <summary>
        /// admin login api
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("adminLogin")]
        public async Task<IActionResult> AdminLogin(LoginModel model)
        {
            var result = await this._admin.AdminLogin(model);
            return Ok(new { result });
        }

        /// <summary>
        /// add service api
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("addService")]
        public async Task<IActionResult> AddService(ServiceModel model)
        {
            var result = await this._admin.AddService(model);
            return Ok(new { result });
        }

        /// <summary>
        /// get user list api for admin
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        
        [HttpGet]
        [Route("UserStatistics")]
        public async Task<UserStatisticsModel> GetUserNotesCount(string token)
        {
            var result = await this._admin.GetUserNotesCount(token);
            return result;
        }

        [HttpGet]
        [Route("UserList/{token}")]
        public async Task<IActionResult> GetUserList(string token)
        {
            var userList = await this._admin.GetUserList(token);        
            return Ok(new { userList });
        }
      
    }
}
