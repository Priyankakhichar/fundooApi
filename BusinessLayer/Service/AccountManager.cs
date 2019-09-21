

namespace BusinessLayer.Service
{
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using RepositoryLayer.Interface;
    using System;
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
                ////if loginModel is not null it will return result else throw the exception 
                if (!loginModel.Equals(null))
                {
                    var result = await this.accountManagerRepository.Login(loginModel);
                    return result;
                }
                else
                {
                    throw new Exception("login model is null");
                }
            }
            catch(Exception ex)
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
        public async Task<string> ResetPassword(ResetPasswordModel model)
        {
            var result = await this.accountManagerRepository.ResetPassword(model);
            return result;
        }
    }
}
