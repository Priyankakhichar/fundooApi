////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "AccountManager.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Service
{
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Interface;
    using ServiceStack.Redis;
    using System;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// AccountManager class 
    /// </summary>
    public class AccountManager :IAccountManager
    {
        /// <summary>
        /// AccountManager Repository instance created
        /// </summary>
        private IAccountManagerRepository accountManagerRepository;
        //
        private string lastLogin;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accountManagerRepository"></param>
        public AccountManager(IAccountManagerRepository accountManagerRepository)
        {
            this.accountManagerRepository = accountManagerRepository;
        }

        /// <summary>
        /// RegisterUser method doing the registration for user
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public async Task<bool> RegisterUser(UserRegistration registration)
        {
            try
            {
                if (!registration.Equals(null))
                {
                    var result = await this.accountManagerRepository.Register(registration);
                    return result;
                }
                else
                {
                    throw new Exception("registration model is null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }
        /// <summary>
        /// Login methods it verify the login details from data base
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public async Task<string> Login(LoginModel loginModel)
        {
            try
            {
                var lastLoginTime = "";
                ////if loginModel is not null it will return result else throw the exception 
                if (!loginModel.Equals(null))
                {
                    var result = await this.accountManagerRepository.Login(loginModel);
                    if(result != null)
                    {
                        var newLoginTime = DateTime.Now.ToString();
                        
                        using (var redis = new RedisClient())
                        {
                            if (redis.Get("lastLogin" + loginModel.UserName) == null)
                            {
                                redis.Set("lastLogin" + loginModel.UserName, newLoginTime);
                            }
                            else
                            {
                               
                                string utfString =  System.Text.Encoding.UTF8.GetString(redis.Get("lastLogin" + loginModel.UserName));
                                redis.Set("lastLogin" + loginModel.UserName, newLoginTime);
                                lastLoginTime = "+" + utfString;
                           
                            }
                           // string utfString1 = System.Text.Encoding.UTF8.GetString(redis.Get("lastLogin" + loginModel.UserName));
                           // redis.Set("lastLogin", newLoginTime);
                           //// return utfString1;
                        }
                    }

                    return result + lastLoginTime;
                }
                else
                {
                    throw new Exception("login model is null");
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ForgetPassword method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> ForgetPassword(ForgetPasswordModel model)
        {
            var result = await this.accountManagerRepository.ForgetPassword(model);
            return result.ToString();
        }

        /// <summary>
        /// ResetPassword method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> ResetPassword(ResetPasswordModel model, string token)
        {
            var result = await this.accountManagerRepository.ResetPassword(model, token);
            return result;
        }

        /// <summary>
        /// image upload
        /// </summary>
        /// <param name="file"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string UploadImage(IFormFile file, string userId)
        {
            return this.accountManagerRepository.UploadImage(file, userId);
        }
    }
}
