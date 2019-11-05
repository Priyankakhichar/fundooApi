////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "IAdminSpBusiness.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Interface
{
    using CommonLayer.Models;
    using CommonLayer.Struct;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Admin interface
    /// </summary>
    public interface IAdminSpBusiness
    {
        /// <summary>
        /// Register user method
        /// </summary>
        /// <param name="model"></param>
        /// <returns>returns success or failure message</returns>
        string RegisterUser(UserRegistration model);

        /// <summary>
        /// login method
        /// </summary>
        /// <param name="model"></param>
        /// <returns>returns token</returns>
        string Login(LoginModel model);

        /// <summary>
        /// user statistics method
        /// </summary>
        /// <param name="token"></param>
        /// <returns>methods to get user information</returns>
        UserStatisticsModel GetUserStatistics(string token);

        /// <summary>
        /// update user method to update user as a admin
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tokenString"></param>
        /// <param name="role"></param>
        /// <returns>returns success or failuer message</returns>
        string UpdateUser(int userId, string tokenString, string role, bool isSuspended);

        Task<NoteTypes> GetTotalNotes(string userId);
    }
}
