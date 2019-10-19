////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "AdminRepository.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Services
{
    using CommonLayer.Models;
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

    /// <summary>
    /// admin repository class
    /// </summary>
    public class AdminRepository : IAdminRepository
    {
        /// <summary>
        /// authenticatioinContext variable
        /// </summary>
        private AuthenticationContext context;

        /// <summary>
        /// UserManager varaible
        /// </summary>
        private UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Siginin Manager type variable
        /// </summary>
        private SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// app settings variable
        /// </summary>
        private readonly ApplicationSettings _appSettings;

        /// <summary>
        /// variables are initilized through constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="appSettings"></param>
        public AdminRepository(AuthenticationContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings)
        {
            this.context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._appSettings = appSettings.Value;
        }

        /// <summary>
        /// register user method to register the admin user
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public async Task<string> RegisterUser(UserRegistration registration)
        {
            ////applicationUser model class to hold the entities value
            ApplicationUser applicationUser = new ApplicationUser()
            {
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                Email = registration.EmailId,
                UserName = registration.UserName,
                Token = registration.Token,
                Role = registration.Role
            };
            try
            {
                ////here applicationUser instance have details of user and second argument have password and adding these details to the database
                var result = await _userManager.CreateAsync(applicationUser, registration.Password);
                if(result != null)
                {
                    return "user registered successfully";
                }
                else
                {
                    return "something went wrong";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    
        /// <summary>
        /// admin login method for admin login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> AdminLogin(LoginModel model)
        {
            ////if user is exist it will return some result
            var user = await this._userManager.FindByNameAsync(model.UserName);

            ////if user and password is matching only then will go to next step
            if(user != null && await this._userManager.CheckPasswordAsync(user, model.Password))
            {
                ////if user is admin it will return result and go further to create token
                var result =  this.context.ApplicationUser.Where(g =>  g.Role == "admin" && g.UserName == model.UserName).FirstOrDefault();
                if(result != null)
                {
                    var tokenDescriptor = new SecurityTokenDescriptor()
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            ////setting the user claims
                            new Claim("email", user.Email.ToString())
                        }),
                        ////token expire validation
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();

                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);

                    ////generating the token
                    var token = tokenHandler.WriteToken(securityToken);
                    return token;
                }
                else
                {
                    return "Only admin can login";
                }
            }
            else
            {
                return "Invalid user";
            }
        }

        /// <summary>
        /// add service method to add the service to the service class
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> AddService(ServiceModel model)
        {
            ////service model class to hold the values of entities
            ServiceModel service = new ServiceModel()
            {
                Id = model.Id,
                ServiceName = model.ServiceName,
                IsDeleted = model.IsDeleted,
                ModifiedDate = model.ModifiedDate,
                CreatedDate = model.CreatedDate
            };

            ////adding the details to the database
            this.context.Add(service);

            ////saving the changes
            var result = await this.context.SaveChangesAsync();
            if(result > 0)
            {
                return "service added successfully";
            }
            else
            {
                return "something went wrong";
            }
        }
        
        /// <summary>
        /// get user list to get all user to the admin
        /// </summary>
        /// <param name="tokenString"></param>
        /// <returns></returns>
        public async Task<UserStatisticsModel> GetUserList(string tokenString)
        {
            ////decrpting the token
            var token = new JwtSecurityToken(jwtEncodedString: tokenString);

            ////getting the email id from token
            var email = token.Claims.First(c => c.Type == "email").Value;

            ////varifiyng the user by mail id
            var user = await this._userManager.FindByEmailAsync(email);

            var result = this.context.ApplicationUser.Where(g => g.Role == "admin" && g.Email == email).FirstOrDefault();

            UserStatisticsModel userStatisticsModel = new UserStatisticsModel();
            if (result != null)
            {
                ////getting the list of basic user
                var adminUsers = this.context.ApplicationUser.Where(g => g.ServiceId == 1);

                ////count of basic users
                userStatisticsModel.BasicUserCount = adminUsers.Count();
            
                ////getting the list of advance user
                var normalUser = this.context.ApplicationUser.Where(g => g.ServiceId == 2);

                ////count of advance users
                userStatisticsModel.AdvanceUserCount = normalUser.Count();

                return userStatisticsModel;
            }
            else
            {
                throw new Exception("Only admin can access this service");
            }
        }
    }
}
