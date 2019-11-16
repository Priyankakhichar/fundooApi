////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "AdminRepository.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Interface
{
    using CommonLayer.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// admin repository interface
    /// </summary>
    public interface IAdminRepository
    {
        /// <summary>
        /// register user
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
        /// method to register the service
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> AddService(ServiceModel model);

        /// <summary>
        /// get all user
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<UserStatisticsModel> GetUserNotesCount(string token);

        /// <summary>
        /// get user list
        /// </summary>
        /// <param name="tokenString"></param>
        /// <returns></returns>
        Task<IQueryable<ApplicationUser>> GetUserList(string tokenString);
    }
}
