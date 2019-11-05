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
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// user registration class
    /// </summary>
    public class UserRegistration
    {
        /// <summary>
        /// first Name with annotations Allow up to 40 uppercase and lowercase 
        /// characters. Use custom error.
        /// </summary>
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",ErrorMessage = "Characters are not allowed.")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "firstName should be at least 5 characters")]
        public string FirstName
        {
            get; set;
        }

        /// <summary>
        /// id
        /// </summary>
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// last name
        /// </summary>
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "firstName should be at least 5 characters")]
        public string LastName
        {
            get; set;
        }

        /// <summary>
        /// email id
        /// </summary>
        [EmailAddress]
        [Required]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
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

        /// <summary>
        /// token
        /// </summary>
        public string Token
        {
            get; set;
        }

        /// <summary>
        /// Role type
        /// </summary>
        [Required]
        public string Role
        {
            get; set;
        }

        /// <summary>
        /// Service id
        /// </summary>
        [Required]
        [ForeignKey("ServiceModel")]
        public int ServiceId
        {
            get; set;
        }

        /// <summary>
        /// Is suspended filed for account suspend
        /// </summary>
        public bool IsSuspended
        {
            get; set;
        }

    }
}
