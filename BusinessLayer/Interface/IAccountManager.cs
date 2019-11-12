////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "IAccountManager.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Interface
{
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    /// <summary>
    /// IAccountInterface has method for registration, login and forget password
    /// </summary>
    public interface IAccountManager
    {
        /// <summary>
        /// Register Login Details method
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        Task<bool> RegisterUser(UserRegistration registration);

        /// <summary>
        /// Login methods
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        Task<string> Login(LoginModel loginModel);

        /// <summary>
        /// ForgetPassword 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> ForgetPassword(ForgetPasswordModel model);

        /// <summary>
        /// ResetPassword
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> ResetPassword(ResetPasswordModel model, string token);

        /// <summary>
        /// image upload
        /// </summary>
        /// <param name="file"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        string UploadImage(IFormFile file, string userId);

        /// <summary>
        /// social login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        Task<string> SocialLogin(string email);

        /// <summary>
        /// logout
        /// </summary>
        /// <returns></returns>
        Task<string> Logout(string token);
    }
}
