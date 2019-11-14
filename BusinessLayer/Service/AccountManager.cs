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
    using CommonLayer;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Interface;
    using ServiceStack.Redis;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
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
                        ////storing the current time in the variable
                        var newLoginTime = DateTime.Now.ToString();
                        
                        using (var redis = new RedisClient())
                        {
                            ////getting the logintime from redis
                            if (redis.Get("lastLogin" + loginModel.UserName) == null)
                            {
                                ////setting the login time to the redis
                                redis.Set("lastLogin" + loginModel.UserName, newLoginTime);
                            }
                            else
                            {
                                ////changing the datetime formate to the string formate
                                string utfString =  System.Text.Encoding.UTF8.GetString(redis.Get("lastLogin" + loginModel.UserName));
                                redis.Set("lastLogin" + loginModel.UserName, newLoginTime);
                                lastLoginTime = "+" + utfString;
                            }
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
            try
            {

                ////object created of custom class ImageCloudinary
                ImageCloudinary cloudinary = new ImageCloudinary();

                ////getting the url from cloudinary class
                var url = cloudinary.UploadImageAtCloudinary(file);

                return this.accountManagerRepository.UploadImage(url, userId);
            }
            catch(Exception ex)
            {
                throw new Exception("Invalid file");
            }
        }

        /// <summary>
        /// social login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns>returns token</returns>
        public async Task<string> SocialLogin(string email)
        {
            try
            {
                if (email != null)
                {
                    ////if email is not null it will go to the repository layer
                    return await this.accountManagerRepository.SocialLogin(email);
                }
                else
                {
                    return "email is empty";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// logout method
        /// </summary>
        /// <returns></returns>

        public async Task<string> Logout(string tokenString)
        {
           
            var token = new JwtSecurityToken(jwtEncodedString: tokenString);
            var email = token.Claims.First(c => c.Type == "Email").Value;
            try
            {
                var result =  await this.accountManagerRepository.Logout(tokenString);
                if(result != null)
                {
                    using(var redis = new RedisClient())
                    {
                        var key = email;
                        redis.Set(key, tokenString);
                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
