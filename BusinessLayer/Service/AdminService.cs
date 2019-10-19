////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "AdminService.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Service
{
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using RepositoryLayer.Interface;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// admin service class
    /// </summary>
    public class AdminService : IAdminBussiness
    {
        /// <summary>
        /// admin repository variable
        /// </summary>
        private IAdminRepository adminRepository;

        /// <summary>
        /// initialize the repository variable
        /// </summary>
        /// <param name="adminRepository"></param>
        public AdminService(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        /// <summary>
        /// registring the admin user
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public Task<string> RegisterUser(UserRegistration registration)
        {
            try
            {
                ////reference to the repository class 
                return this.adminRepository.RegisterUser(registration);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// admin login method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> AdminLogin(LoginModel model)
        {
            try
            {
                ////refrence to the repository class
                return await this.adminRepository.AdminLogin(model);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// add service method to add the services to the service table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> AddService(ServiceModel model)
        {
            try
            {
                ////reference to the repository class
                return await this.adminRepository.AddService(model);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// getting the all users to if user is admin
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<UserStatisticsModel> GetUserList(string token)
        {
            try
            {
                ////reference to the respository class
                return await this.adminRepository.GetUserList(token);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
