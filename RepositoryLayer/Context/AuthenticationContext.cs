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
        public AuthenticationContext(DbContextOptions options, object options1) : base(options)
        {

        }
        /// <summary>
        /// ApplicationUser to create table
        /// </summary>
        public DbSet<ApplicationUser> ApplicationUser
        {
            get; set;
        }

        /// <summary>
        /// notes model
        /// </summary>
        public DbSet<NotesModel> NotesModels
        {
            get; set;
        }

        /// <summary>
        /// label model
        /// </summary>
        public DbSet<LabelModel> LabelModels
        {
            get; set;
        }

        /// <summary>
        /// collaboration
        /// </summary>
        public DbSet<NotesCollaboration> Collaborations
        {
            get; set;
        }

        /// <summary>
        /// service
        /// </summary>
        public DbSet<ServiceModel> Service
        {
            get; set;
        }
    }
}
