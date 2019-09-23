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
    public class ApplicationUser : IdentityUser
    {

        [Column(TypeName = "nvarchar(150)")]
        public string FirstName
        {
            get; set;
        }

        [Column(TypeName = "nvarchar(150)")]
        public string LastName
        {
            get; set;
        }
    }
}
