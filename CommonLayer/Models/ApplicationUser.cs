////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "ApplicationUser.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace CommonLayer.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Application User class is for identity
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// first Name
        /// </summary>
        [Column(TypeName = "nvarchar(150)")]
        public string FirstName
        {
            get; set;
        }

        /// <summary>
        /// last Name
        /// </summary>
        [Column(TypeName = "nvarchar(150)")]
        public string LastName
        {
            get; set;
        }

        /// <summary>
        /// role type
        /// </summary>
        [Column(TypeName = "nvarchar(150)")]
        public string Role
        {
            get; set;
        }
       
        /// <summary>
        /// Image
        /// </summary>
        public string Image
        {
            get; set;
        }

        /// <summary>
        /// Token
        /// </summary>
        public string Token
        {
            get; set;
        }

        /// <summary>
        /// Service id 
        /// </summary>
        public int ServiceId
        {
            get; set;
        }
    }
}
