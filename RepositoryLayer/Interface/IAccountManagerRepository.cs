////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "IAccountManagerRepository.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Interface
{
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    public interface IAccountManagerRepository
    {
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        Task<bool> Register(UserRegistration registration);
        /// <summary>
        /// Login
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
        /// uploading the image 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        string UploadImage(string url, string userId);

        /// <summary>
        /// social login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns>returns token</returns>
        Task<string> SocialLogin(string email);

        /// <summary>
        /// logout
        /// </summary>
        /// <returns></returns>
        Task<string> Logout();
    }
}
