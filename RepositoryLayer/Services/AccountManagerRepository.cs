////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "AccountManagerRepository.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Services
{
    using CommonLayer;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using RepositoryLayer.Context;
    using RepositoryLayer.Interface;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using System.IdentityModel.Tokens;

    /// <summary>
    /// AccountManagerRepository class having main functionality
    /// </summary>
    public class AccountManagerRepository : IAccountManagerRepository
    {
        /// <summary>
        /// UserManager instance variable
        /// </summary>
        private UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// 
        /// </summary>
        private AuthenticationContext _context;

        /// <summary>
        /// SignInManager instance variable
        /// </summary>
        private SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// ApplicationSettings instance variable
        /// </summary>
        private readonly ApplicationSettings _appSettings;

        /// <summary>
        /// AccountManagerRepository constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="appSettings"></param>
        public AccountManagerRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings, AuthenticationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            this._context = context;
        }

        /// <summary>
        /// Register method register the user details in data base
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public async Task<bool> Register(UserRegistration registration)
        {
            ////creating the applicationUser instance to store details 
            ApplicationUser applicationUser = new ApplicationUser()
            {
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                Email = registration.EmailId,
                UserName=registration.UserName
            };
            try
            {
                ////here applicationUser instance have details of user and second argument have password and adding these details to the database
                var result = await _userManager.CreateAsync(applicationUser, registration.Password);
               return result.Succeeded;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Login method to for varify the email and password stored in database
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public async Task<string> Login(LoginModel loginModel)
        {
            ////finding the userName from database that we passed
            var user = await _userManager.FindByNameAsync(loginModel.UserName);

            ////if it gets user name again it looking for crossponding password
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                ////it creates the SecurityTokenDescriptor
                var tokenDescripter = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("userId", user.Id.ToString())
                    }),
                    ////DateTime.UtcNow sets the current system time. it allow user login for 30 minutes
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                ////it creates the security token
                var securityToken = tokenHandler.CreateToken(tokenDescripter);

                ////it writes security token to the token variable.
                var token = tokenHandler.WriteToken(securityToken);
                return token ;
            }
            else
            {
                ////if user name and password not matching 
                return "wrong user name or password";
            }
        }

        /// <summary>
        /// ForgetPassword method send mail to the msmq for perticular user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> ForgetPassword(ForgetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.EmailId);
            if (user != null)
            {
                ////msmq object
                MSMQ msmq = new MSMQ();

              
                    ////it creates the SecurityTokenDescriptor
                    var tokenDescripter = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim("Email", user.Email.ToString())
                        }),
                        ////DateTime.UtcNow sets the current system time. it allow user login for 30 minutes
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();

                    ////it creates the security token
                    var securityToken = tokenHandler.CreateToken(tokenDescripter);

                    ////it writes security token to the token variable.
                    var token = tokenHandler.WriteToken(securityToken);
                    msmq.SendEmailToQueue(model.EmailId, token);
                   

                    ////////generated token
                    //var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                    ////sending the mail to queue
                    //    msmq.SendEmailToQueue(model.EmailId, code);
                    //return code;
                
                return token;
            }
            else
            {
                return "Invalid user";
            }
        }

        /// <summary>
        /// ResetPassword
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> ResetPassword(ResetPasswordModel model, string tokenString)
        {
             

            var token = new JwtSecurityToken(jwtEncodedString: tokenString);

            var email = token.Claims.First(c => c.Type == "Email").Value;

            var user = await this._userManager.FindByEmailAsync(email);

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            if (!user.Equals(null))
            {
                var result = await this._userManager.ResetPasswordAsync(user, code, model.Password);
                return result.ToString();
            }
            else
            {
                return "somthing went wrong";
            }
        }

        /// <summary>
        /// uploading the image
        /// </summary>
        /// <param name="file"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string UploadImage(string url, string userId)
        {

            var user = this._userManager.FindByIdAsync(userId);
            if (user != null)
            {
                ////getting the row according to the user id
                var updatableRow = this._context.ApplicationUser.Where(u => u.Id == userId).FirstOrDefault();

                ////updating the url  to image attribute
                updatableRow.Image = url;

                ////saving the changes to the database
                var result = this._context.SaveChanges();

                if (result > 0)
                {
                    return "image uploaded successfully";
                }
                else
                {
                    return "somthing went wrong";
                }
            }
            else
            {
                return "user not exist";
            }
        }
    }
}
