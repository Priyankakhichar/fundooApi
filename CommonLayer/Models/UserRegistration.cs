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

    /// <summary>
    /// user registration class
    /// </summary>
    public class UserRegistration
    {
        /// <summary>
        /// first Name with annotations
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "firstName should be at least 5 characters")]
        public string FirstName
        {
            get; set;
        }

        /// <summary>
        /// last name
        /// </summary>
        public string LastName
        {
            get; set;
        }

        /// <summary>
        /// email id
        /// </summary>
        [EmailAddress]
        [Required]
        public string EmailId
        {
            get; set;
        }

        /// <summary>
        /// password
        /// </summary>
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "password should be at least 5 characters")]
        public string Password
        {
            get; set;
        }

        /// <summary>
        /// User name
        /// </summary>
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string UserName { get; set; }
        public string Image
        {
            get; set;
        }
    }
}
