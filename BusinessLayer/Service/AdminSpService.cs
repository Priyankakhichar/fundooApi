////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "AdminSpService.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Service
{
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using CommonLayer.Struct;
    using RepositoryLayer.Interface;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// admin service class
    /// </summary>
    public class AdminSpService : IAdminSpBusiness
    {
        private readonly IAdminSpRepository repository;

        /// <summary>
        /// constructor for dependency injection
        /// </summary>
        /// <param name="repository"></param>
        public AdminSpService(IAdminSpRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// register user method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string RegisterUser(UserRegistration model)
        {
            try
            {
                var result = this.repository.RegisterUser(model);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// login method for user login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Login(LoginModel model)
        {
            try
            {
                var result = this.repository.Login(model);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// user statistics method
        /// </summary>
        /// <param name="token"></param>
        /// <returns>methods to get user information</returns>
        public UserStatisticsModel GetUserStatistics(string token)
        {
           try
            {
                var result = this.repository.GetUserStatistics(token);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// update user method to update user as a admin
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tokenString"></param>
        /// <param name="role"></param>
        /// <returns>returns success or failuer message</returns>
        public string UpdateUser(int userId, string tokenString, string role, bool isSuspended)
        {
            try
            {
                var result = this.repository.UpdateUser(userId, tokenString, role, isSuspended);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<NoteTypes> GetTotalNotes(string userId)
        {
            try
            {
                var result = await repository.GetTotalNotes(userId);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
