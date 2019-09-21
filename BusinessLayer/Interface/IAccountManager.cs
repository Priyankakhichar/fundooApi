

namespace BusinessLayer.Interface
{
    using CommonLayer.Models;
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
        Task<string> ResetPassword(ResetPasswordModel model);
    }
}
