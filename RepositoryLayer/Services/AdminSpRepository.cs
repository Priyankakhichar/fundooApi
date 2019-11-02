using CommonLayer.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AdminSpRepository : IAdminSpRepository
    {
        private readonly ApplicationSettings _appSettings;
        public AdminSpRepository(IOptions<ApplicationSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        private static readonly string connectionstring = "Server= (LocalDb)\\localdbDemo;Database=FundooNotes;Trusted_connection=True;MultipleActiveResultSets=True";

        private SqlConnection connection = new SqlConnection(connectionstring);

        public string RegisterUser(UserRegistration model)
        {
            try
            {
                SqlCommand command = new SqlCommand("AddSystemUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", model.Id);
                command.Parameters.AddWithValue("@FirstName", model.FirstName);
                command.Parameters.AddWithValue("@LastName", model.LastName);
                command.Parameters.AddWithValue("@EmailId", model.EmailId);
                command.Parameters.AddWithValue("@Password", model.Password);
                command.Parameters.AddWithValue("@UserName", model.UserName);
                command.Parameters.AddWithValue("@token", model.Token);
                command.Parameters.AddWithValue("@Role", model.Role);
                command.Parameters.AddWithValue("@ServiceId", model.ServiceId);
                command.Parameters.AddWithValue("@EmailConfirmed", 0);
                command.Parameters.AddWithValue("@PhoneNumberConfirmed", 0);
                command.Parameters.AddWithValue("@TwoFactorEnabled", 0);
                command.Parameters.AddWithValue("@AccessFailedCount", 0);
                command.Parameters.AddWithValue("@Discriminator", "ApplicationUser");
                command.Parameters.AddWithValue("@LockoutEnabled", 1);

                connection.Open();
                int i = command.ExecuteNonQuery();
                if (i > 0)
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
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        public string Login(LoginModel model)
        {
            try
            {
                SqlCommand command = new SqlCommand("sp_GetAllUsers", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", model.UserName);
                connection.Open();
                command.ExecuteNonQuery();
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        if (dataReader["PasswordHash"].ToString() == model.Password && dataReader["Role"].ToString() == "admin")
                        {
                            var tokenDescriptor = new SecurityTokenDescriptor()
                            {
                                Subject = new ClaimsIdentity(new Claim[]
                                {
                                    new Claim("userName", dataReader["UserName"].ToString())
                                }),
                                Expires = DateTime.UtcNow.AddDays(1),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                            };
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var securitytoken = tokenHandler.CreateToken(tokenDescriptor);
                            var token = tokenHandler.WriteToken(securitytoken);
                            return token;
                        }
                        else
                        {
                            return "only admin can login";
                        }
                    }

                    dataReader.Close();
                    return "wrong password";
                }
                else
                {
                    return "invalid user";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public UserStatisticsModel GetUserStatistics(string tokenString)
        {
            ////decripting the token
            var token = new JwtSecurityToken(jwtEncodedString: tokenString);

            ////claims the userName from token
            var userName = token.Claims.First(c => c.Type == "userName").Value;
            SqlCommand command = new SqlCommand("GetUsers", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserName", userName);
            ////opening the connection
            connection.Open();
            command.ExecuteNonQuery();

            ////getting records from data base
            SqlDataReader reader = command.ExecuteReader();
            int basicUserCount = 0;
            int advanceUserCount = 0;
            UserStatisticsModel statisticsModel = new UserStatisticsModel();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ////reading the next result set
                    reader.NextResult();
                    while (reader.Read())
                    {
                        basicUserCount++;
                    }

                    ////reading the next result set
                    reader.NextResult();
                    while (reader.Read())
                    {
                        advanceUserCount++;
                    }
                }

                ////basic user count
                statisticsModel.BasicUserCount = basicUserCount;

                ////total user count
                statisticsModel.AdvanceUserCount = advanceUserCount;

                ////total user in table
                statisticsModel.TotalUsers = basicUserCount + advanceUserCount;
                connection.Close();
                return statisticsModel;
            }
            else
            {
                throw new Exception("only admin can access this service");
            }
        }

        public string UpdateUser(int userId, string tokenString, string role)
        {
            var token = new JwtSecurityToken(jwtEncodedString: tokenString);
            var userName = token.Claims.First(c => c.Type == "userName").Value;
            SqlCommand command = new SqlCommand("GetUsers", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserName", userName);
            ////opening the connection
            connection.Open();
            command.ExecuteNonQuery();

            ////getting records from data base
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                command = new SqlCommand("UpdateUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", userId);
                command.Parameters.AddWithValue("@Role", role);
                int i = command.ExecuteNonQuery();
                if(i > 0)
                {
                    return "role updated successfully";
                }

                connection.Close();
                return "somthing went wrong, please try again";
            }
            else
            {
                throw new Exception("only admin can access this service");
            }
        }
    }
}
