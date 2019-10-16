using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
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
        (IList<ApplicationUser>, IList<ApplicationUser>) GetUserList(string token);
    }
}
