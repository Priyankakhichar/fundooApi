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
    /// Application User clas is for identity
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
       
        public string Image
        {
            get; set;
        }
    }
}
