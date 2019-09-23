////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "AuthenticationContext.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Context
{
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    /// <summary>
    /// AuthenticationContext
    /// </summary>
    public class AuthenticationContext : IdentityDbContext
    {
        public AuthenticationContext(DbContextOptions options) : base(options)
        {

        }
        /// <summary>
        /// ApplicationUser to create table
        /// </summary>
        public DbSet<ApplicationUser> ApplicationUser
        {
            get; set;
        }
    }
}
