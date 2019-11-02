using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAdminSpBusiness
    {
        string RegisterUser(UserRegistration model);
        string Login(LoginModel model);
        UserStatisticsModel GetUserStatistics(string token);
        string UpdateUser(int userId, string tokenString, string role);
    }
}
