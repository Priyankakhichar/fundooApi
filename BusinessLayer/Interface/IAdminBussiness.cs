using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAdminBussiness
    {
        Task<string> RegisterUser(UserRegistration registration);
        Task<string> AdminLogin(LoginModel model);
        Task<string> AddService(ServiceModel model);
        (IList<ApplicationUser>, IList<ApplicationUser>) GetUserList(string token);
    }
}
