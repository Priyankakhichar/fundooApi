////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "UserRegistration.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace CommonLayer.Models
{
    using System.ComponentModel.DataAnnotations;
    public class UserRegistration
    {
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "firstName should be at least 5 characters")]
        public string FirstName
        {
            get; set;
        }


        public string LastName
        {
            get; set;
        }

        [EmailAddress]
        [Required]
        public string EmailId
        {
            get; set;
        }

        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "password should be at least 5 characters")]
        public string Password
        {
            get; set;
        }

        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string UserName { get; set; }
    }
}
