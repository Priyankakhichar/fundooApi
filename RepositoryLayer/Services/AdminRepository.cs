using CommonLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class AdminRepository : IAdminRepository
    {
        private AuthenticationContext context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _appSettings;
        public AdminRepository(AuthenticationContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings)
        {
            this.context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._appSettings = appSettings.Value;
        }

        public async Task<string> RegisterUser(UserRegistration registration)
        {
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
                    return "somthing went wrong";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
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
                            new Claim("email", user.Email.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();

                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
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

        public async Task<string> AddService(ServiceModel model)
        {
            ServiceModel service = new ServiceModel()
            {
                Id = model.Id,
                ServiceName = model.ServiceName,
                IsDeleted = model.IsDeleted,
                ModifiedDate = model.ModifiedDate,
                CreatedDate = model.CreatedDate
            };

            this.context.Add(service);
            var result = await this.context.SaveChangesAsync();
            if(result > 0)
            {
                return "service added successfully";
            }
            else
            {
                return "somthing went wrong";
            }
        }
        
        public (IList<ApplicationUser>, IList<ApplicationUser>) GetUserList(string tokenString)
        {
            var token = new JwtSecurityToken(jwtEncodedString: tokenString);

            var email = token.Claims.First(c => c.Type == "email").Value;

            var user =  this._userManager.FindByEmailAsync(email);
            IList<ApplicationUser> adminList = new List<ApplicationUser>();
            IList<ApplicationUser> userList = new List<ApplicationUser>();

            if(user != null)
            {
                var adminUsers = this.context.ApplicationUser.Where(g => g.ServiceId == 1);
                foreach(var admin in adminUsers)
                {
                    adminList.Add(admin);
                }

                var normalUser = this.context.ApplicationUser.Where(g => g.ServiceId == 2);
                foreach(var list in normalUser)
                {
                    userList.Add(list);
                }

                return (adminList, userList); 
            }
            else
            {
                throw new Exception("only admin can access this service");
            }
        }
    }
}
