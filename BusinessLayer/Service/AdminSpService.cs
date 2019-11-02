using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class AdminSpService : IAdminSpBusiness
    {
        private readonly IAdminSpRepository repository;

        public AdminSpService(IAdminSpRepository repository)
        {
            this.repository = repository;
        }

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

        public string UpdateUser(int userId, string tokenString, string role)
        {
            try
            {
                var result = this.repository.UpdateUser(userId, tokenString, role);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
