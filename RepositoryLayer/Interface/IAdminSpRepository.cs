using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAdminSpRepository
    {
        string RegisterUser(UserRegistration model);
        string Login(LoginModel model);
        UserStatisticsModel GetUserStatistics(string token);
        string UpdateUser(int userId, string tokenString, string role);
    }
}
