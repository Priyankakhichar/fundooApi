////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "IAdminSpRepository.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Interface
{
    using CommonLayer.Models;
    using CommonLayer.Struct;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Admin repository interface
    /// </summary>
    public interface IAdminSpRepository
    {
        /// <summary>
        /// regitser user method to register the user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>returns success or failuer message</returns>
        string RegisterUser(UserRegistration model);

        /// <summary>
        /// login method for user login
        /// </summary>
        /// <param name="model"></param>
        /// <returns>returns token</returns>
        string Login(LoginModel model);

        /// <summary>
        /// user statistics method to get user information
        /// </summary>
        /// <param name="token"></param>
        /// <returns>retiuns user count</returns>
        UserStatisticsModel GetUserStatistics(string token);

        /// <summary>
        /// update user metgod to update user role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tokenString"></param>
        /// <param name="role"></param>
        /// <returns>returns success or failuer message</returns>
        string UpdateUser(int userId, string tokenString, string role, bool isSuspended);

        Task<NoteTypes> GetTotalNotes(string userId);

    }
}
