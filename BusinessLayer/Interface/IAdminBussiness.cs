////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "IAdminBussiness.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Interface
{
    using CommonLayer.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// admin bussiness interface
    /// </summary>
    public interface IAdminBussiness
    {
        /// <summary>
        /// registering admin users
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        Task<string> RegisterUser(UserRegistration registration);

        /// <summary>
        /// admin login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> AdminLogin(LoginModel model);

        /// <summary>
        /// adding the service
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> AddService(ServiceModel model);

        /// <summary>
        /// getting user details
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<UserStatisticsModel> GetUserNotesCount(string token);
        Task<IQueryable<ApplicationUser>> GetUserList(string tokenString);
    }
}
