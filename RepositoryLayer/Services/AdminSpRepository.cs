////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "AdminSpRepository.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Services
{
    using CommonLayer.Constants;
    using CommonLayer.Models;
    using CommonLayer.Struct;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using RepositoryLayer.Interface;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class AdminSpRepository : IAdminSpRepository
    {
        private readonly ApplicationSettings _appSettings;
        public AdminSpRepository(IOptions<ApplicationSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// connection string
        /// </summary>
        private static readonly string connectionstring = "Server= (LocalDb)\\localdbDemo;Database=FundooNotes;Trusted_connection=True;MultipleActiveResultSets=True";

        /// <summary>
        /// initilizating the connection
        /// </summary>
        private SqlConnection connection = new SqlConnection(connectionstring);

        /// <summary>
        /// regitser user method to register the user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>returns success or failuer message</returns>
        public string RegisterUser(UserRegistration model)
        {
            try
            {
                ////initilize the sql command to add query and connection
                SqlCommand command = new SqlCommand("AddSystemUser", connection);

                ////command type is stored procedure
                command.CommandType = CommandType.StoredProcedure;
                ////adding the parameter to the stored procedure
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

                ////opening the connection
                connection.Open();

                ////executing the sql query in stored procedure
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
                ////closing the connection
                connection.Close();
            }
        }

        /// <summary>
        /// login method for user login
        /// </summary>
        /// <param name="model"></param>
        /// <returns>returns token</returns>
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
                    return ErrorMessages.invalidUserErrorMessage;
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

        /// <summary>
        /// user statistics method to get user information
        /// </summary>
        /// <param name="token"></param>
        /// <returns>retiuns user count</returns>
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

        /// <summary>
        /// update user method to update user role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tokenString"></param>
        /// <param name="role"></param>
        /// <returns>returns success or failuer message</returns>
        public string UpdateUser(int userId, string tokenString, string role, bool isSuspended)
        {
            var token = new JwtSecurityToken(jwtEncodedString: tokenString);
            var userName = token.Claims.First(c => c.Type == "userName").Value;
            SqlCommand command = new SqlCommand("GetUsers", connection);
            command.CommandType = CommandType.StoredProcedure;

            ////adding the parameter to the stored procedure
            command.Parameters.AddWithValue("@UserName", userName);

            ////opening the connection
            connection.Open();

            ////executing the query
            command.ExecuteNonQuery();

            ////getting records from data base
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                ////command object for update user stored procedure
                command = new SqlCommand("UpdateUser", connection);
                command.CommandType = CommandType.StoredProcedure;

                ////adding the is, role, Is suspended parameter to the stored procedure
                command.Parameters.AddWithValue("@Id", userId);
                command.Parameters.AddWithValue("@Role", role);
                command.Parameters.AddWithValue("@IsSuspended", isSuspended);

                ////executing the sql command
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    return "record updated successfully";
                }
                ////closing the connection
                connection.Close();
                return "somthing went wrong, please try again";
            }
            else
            {
                throw new Exception("only admin can access this service");
            }
        }

        /// <summary>
        /// Get total notes method and total of notes , archive and trash note by user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>returning the count of notes</returns>
        public async Task<NoteTypes> GetTotalNotes(string userId)
        {
            try
            {
                using(SqlConnection newConnection = new SqlConnection(connectionstring))
                { 
                ////command object to connect with stored procedure
                using (SqlCommand command = new SqlCommand("GetNotes", newConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    ////adding the user id to the sotred procedure
                    command.Parameters.AddWithValue("@UserId", userId);
                        ////opening the connection
                        newConnection.Open();
                        ////executing the query in stored procedure
                        //await command.ExecuteNonQueryAsync();

                        ////data reader to read the records from database
                        using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                        {
                            var data = dataReader;
                            int noteCount = 0;
                            int archiveCount = 0;
                            int trashCount = 0;

                            ////object of struct NoteTypes
                            NoteTypes note = new NoteTypes();
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    ////if value of Note type is zero it will increment the notecount by 1.
                                    if (Convert.ToInt32(dataReader["NoteType"]) == 0)
                                    {
                                        noteCount++;
                                    }
                                    ////if value of notetype is 1 , it will increment the archive count by 1.
                                    else if (Convert.ToInt32(dataReader["Notetype"]) == 1)
                                    {
                                        archiveCount++;
                                    }
                                    ////if value of note type is 2 it will increment the trash count by 1.
                                    else
                                    {
                                        trashCount++;
                                    }
                                }
                               // dataReader.Close();
                                ////storing the values in struct variables
                                note.Notes = noteCount;
                                note.Archive = archiveCount;
                                note.Trash = trashCount;
                                return note;
                            }
                        }
                       
                    }
                    return new NoteTypes();
                        ////declared a variable and initlize it with zero to store the value of note count
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
