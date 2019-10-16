using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class AdminService : IAdminBussiness
    {
        private IAdminRepository adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        public Task<string> RegisterUser(UserRegistration registration)
        {
            try
            {
                return this.adminRepository.RegisterUser(registration);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> AdminLogin(LoginModel model)
        {
            try
            {
                return await this.adminRepository.AdminLogin(model);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> AddService(ServiceModel model)
        {
            try
            {
                return await this.adminRepository.AddService(model);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public (IList<ApplicationUser>, IList<ApplicationUser>) GetUserList(string token)
        {
            try
            {
                return this.adminRepository.GetUserList(email);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
