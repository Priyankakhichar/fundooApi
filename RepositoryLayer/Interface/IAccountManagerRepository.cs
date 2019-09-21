
namespace RepositoryLayer.Interface
{
    using CommonLayer.Models;
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
        Task<string> ResetPassword(ResetPasswordModel model);
    }
}
